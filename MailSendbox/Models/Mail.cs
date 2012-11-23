using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MailSendbox.Models
{
    public class Mail
    {
        public string MessageId { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public DateTime Date { get; set; }
    }
}