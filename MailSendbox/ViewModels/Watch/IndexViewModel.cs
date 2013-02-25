using System.Collections.Generic;
using MailSendbox.Models;

namespace MailSendbox.ViewModels.Home
{
    public class IndexViewModel
    {
        public IEnumerable<Mail> Mails { get; set; }
    }
}