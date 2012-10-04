using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MailSendbox.Models;
using MailSendbox.ViewModels.Home;

namespace MailSendbox.Controllers {
    public class HomeController : Controller {
        private readonly MailRepository _mailRepo;

        public HomeController(MailRepository mailRepo) {
            _mailRepo = mailRepo;
        }


        public ActionResult Index() {
            var viewModel = new IndexViewModel(_mailRepo);
            return View(viewModel);
        }
    }
}
