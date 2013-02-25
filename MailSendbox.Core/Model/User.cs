using System.Collections.Generic;

namespace MailSendbox.Core
{
    public class User
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public IList<DenormalizedMail> Mails { get; set; }
    }
}