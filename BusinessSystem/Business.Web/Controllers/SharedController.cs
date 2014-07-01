using System;
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


            if (CurrentManager.ManagerType == ManagerTypeEnum.Super)
            {
                return PartialView("~/Views/Shared/_NavSuperList.cshtml");
            }
            int unReadEnquiryCount = EnquiryService.GetReadEnquiryCount(CurrentManager.Id, HandlerStatusEnum.UnRead);
            ViewBag.UnReadEnquiryCount = unReadEnquiryCount;
            return PartialView();
        }
    }
}
