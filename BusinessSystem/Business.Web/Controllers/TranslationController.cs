using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Business.Core;
using Business.Serives;

namespace Business.Web.Controllers
{
    public class TranslationController : Controller
    {
       
        [HttpGet]
        public ActionResult EmailTranslationAdd()
        {
            IList<BaseDictionary> languageList = BaseService.GetBaseDictionaries(ValueTypeEnum.Language);
            return View(languageList);
        }

        [HttpPost]
        public ActionResult EmailTranslationAdd(string emailTheme, long originalLanguage, long targetLanguage)
        {
            string filePath = string.Empty;
            if (Request.Files.Count > 0 && Request.Files[0].ContentLength>0)
            {
                HttpPostedFileBase file = Request.Files[0];
                file.SaveAs(Server.MapPath(@"\uploadFile"));
            }
            return null;
        }

        [HttpGet]
        public ActionResult HasReadTranslationList()
        {
            return View();
        }
        [HttpGet]
        public ActionResult UnReadTranslationList()
        {
            return View();
        }
        [HttpGet]
        public ActionResult RecycledTranslationList()
        {
            return View();
        }
    }
}
