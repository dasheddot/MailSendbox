using System.Configuration;
using OpenPop.Pop3;
using System.Collections.Generic;
using System.Linq;

namespace MailSendbox.Models {
    public class MailRepository : IReadOnlyRepository<Mail> {
        public IEnumerable<Mail> Get() {

            var server = ConfigurationManager.AppSettings["Pop3Server"];
            var user = ConfigurationManager.AppSettings["Pop3Username"];
            var password = ConfigurationManager.AppSettings["Pop3Password"];

            var result = new List<Mail>();
            using (var popClient = new OpenPop.Pop3.Pop3Client()) {
                popClient.Connect(server, 110, false);
                popClient.Authenticate(user, password, AuthenticationMethod.UsernameAndPassword);

                var messageCount = popClient.GetMessageCount();
                for (var i = messageCount; i > 0; i--) {
                    var message = popClient.GetMessage(i);
                    var mail = new Mail {
                                            Body = message.FindFirstPlainTextVersion().GetBodyAsText(),
                                            Date = message.Headers.DateSent,
                                            From = message.Headers.From.Address,
                                            To = message.Headers.To.Aggregate("", (seed, recipient) => seed += ", " + recipient.Address).Trim(' ', ','),
                                            Subject = message.Headers.Subject
                                        };
                    result.Add(mail);
                }

            }
            return result;
        }
    }
}