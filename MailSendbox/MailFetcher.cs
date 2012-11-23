using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using MailSendbox.Models.Repositories;
using Raven.Client;
using Raven;
using Raven.Client.Linq;
using Raven.Abstractions.Extensions;

namespace MailSendbox.Core
{
    /// <summary>
    /// Fetches new mails from mail server to mail cached.
    /// </summary>
    /// <remarks>Note that this should be removed as soon as
    /// push mechanism was introduced. Polling will slow down
    /// the mail server when we want to reach a good latency.</remarks>
    public class MailFetcher
    {
        private readonly IMailRepository _mailRepository;
        private readonly IDocumentStore _documentStore;
        private Timer _timer;

        public MailFetcher(IDocumentStore documentStore, IMailRepository mailRepository)
        {
            this._mailRepository = mailRepository;
            this._documentStore = documentStore;
        }

        public void Start()
        {
            _timer = new Timer((state) => this.Fetch());
            _timer.Change(TimeSpan.FromSeconds(0), TimeSpan.FromMinutes(30));
        }

        public void Stop()
        {
            _timer.Change(-1, -1);
            _timer.Dispose();
            _timer = null;
        }

        public void Fetch()
        {
            var serverMails = _mailRepository.Get();

            foreach (var serverMail in serverMails)
            {
                using (var session = this._documentStore.OpenSession())
                {
                    if (session.Query<Mail>().Any(x => x.MessageId == serverMail.MessageId))
                        continue;

                    Models.Mail mail = serverMail;

                    // load existing user
                    var user = session.Query<User>()
                        .Where(x => x.Email.Equals(mail.From, StringComparison.InvariantCultureIgnoreCase))
                        .Customize(x => x.WaitForNonStaleResultsAsOfNow())
                        .FirstOrDefault();

                    // create user if not exist
                    if (user == null)
                    {
                        user = new User
                            {
                                Email = serverMail.From,
                                Username = serverMail.From
                            };
                        session.Store(user);
                    }

                    session.Store(new Mail
                        {
                            MessageId = serverMail.MessageId,
                            Subject = serverMail.Subject,
                            Body = serverMail.Body,
                            From = serverMail.From,
                            To = serverMail.To,
                            ReceivedDate = serverMail.Date,
                            UserId = user.Id,
                            UserName = user.Username,
                            UserEmail = user.Email,
                        });

                    session.SaveChanges();
                }
            }
        }
    }
}