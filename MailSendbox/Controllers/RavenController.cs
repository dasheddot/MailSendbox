using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Raven.Client;

namespace MailSendbox.Controllers
{
    public abstract class RavenController : Controller
    {
        public new IDocumentSession Session { get; set; }
    }
}
