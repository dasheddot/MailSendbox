using MailSendbox.Models;
using MailSendbox.ViewModels.Home;
using System;
using System.Web.Mvc;

namespace MailSendbox.Controllers {
    public class HomeController : Controller {
        private readonly MailRepository _mailRepo;

        public HomeController(MailRepository mailRepo) {
            _mailRepo = mailRepo;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TestErrorReporting() {
            throw new ApplicationException("This exception is just a test.");
        }
    }
}
