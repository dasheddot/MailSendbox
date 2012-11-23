using OpenPop.Pop3;
using System.Collections.Generic;
using System.Configuration;

namespace MailSendbox.Models.Repositories
{
    public class MailRepository : IMailRepository
    {
        private readonly IPop3Client _pop3Client;

        public MailRepository(IPop3Client pop3Client)
        {
            _pop3Client = pop3Client;
        }

        public IEnumerable<Mail> Get()
        {
            this.Connect();
            
            var result = new List<Mail>();

            var messageCount = _pop3Client.GetMessageCount();
            for (var i = messageCount; i > 0; i--)
                result.Add(_pop3Client.GetMail(i));

            this.Disconnect();

            return result;
        }

        public void Delete(Mail serverMail)
        {
            this.Connect();

            //_pop3Client.Delete(serverMail.MessageId);

            this.Disconnect();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>Refactor this to Pop3Client</remarks>
        protected virtual void Connect()
        {
            var server = ConfigurationManager.AppSettings["Pop3Server"];
            var user = ConfigurationManager.AppSettings["Pop3Username"];
            var password = ConfigurationManager.AppSettings["Pop3Password"];

            _pop3Client.Connect(server, 110, false);
            _pop3Client.Authenticate(user, password, AuthenticationMethod.UsernameAndPassword);
        }

        protected virtual void Disconnect()
        {
            _pop3Client.Disconnect();
        }
    }
}