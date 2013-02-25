using System;

namespace MailSendbox.Core
{
    public class Mail
    {
        public string Id { get; set; }
        public string MessageId { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public DateTime ReceivedDate { get; set; }

        // TODO: lot more properties to avoid data loss.
        // TODO: add attachementss

        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
    }
}