using System.Web.Mvc;
using MailSendbox.Core;
using MailSendbox.ViewModels.Home;

namespace MailSendbox.Controllers
{
    public class InboxController : RavenController
    {
        public ActionResult Index()
        {
            var viewModel = new IndexViewModel
                                {
                                    Mails = Session.Query<Mail>().Take(10).Select(x => new Models.Mail
                                                                                           {
                                                                                               Body = x.Body,
                                                                                               Date = x.ReceivedDate,
                                                                                               From = x.From,
                                                                                               MessageId = x.MessageId,
                                                                                               Subject = x.Subject,
                                                                                               To = x.To,
                                                                                           })
                                };
            return View(viewModel);
        }
    }
}