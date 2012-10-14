using OpenPop.Pop3;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace MailSendbox.Models.Repositories {
    public class MailRepository : IMailRepository {

        private readonly IPop3Client _pop3Client;

        public MailRepository(IPop3Client pop3Client) {
            _pop3Client = pop3Client;
        }

        public IEnumerable<Mail> Get() {
            var server = ConfigurationManager.AppSettings["Pop3Server"];
            var user = ConfigurationManager.AppSettings["Pop3Username"];
            var password = ConfigurationManager.AppSettings["Pop3Password"];

            var result = new List<Mail>();

            _pop3Client.Connect(server, 110, false);
            _pop3Client.Authenticate(user, password, AuthenticationMethod.UsernameAndPassword);

            var messageCount = _pop3Client.GetMessageCount();
            for (var i = messageCount; i > 0; i--) 
                result.Add(_pop3Client.GetMail(i));

            _pop3Client.Disconnect();

            return result;
        }

    }
}