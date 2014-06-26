using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Business.Core;
using Business.Serives;

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
            ViewBag.Intentions = BaseService.GetIntentions();
            ViewBag.UserDefineds = BaseService.GetUserDefineds();
            ViewBag.ChildManagers = ManageService.GetChildManagers(CurrentManager.Id);
            return View(pageModel);
        }

         [HttpGet]
        public ActionResult UnReadEnquiryList()
        {
            return View();
        }

         [HttpGet]
         public ActionResult RecycledEnquiryList()
         {
             return View();
         }

         [HttpGet]
         public ActionResult UnReadEnquiryEmailList()
         {
             return View();
         }

        [HttpGet]
        public ActionResult EnquiryDetail()
        {
            return View();
        }
    }
}
