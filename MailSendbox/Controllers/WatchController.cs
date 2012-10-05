using MailSendbox.Models;
using MailSendbox.ViewModels.Home;
using System;
using System.Web.Mvc;

namespace MailSendbox.Controllers
{
    public class WatchController : Controller
    {
        private readonly MailRepository _mailRepo;

        public WatchController(MailRepository mailRepo)
        {
            _mailRepo = mailRepo;
        }

        public ActionResult Index()
        {
            var viewModel = new IndexViewModel(_mailRepo);
            return View(viewModel);
        }
    }
}
