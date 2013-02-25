using System.Collections.Generic;
using System.Configuration;
using OpenPop.Pop3;

namespace MailSendbox.Models.Repositories
{
    public class MailRepository : IMailRepository
    {
        private readonly IPop3Client _pop3Client;

        public MailRepository(IPop3Client pop3Client)
        {
            _pop3Client = pop3Client;
        }

        #region IMailRepository Members

        public IEnumerable<Mail> Get()
        {
            Connect();

            var result = new List<Mail>();

            int messageCount = _pop3Client.GetMessageCount();
            for (int i = messageCount; i > 0; i--)
                result.Add(_pop3Client.GetMail(i));

            Disconnect();

            return result;
        }

        public void Delete(Mail serverMail)
        {
            Connect();

            //_pop3Client.Delete(serverMail.MessageId);

            Disconnect();
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>Refactor this to Pop3Client</remarks>
        protected virtual void Connect()
        {
            string server = ConfigurationManager.AppSettings["Pop3Server"];
            string user = ConfigurationManager.AppSettings["Pop3Username"];
            string password = ConfigurationManager.AppSettings["Pop3Password"];

            _pop3Client.Connect(server, 110, false);
            _pop3Client.Authenticate(user, password, AuthenticationMethod.UsernameAndPassword);
        }

        protected virtual void Disconnect()
        {
            _pop3Client.Disconnect();
        }
    }
}