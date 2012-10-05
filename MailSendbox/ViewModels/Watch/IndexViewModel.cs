using MailSendbox.Models;
using System.Collections.Generic;
using System.Linq;

namespace MailSendbox.ViewModels.Home {
    public class IndexViewModel {

        public IndexViewModel(IReadOnlyRepository<Mail> mailRepo)
        {
            Mails = mailRepo.Get().Take(10);
        }

        public IEnumerable<Mail> Mails { get; private set; }
    }
}