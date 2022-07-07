using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Examen_ASP.Net.Controllers
{
    public class LangController : Controller
    {
        // GET: Lang
        [HttpPost]

        public ActionResult Index(string lg)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("fr");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("fr");
            HttpCookie cookie = new HttpCookie("lang");
            cookie.Value = "fr";
            Response.Cookies.Add(cookie);
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}