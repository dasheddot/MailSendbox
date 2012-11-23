using MailSendbox.Models.Repositories;
using MailSendbox.ViewModels.Home;
using System.Linq;
using System.Web.Mvc;

namespace MailSendbox.Controllers
{
    public class InboxController : RavenController
    {
        private readonly IMailRepository _mailRepo;

        public InboxController(IMailRepository mailRepo)
        {
            _mailRepo = mailRepo;
        }

        public ActionResult Index()
        {
            var viewModel = new IndexViewModel()
            {
                Mails = _mailRepo.Get().Take(10)
            };
            return View(viewModel);
        }
    }
}
