using MailSendbox.Models;
using MailSendbox.ViewModels.Home;
using System;
using System.Web.Mvc;

namespace MailSendbox.Controllers
{
    public class GetStartedController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
