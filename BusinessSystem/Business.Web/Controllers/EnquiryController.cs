using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Business.Core;
using Business.Serives;
using Business.Web.PageModel;

namespace Business.Web.Controllers
{
    public class EnquiryController : AdminBaseController
    {
        //
        // GET: /Enquiry/

        [HttpGet]
        public ActionResult HasReadEnquiryList()
        {
            PageModel<Enquiry> pageModel = EnquiryService.GetEnquiryPages(CurrentManager.Id, 0, 0,
                HandlerStatusEnum.HasRead, 0, 10);
            ViewBag.CurrentManager = CurrentManager;
            ViewBag.Intentions = BaseService.GetIntentions(CurrentManager.ParentId != 0 ? CurrentManager.ParentId : CurrentManager.Id);
            ViewBag.UserDefineds = BaseService.GetUserDefineds(CurrentManager.ParentId != 0 ? CurrentManager.ParentId : CurrentManager.Id);
            ViewBag.ChildManagers = ManageService.GetChildManagers(CurrentManager.Id);
            return View(pageModel);
        }

        [HttpGet]
        public ActionResult UnReadEnquiryList()
        {
            PageModel<Enquiry> pageModel = EnquiryService.GetEnquiryPages(CurrentManager.Id, 0, 0,
                HandlerStatusEnum.UnRead, 1, 10);
            ViewBag.CurrentManager = CurrentManager;
            ViewBag.Intentions = BaseService.GetIntentions(CurrentManager.ParentId != 0 ? CurrentManager.ParentId : CurrentManager.Id);
            ViewBag.UserDefineds = BaseService.GetUserDefineds(CurrentManager.ParentId != 0 ? CurrentManager.ParentId : CurrentManager.Id);
            ViewBag.ChildManagers = ManageService.GetChildManagers(CurrentManager.Id);
            return View(pageModel);
        }

        [HttpGet]
         public ActionResult RecycledEnquiryList()
         {
             PageModel<Enquiry> pageModel = EnquiryService.GetEnquiryPages(CurrentManager.Id, 0, 0,
               HandlerStatusEnum.UnRead, 0, 10);
             ViewBag.CurrentManager = CurrentManager;
             ViewBag.Intentions = BaseService.GetIntentions(CurrentManager.ParentId != 0 ? CurrentManager.ParentId : CurrentManager.Id);
             ViewBag.UserDefineds = BaseService.GetUserDefineds(CurrentManager.ParentId != 0 ? CurrentManager.ParentId : CurrentManager.Id);
             ViewBag.ChildManagers = ManageService.GetChildManagers(CurrentManager.Id);
             return View(pageModel);
         }

         [HttpGet]
         public ActionResult UnReadEnquiryEmailList()
         {
             PageModel<Enquiry> pageModel = EnquiryService.GetEnquiryPages(CurrentManager.Id, 0, 0,
                HandlerStatusEnum.UnRead, 0, 10);
             ViewBag.CurrentManager = CurrentManager;
             ViewBag.Intentions = BaseService.GetIntentions(CurrentManager.ParentId != 0 ? CurrentManager.ParentId : CurrentManager.Id);
             ViewBag.UserDefineds = BaseService.GetUserDefineds(CurrentManager.ParentId != 0 ? CurrentManager.ParentId : CurrentManager.Id);
             ViewBag.ChildManagers = ManageService.GetChildManagers(CurrentManager.Id);
             return View(pageModel);
         }

        [HttpGet]
        public ActionResult EnquiryDetail(long id)
        {
            Enquiry enquiry = EnquiryService.GetEnquiryById(id);
            EnquiryService.ChangeEnquiryStatus(CurrentManager,enquiry);
            PageEnquiry pageEnquiry = new PageEnquiry();
            pageEnquiry.Enquiry = enquiry;
            return View(pageEnquiry);
        }
    }
}
