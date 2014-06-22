using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Business.Core;
using Business.Serives;
using Business.Utils.Info;
using Business.Web.PageModel;

namespace Business.Web.Controllers
{
    public class TranslationController : AdminBaseController
    {
       
        [HttpGet]
        public ActionResult EmailTranslationAdd()
        {
            IList<BaseDictionary> languageList = BaseService.GetBaseDictionaries(ValueTypeEnum.Language);
            return View(languageList);
        }

        [HttpPost]
        [ValidateInput(false)] 
        public ActionResult EmailTranslationAdd(string emailTheme,string translationContent,long originalLanguage, long targetLanguage)
        {
            string filePath = string.Empty;
            if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
            {
                HttpPostedFileBase file = Request.Files[0];
            }
            Manager superManager = ManageService.GetSuperManager();
            if (superManager != null)
            {
                EmailTranslation emailTranslation = EmailTranslationFactory.Create(superManager.Id, CurrentManager.Id,
                    emailTheme,
                    translationContent, filePath);
                EmailFollow emailFollow = EmailFollowFactory.Create(0,
                    SystemDictionary.GetInstance.GetBaseDictionary(originalLanguage).Value,
                    SystemDictionary.GetInstance.GetBaseDictionary(targetLanguage).Value);
                string result=TranslationService.SaveTranslation(emailTranslation, emailFollow);
                if (result == ResponseCode.Ok)
                {
                    return RedirectToAction("HasReadTranslationList");
                }
            }
            return RedirectToAction("EmailTranslationAdd");
        }

        [HttpGet]
        public ActionResult HasReadTranslationList()
        {
            PageTranslations pageTranslations = new PageTranslations();
            pageTranslations.Intentions = BaseService.GetIntentions();
            return View(pageTranslations);
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
