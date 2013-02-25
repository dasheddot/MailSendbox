using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;

namespace MailSendbox.Infrastructure.Raven
{
    public class DocumentStoreHolder
    {
        private static IDocumentStore documentStore;
        private static readonly ConcurrentDictionary<Type, Accessors> AccessorsCache = new ConcurrentDictionary<Type, Accessors>();

        public static IDocumentStore DocumentStore
        {
            get { return (documentStore ?? (documentStore = CreateDocumentStore())); }
        }

        private static IDocumentStore CreateDocumentStore()
        {
            var parser = ConnectionStringParser<RavenConnectionStringOptions>.FromConnectionStringName("RavenDB");
            parser.Parse();

            var store = new EmbeddableDocumentStore
                            {
                                ApiKey = parser.ConnectionStringOptions.ApiKey,
                                Url = parser.ConnectionStringOptions.Url,
                                UseEmbeddedHttpServer = false,
                            }.Initialize();

            return store;
        }

        private static Accessors CreateAccessorsForType(Type type)
        {
            PropertyInfo sessionProp =
                type.GetProperties().FirstOrDefault(
                    x => x.PropertyType == typeof (IDocumentSession) && x.CanRead && x.CanWrite);
            if (sessionProp == null)
                return null;

            return new Accessors
                       {
                           Set = (instance, session) => sessionProp.SetValue(instance, session, null),
                           Get = instance => (IDocumentSession) sessionProp.GetValue(instance, null)
                       };
        }


        public static IDocumentSession TryAddSession(object instance)
        {
            Accessors accessors = AccessorsCache.GetOrAdd(instance.GetType(), CreateAccessorsForType);

            if (accessors == null)
                return null;

            var documentSession = DocumentStore.OpenSession();
            accessors.Set(instance, documentSession);

            return documentSession;
        }

        public static void TryComplete(object instance, bool succcessfully)
        {
            Accessors accesors;
            if (AccessorsCache.TryGetValue(instance.GetType(), out accesors) == false || accesors == null)
                return;

            using (var documentSession = accesors.Get(instance))
            {
                if (documentSession == null)
                    return;

                if (succcessfully)
                    documentSession.SaveChanges();
            }
        }

        public static void Initialize()
        {
            IndexCreation.CreateIndexes(typeof (DocumentStoreHolder).Assembly, DocumentStore);
        }

        public static void TrySetSession(object instance, IDocumentSession documentSession)
        {
            Accessors accessors = AccessorsCache.GetOrAdd(instance.GetType(), CreateAccessorsForType);

            if (accessors == null)
                return;

            accessors.Set(instance, documentSession);
        }

        #region Nested type: Accessors

        private class Accessors
        {
            public Func<object, IDocumentSession> Get;
            public Action<object, IDocumentSession> Set;
        }

        #endregion
    }
}