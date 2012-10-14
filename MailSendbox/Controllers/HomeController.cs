using MailSendbox.Models;
using MailSendbox.Models.Repositories;
using MailSendbox.ViewModels.Home;
using System;
using System.Web.Mvc;

namespace MailSendbox.Controllers {
    public class HomeController : Controller {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TestErrorReporting() {
            throw new ApplicationException("This exception is just a test.");
        }
    }
}
