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
            Manager selfManager = ManageService.GetManagerById(CurrentManager.Id);
            if (superManager != null)
            {
                EmailTranslation emailTranslation = EmailTranslationFactory.Create(superManager.Id, SystemDictionary.GetInstance.GetBaseDictionary(selfManager.Language).Value, selfManager.RealName, CurrentManager.Id,
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
            pageTranslations.EmailTranslations = TranslationService.GetEmailTranslations(EmailStatusEnum.HasRead,CurrentManager.Id, 0, 1,
                10);
            pageTranslations.CurrentManager = CurrentManager;
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

        [HttpPost]
        public ActionResult MoveTransToIntention(string translationIds, long intentionId)
        {
            if (!string.IsNullOrEmpty(translationIds))
            {
                string[] translationArray = translationIds.Split(',');
                foreach (var translationId in translationArray)
                {
                    TranslationService.MoveToIntentionCustom(long.Parse(translationId), intentionId);
                }
            }
            return Json(InfoTools.GetMsgInfo(ResponseCode.Ok));
        }
    }
}
