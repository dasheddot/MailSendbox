using System.Web;
using System.Web.Mvc;

namespace MailSendbox
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //don't use MVC error handling yet, I want to see the real ASP.NET stuff.
            //filters.Add(new HandleErrorAttribute());
        }
    }
}