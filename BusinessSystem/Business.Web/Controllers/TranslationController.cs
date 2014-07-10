using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
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
        public ActionResult EmailTranslationAdd(string emailTheme, string translationContent, long originalLanguage,
            long targetLanguage)
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
                EmailTranslation emailTranslation = EmailTranslationFactory.Create(superManager.Id,
                    SystemDictionary.GetInstance.GetBaseDictionary(selfManager.Language).Value, selfManager.RealName,
                    CurrentManager.Id,
                    emailTheme,
                    translationContent, filePath);
                EmailFollow emailFollow = EmailFollowFactory.Create(0, translationContent,
                    SystemDictionary.GetInstance.GetBaseDictionary(targetLanguage).Value,filePath);
                string result = TranslationService.SaveTranslation(emailTranslation, emailFollow);
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
            int pageIndex = 1;
            int pageSize = 10;
            if (!string.IsNullOrEmpty(Request["pageIndex"]))
            {
                int.TryParse(Request["pageIndex"].ToString(), out pageIndex);
            }
            if (!string.IsNullOrEmpty(Request["pageSize"]))
            {
                int.TryParse(Request["pageSize"].ToString(), out pageSize);
            }
            long intentionId = 0;
            if (!string.IsNullOrEmpty(Request["intentionId"]))
            {
                long.TryParse(Request["intentionId"].ToString(), out intentionId);
            }
            ViewBag.IntentionId = intentionId;
            ViewBag.Intentions = BaseService.GetIntentions(CurrentManager.ParentId != 0 ? CurrentManager.ParentId : CurrentManager.Id);
            ViewBag.CurrentManager = CurrentManager;
            ViewBag.ChildManagers = ManageService.GetChildManagers(CurrentManager.Id);
            return
                View(TranslationService.GetEmailTranslations(CurrentManager.ManagerType, EmailStatusEnum.HasRead,
                    Utils.CoreDefaultValue.False,
                    CurrentManager.Id, intentionId, pageIndex, pageSize));
        }

        [HttpGet]
        public ActionResult UnReadTranslationList()
        {
            int pageIndex = 1;
            int pageSize = 10;
            if (!string.IsNullOrEmpty(Request["pageIndex"]))
            {
                int.TryParse(Request["pageIndex"].ToString(), out pageIndex);
            }
            if (!string.IsNullOrEmpty(Request["pageSize"]))
            {
                int.TryParse(Request["pageSize"].ToString(), out pageSize);
            }
            long intentionId = 0;
            if (!string.IsNullOrEmpty(Request["intentionId"]))
            {
                long.TryParse(Request["intentionId"].ToString(), out intentionId);
            }
            ViewBag.IntentionId = intentionId;
            ViewBag.Intentions = BaseService.GetIntentions(CurrentManager.ParentId != 0 ? CurrentManager.ParentId : CurrentManager.Id);
            ViewBag.CurrentManager = CurrentManager;
            ViewBag.ChildManagers = ManageService.GetChildManagers(CurrentManager.Id);
            return
                View(TranslationService.GetEmailTranslations(CurrentManager.ManagerType, EmailStatusEnum.UnRead,
                    Utils.CoreDefaultValue.False,
                    CurrentManager.Id, intentionId, pageIndex, pageSize));
        }

        [HttpGet]
        public ActionResult RecycledTranslationList()
        {
            int pageIndex = 1;
            int pageSize = 10;
            ViewBag.CurrentManager = CurrentManager;
            if (!string.IsNullOrEmpty(Request["pageIndex"]))
            {
                int.TryParse(Request["pageIndex"].ToString(), out pageIndex);
            }
            if (!string.IsNullOrEmpty(Request["pageSize"]))
            {
                int.TryParse(Request["pageSize"].ToString(), out pageSize);
            }

            return View(TranslationService.GetRecycledTranslationList(CurrentManager.ManagerType,
                CurrentManager.Id, pageIndex, pageSize));
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
            return Json(InfoTools.GetMsgInfo(ResponseCode.Ok), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult BackToMainAccount(string translationIds)
        {
            if (!string.IsNullOrEmpty(translationIds))
            {
                string[] translationArray = translationIds.Split(',');
                foreach (var translationId in translationArray)
                {
                    TranslationService.BackTranslationToMain(long.Parse(translationId), CurrentManager);
                }
            }
            return Json(InfoTools.GetMsgInfo(ResponseCode.Ok), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult TranslationDelete(string translationIds)
        {
            if (!string.IsNullOrEmpty(translationIds))
            {
                string[] translationArray = translationIds.Split(',');
                foreach (var translationId in translationArray)
                {
                    TranslationService.DeleteTranslation(long.Parse(translationId), CurrentManager.Id);
                }
            }
            return Json(InfoTools.GetMsgInfo(ResponseCode.Ok), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult EmailTranslationDetail(long id)
        {
            ViewBag.CurrentManager = CurrentManager;
            ViewBag.Intentions = BaseService.GetIntentions(CurrentManager.ParentId != 0 ? CurrentManager.ParentId : CurrentManager.Id);
            EmailTranslation emailTranslation = TranslationService.GeEmailTranslationById(id);
            PageTranslationFollow translationFollow =
                new PageTranslationFollow(emailTranslation,
                    TranslationService.GetEmailFollows(id));
            TranslationService.ChangeEmailTranslationStatus(emailTranslation);
            return View(translationFollow);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult TranslationReply(string translationContent)
        {
            string filePath = string.Empty;
            if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
            {
                HttpPostedFileBase file = Request.Files[0];
            }
            long replyfollowId = 0;
            if (!string.IsNullOrEmpty(Request["replyfollowId"]))
            {
                long.TryParse(Request["replyfollowId"].ToString(), out replyfollowId);
            }
            long translationId = 0;
            if (!string.IsNullOrEmpty(Request["emailTranslationId"]))
            {
                long.TryParse(Request["emailTranslationId"].ToString(), out translationId);
            }
            string content = translationContent;


            if (replyfollowId == 0)
            {
                TranslationService.CreateEmailFollow(translationId, content, filePath);
            }
            else
            {
                TranslationService.ReplyTranslation(replyfollowId, translationId, content, CurrentManager,filePath);
            }
            return Redirect("/Translation/EmailTranslationDetail/" + translationId);
        }
       
        [HttpPost]
        public ActionResult IssueToChildManager(string translationIds,long managerId)
        {
            if (!string.IsNullOrEmpty(translationIds) && managerId != 0)
            {
                string[] translationArrays = translationIds.Split(',');
                foreach (var translationId in translationArrays)
                {
                    TranslationService.IssueEmailToChildManager(long.Parse(translationId), managerId);
                }
            }
            return Json(InfoTools.GetMsgInfo(ResponseCode.Ok));
        }

        [HttpPost]
        public ActionResult RecoveryEmailTranlations(string translationIds)
        {
            if (!string.IsNullOrEmpty(translationIds))
            {
                string[] translationArrays = translationIds.Split(',');
                foreach (var translationId in translationArrays)
                {
                    TranslationService.RecoveryEmailTranlations(long.Parse(translationId));
                }
            }
            return Json(InfoTools.GetMsgInfo(ResponseCode.Ok));
        }
    }
}
