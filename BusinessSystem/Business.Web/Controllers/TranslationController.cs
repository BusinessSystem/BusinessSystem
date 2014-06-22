using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Business.Web.Controllers
{
    public class TranslationController : Controller
    {
       
        public ActionResult EmailTranslationAdd()
        {
            return View();
        }
        public ActionResult HasReadTranslationList()
        {
            return View();
        }
        public ActionResult UnReadTranslationList()
        {
            return View();
        }

        public ActionResult RecycledTranslationList()
        {
            return View();
        }
    }
}
