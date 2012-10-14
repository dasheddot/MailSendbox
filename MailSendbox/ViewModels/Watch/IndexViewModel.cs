using MailSendbox.Models;
using System.Collections.Generic;
using System.Linq;

namespace MailSendbox.ViewModels.Home {
    public class IndexViewModel {

        public IEnumerable<Mail> Mails { get; set; }

    }
}