using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Business.Serives;

namespace Business.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return Redirect("/index.html");
        }

        public ActionResult About()
        {
            ViewBag.Message = "你的应用程序说明页。";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "你的联系方式页。";

            return View();
        }

        public ActionResult Market()
        {
            return View();
        }

        public ActionResult Reader()
        {
            return View();
        }
    }
}
