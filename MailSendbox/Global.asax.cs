using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MailSendbox.Core;
using MailSendbox.Infrastructure.Raven;
using MailSendbox.Models.Repositories;

namespace MailSendbox
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            DocumentStoreHolder.Initialize();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            // OPTIMIZE: use either push notification or make sure instances are injected properly
            new MailFetcher(DocumentStoreHolder.DocumentStore, new MailRepository(new Pop3Client())).Start();
        }
    }
}