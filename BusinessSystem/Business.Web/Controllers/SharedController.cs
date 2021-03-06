﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Business.Core;
using Business.Serives;

namespace Business.Web.Controllers
{
    public class SharedController : AdminBaseController
    {
        
        [ChildActionOnly]
         
        public ActionResult _Navbar()
        {
            return PartialView(CurrentManager);
        }

        [ChildActionOnly]
        public ActionResult _NavList()
        {
            ViewBag.CurrentUrl = System.Web.HttpContext.Current.Request.RawUrl;
            int unReadTranslationCount = TranslationService.GetEmailTranslationsCount(CurrentManager.ManagerType,
                 EmailStatusEnum.UnRead, CurrentManager.Id);
            ViewBag.UnReadTranslationCount = unReadTranslationCount;

            int hasReadEmailCount = TranslationService.GetEmailTranslationsCount(CurrentManager.ManagerType,
                 EmailStatusEnum.HasRead, CurrentManager.Id);
            ViewBag.HasReadEmailCount = hasReadEmailCount;

            if (CurrentManager.ManagerType == ManagerTypeEnum.Super)
            {
                return PartialView("~/Views/Shared/_NavSuperList.cshtml", CurrentManager);
            }
            int unReadEnquiryCount = EnquiryService.GetReadEnquiryCount(CurrentManager.Id,HandlerStatusEnum.UnRead);
            ViewBag.UnReadEnquiryCount = unReadEnquiryCount;

            int hasReadEnquiryCount = EnquiryService.GetReadEnquiryCount(CurrentManager.Id, HandlerStatusEnum.HasRead);
            ViewBag.HasReadEnquiryCount = hasReadEnquiryCount;

            int unReadEmailEnquiryCount = EnquiryService.GetUnReadEmailEnquiryCount(CurrentManager.Id,
               EmailStatusEnum.UnRead);
            ViewBag.UnReadEmailEnquiryCount = unReadEmailEnquiryCount;
         
            return PartialView(CurrentManager);
        }
    }
}
