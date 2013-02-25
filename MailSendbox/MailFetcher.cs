using System;
using System.Collections.Generic;
using System.Threading;
using MailSendbox.Models.Repositories;

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
        private readonly IDocumentStore _documentStore;
        private readonly IMailRepository _mailRepository;
        private Timer _timer;

        public MailFetcher(IDocumentStore documentStore, IMailRepository mailRepository)
        {
            _mailRepository = mailRepository;
            _documentStore = documentStore;
        }

        public void Start()
        {
            _timer = new Timer((state) => Fetch());
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
            IEnumerable<Models.Mail> serverMails = _mailRepository.Get();

            foreach (Models.Mail serverMail in serverMails)
            {
                using (var session = _documentStore.OpenSession())
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