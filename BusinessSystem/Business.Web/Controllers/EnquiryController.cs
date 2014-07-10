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
    public class EnquiryController : AdminBaseController
    {
        [HttpGet]
        public ActionResult ManagerEnquiryList()
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
            string email = string.Empty;
            if (!string.IsNullOrEmpty(Request["email"]))
            {
                email = Request["email"];
            }
            long languageId = 0;
            if (!string.IsNullOrEmpty(Request["languageId"]))
            {
                long.TryParse(Request["languageId"].ToString(), out languageId);
            }
            PageModel<Enquiry> pageModel = EnquiryService.GetEnquirysBySuperManager(email,  languageId, pageIndex, pageSize);
            ViewBag.CurrentManager = CurrentManager;
            ViewBag.Languages =
                ManageService.GetManagerMainSitesByManagerId(CurrentManager.ParentId == 0
                    ? CurrentManager.Id
                    : CurrentManager.ParentId);
            ViewBag.CurrentLanguageId = languageId;
            ViewBag.CurrentEmail = email;
            return View(pageModel);
        }
         

        [HttpGet]
        public ActionResult HasReadEnquiryList()
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
            long languageId = 0;
            if (!string.IsNullOrEmpty(Request["languageId"]))
            {
                long.TryParse(Request["languageId"].ToString(), out languageId);
            }
            long userDefinedId = 0;
            if (!string.IsNullOrEmpty(Request["userDefinedId"]))
            {
                long.TryParse(Request["userDefinedId"].ToString(), out userDefinedId);
            }
            string email = string.Empty;
            if (!string.IsNullOrEmpty(Request["email"]))
            {
                email = Request["email"];
            }
            PageModel<Enquiry> pageModel = EnquiryService.GetEnquiryPages(email,CurrentManager.Id, languageId, intentionId, userDefinedId,
                HandlerStatusEnum.HasRead, pageIndex, pageSize);
            ViewBag.CurrentManager = CurrentManager;
            ViewBag.Intentions = BaseService.GetIntentions(CurrentManager.ParentId != 0 ? CurrentManager.ParentId : CurrentManager.Id);
            ViewBag.UserDefineds = BaseService.GetUserDefineds(CurrentManager.ParentId != 0 ? CurrentManager.ParentId : CurrentManager.Id);
            ViewBag.ChildManagers = ManageService.GetChildManagers(CurrentManager.Id);
            ViewBag.Languages =
                ManageService.GetManagerMainSitesByManagerId(CurrentManager.ParentId == 0
                    ? CurrentManager.Id
                    : CurrentManager.ParentId);

            ViewBag.CurrentIntentionId = intentionId;
            ViewBag.CurrentUserdefinedId = userDefinedId;
            ViewBag.CurrentLanguageId = languageId;
            ViewBag.CurrentEmail = email;
            return View(pageModel);
        }

        [HttpGet]
        public ActionResult UnReadEnquiryList()
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
            long languageId = 0;
            if (!string.IsNullOrEmpty(Request["languageId"]))
            {
                long.TryParse(Request["languageId"].ToString(), out languageId);
            }
            long userDefinedId = 0;
            if (!string.IsNullOrEmpty(Request["userDefinedId"]))
            {
                long.TryParse(Request["userDefinedId"].ToString(), out userDefinedId);
            }
            string email = string.Empty;
            if (!string.IsNullOrEmpty(Request["email"]))
            {
                email = Request["email"];
            }
            PageModel<Enquiry> pageModel = EnquiryService.GetEnquiryPages(email,CurrentManager.Id, languageId,intentionId, userDefinedId,
                HandlerStatusEnum.UnRead, pageIndex, pageSize);
            ViewBag.CurrentManager = CurrentManager;
            ViewBag.Intentions = BaseService.GetIntentions(CurrentManager.ParentId != 0 ? CurrentManager.ParentId : CurrentManager.Id);
            ViewBag.UserDefineds = BaseService.GetUserDefineds(CurrentManager.ParentId != 0 ? CurrentManager.ParentId : CurrentManager.Id);
            ViewBag.ChildManagers = ManageService.GetChildManagers(CurrentManager.Id);
            ViewBag.Languages =
                ManageService.GetManagerMainSitesByManagerId(CurrentManager.ParentId == 0
                    ? CurrentManager.Id
                    : CurrentManager.ParentId);

            ViewBag.CurrentIntentionId = intentionId;
            ViewBag.CurrentUserdefinedId = userDefinedId;
            ViewBag.CurrentLanguageId = languageId;
            ViewBag.CurrentEmail = email;
            return View(pageModel);
        }

        [HttpGet]
         public ActionResult RecycledEnquiryList()
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
            long languageId = 0;
            if (!string.IsNullOrEmpty(Request["languageId"]))
            {
                long.TryParse(Request["languageId"].ToString(), out languageId);
            }
            long userDefinedId = 0;
            if (!string.IsNullOrEmpty(Request["userDefinedId"]))
            {
                long.TryParse(Request["userDefinedId"].ToString(), out userDefinedId);
            }

             PageModel<Enquiry> pageModel = EnquiryService.GetRecycledEnquiryPages(CurrentManager.Id,languageId,intentionId,userDefinedId,pageIndex,pageSize);
             ViewBag.CurrentManager = CurrentManager;
             ViewBag.Intentions = BaseService.GetIntentions(CurrentManager.ParentId != 0 ? CurrentManager.ParentId : CurrentManager.Id);
             ViewBag.UserDefineds = BaseService.GetUserDefineds(CurrentManager.ParentId != 0 ? CurrentManager.ParentId : CurrentManager.Id);
             ViewBag.ChildManagers = ManageService.GetChildManagers(CurrentManager.Id);
             return View(pageModel);
         }

         [HttpGet]
         public ActionResult UnReadEnquiryEmailList()
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
             long languageId = 0;
             if (!string.IsNullOrEmpty(Request["languageId"]))
             {
                 long.TryParse(Request["languageId"].ToString(), out languageId);
             }
             long userDefinedId = 0;
             if (!string.IsNullOrEmpty(Request["userDefinedId"]))
             {
                 long.TryParse(Request["userDefinedId"].ToString(), out userDefinedId);
             }
             PageModel<Enquiry> pageModel = EnquiryService.GetEnquiryUnReadEmailPages(CurrentManager.Id, languageId, intentionId, userDefinedId,
                 EmailStatusEnum.UnRead, pageIndex, pageSize);
             ViewBag.CurrentManager = CurrentManager;
             ViewBag.Intentions = BaseService.GetIntentions(CurrentManager.ParentId != 0 ? CurrentManager.ParentId : CurrentManager.Id);
             ViewBag.UserDefineds = BaseService.GetUserDefineds(CurrentManager.ParentId != 0 ? CurrentManager.ParentId : CurrentManager.Id);
             ViewBag.ChildManagers = ManageService.GetChildManagers(CurrentManager.Id);
             ViewBag.Languages =
               ManageService.GetManagerMainSitesByManagerId(CurrentManager.ParentId == 0
                   ? CurrentManager.Id
                   : CurrentManager.ParentId);
             ViewBag.CurrentIntentionId = intentionId;
             ViewBag.CurrentUserdefinedId = userDefinedId;
             ViewBag.CurrentLanguageId = languageId;
             return View(pageModel);
         }

        [HttpGet]
        public ActionResult EnquiryDetail(long id)
        {
            Enquiry enquiry = EnquiryService.GetEnquiryById(id);
            EnquiryService.ChangeEnquiryStatus(CurrentManager,enquiry);
            EmailTranslation emailTranslation = TranslationService.GeEmailTranslationByEnquiryId(id);
            PageEnquiry pageEnquiry = new PageEnquiry();
            pageEnquiry.Enquiry = enquiry;
            pageEnquiry.EmailTranslation = emailTranslation;
            if (emailTranslation != null)
            {
                pageEnquiry.EnquiryTransFollows = TranslationService.GetEmailFollows(emailTranslation.Id);
            }
            ViewBag.CurrentManager = CurrentManager;
            ViewBag.Intentions = BaseService.GetIntentions(CurrentManager.ParentId != 0 ? CurrentManager.ParentId : CurrentManager.Id);
            ViewBag.UserDefineds = BaseService.GetUserDefineds(CurrentManager.ParentId != 0 ? CurrentManager.ParentId : CurrentManager.Id);
            return View(pageEnquiry);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SendEnquiryEmail(long enquiryId, string emailContent, long emailTranslationId)
        {
            string filePath = string.Empty;
            if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
            {
                HttpPostedFileBase file = Request.Files[0];
            }
            if (enquiryId != 0)
            {

                if (emailTranslationId != 0)
                {
                    TranslationService.CreateEmailFollow(emailTranslationId, emailContent, filePath);
                }
                else
                {
                    Manager superManager = ManageService.GetSuperManager();
                    Enquiry enquiry = EnquiryService.GetEnquiryById(enquiryId);
                    EmailTranslation emailTranslation = EmailTranslationFactory.Create(superManager.Id,
                        enquiry.VisitLanguage, CurrentManager.RealName, CurrentManager.Id, "用户询盘", emailContent,
                        filePath);
                    emailTranslation.EnquiryId = enquiryId;
                    EmailFollow emailFollow = EmailFollowFactory.Create(0, emailContent, enquiry.VisitLanguage,
                        enquiry.VisitLanguage,filePath);
                    string result = TranslationService.SaveTranslation(emailTranslation, emailFollow);
                }
            }
            return Redirect("/Enquiry/EnquiryDetail/" + enquiryId);
        }

        [HttpPost]
        public ActionResult MoveEnquiryToIntention(string enquiryIds, long intentionId)
        {
            if (!string.IsNullOrEmpty(enquiryIds))
            {
                string[] enquiryIdArray = enquiryIds.Split(',');
                foreach (var enquiryId in enquiryIdArray)
                {
                    EnquiryService.MoveEnquiryToIntention(long.Parse(enquiryId), intentionId);
                }
            }
            return Json(InfoTools.GetMsgInfo(ResponseCode.Ok));
        }

        [HttpPost]
        public ActionResult MoveEnquiryToUserDefined(string enquiryIds, long userDefinedId)
        {
            if (!string.IsNullOrEmpty(enquiryIds))
            {
                string[] enquiryIdArray = enquiryIds.Split(',');
                foreach (var enquiryId in enquiryIdArray)
                {
                    EnquiryService.MoveEnquiryToUseDefine(long.Parse(enquiryId), userDefinedId);
                }
            }
            return Json(InfoTools.GetMsgInfo(ResponseCode.Ok));
        }

        [HttpPost]
        public ActionResult IssueEnquiryToChildManager(string enquiryIds, long managerId)
        {
            if (!string.IsNullOrEmpty(enquiryIds))
            {
                string[] enquiryIdArray = enquiryIds.Split(',');
                foreach (var enquiryId in enquiryIdArray)
                {
                    EnquiryService.IssueEnquiryToChild(managerId, long.Parse(enquiryId));
                }
            }
            return Json(InfoTools.GetMsgInfo(ResponseCode.Ok));
        }

        [HttpPost]
        public ActionResult IssueEnquiryToParentManager(string enquiryIds)
        {
            if (!string.IsNullOrEmpty(enquiryIds))
            {
                long managerId = CurrentManager.ParentId;
                if (managerId == 0)
                {
                    managerId = CurrentManager.Id;
                }
                string[] enquiryIdArray = enquiryIds.Split(',');
                foreach (var enquiryId in enquiryIdArray)
                {
                    EnquiryService.IssueEnquiryToChild(managerId, long.Parse(enquiryId));
                }
            }
            return Json(InfoTools.GetMsgInfo(ResponseCode.Ok),JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult EnquiryDelete(string enquiryIds)
        {
            if (!string.IsNullOrEmpty(enquiryIds))
            {
                string[] enquiryIdArray = enquiryIds.Split(',');
                foreach (var enquiryId in enquiryIdArray)
                {
                    EnquiryService.DeleteEnquiryById(long.Parse(enquiryId),CurrentManager);
                }
            }
            return Json(InfoTools.GetMsgInfo(ResponseCode.Ok), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult RecoveryEnquiry(string enquiryIds)
        {
            if (!string.IsNullOrEmpty(enquiryIds))
            {
                string[] enquiryIdArray = enquiryIds.Split(',');
                foreach (var enquiryId in enquiryIdArray)
                {
                    EnquiryService.RecoveryEnquiry(long.Parse(enquiryId), CurrentManager);
                }
            }
            return Json(InfoTools.GetMsgInfo(ResponseCode.Ok), JsonRequestBehavior.AllowGet);
        }

    }
}
