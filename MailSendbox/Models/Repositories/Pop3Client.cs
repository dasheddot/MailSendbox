
using System.Linq;

namespace MailSendbox.Models.Repositories {
    public class Pop3Client : OpenPop.Pop3.Pop3Client, IPop3Client {

        public Mail GetMail(int messageId) {
            var message = this.GetMessage(messageId);
            var mail = new Mail {
                                    Body = message.FindFirstPlainTextVersion().GetBodyAsText(),
                                    Date = message.Headers.DateSent,
                                    From = message.Headers.From.Address,
                                    To = message.Headers.To.Aggregate("", (seed, recipient) => seed += ", " + recipient.Address).Trim(' ', ','),
                                    Subject = message.Headers.Subject
                                };
            return mail;
        }

    }
}