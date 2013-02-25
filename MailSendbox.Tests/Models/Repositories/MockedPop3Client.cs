using System;
using MailSendbox.Models;
using MailSendbox.Models.Repositories;

namespace MailSendbox.Tests.Models.Repositories
{
    public class MockedPop3Client : IPop3Client
    {
        private readonly int _messageCount;

        public MockedPop3Client(int messageCount)
        {
            _messageCount = messageCount;
        }

        #region IPop3Client Members

        public void Connect(string host, int port, bool useSsl)
        {
            //do nothing here
        }

        public void Authenticate(string user, string password, AuthenticationMethod authMethod)
        {
            //to nothing here
        }

        public int GetMessageCount()
        {
            return _messageCount;
        }

        public Mail GetMail(int messageId)
        {
            if (messageId > _messageCount || messageId <= 0)
                throw new ArgumentException("messageId");

            return new Mail
                       {
                           Subject = "Subject for message #" + messageId,
                           Body = "Body for" + Environment.NewLine + "message #" + messageId,
                           Date = DateTime.Now,
                           From = "unittest@mailsendbox.net",
                           To = "unittest@mailsendbox.net"
                       };
        }

        public void Disconnect()
        {
            //do nothing here
        }

        public void Delete(int messageId)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}