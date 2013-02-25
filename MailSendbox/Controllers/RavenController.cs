using System.Web.Mvc;
using MailSendbox.Infrastructure.Raven;
using Raven.Client;

namespace MailSendbox.Controllers
{
    [RavenActionFilter]
    public abstract class RavenController : Controller
    {
        public new IDocumentSession Session { get; set; }
    }
}