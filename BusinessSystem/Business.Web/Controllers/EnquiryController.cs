using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Business.Web.Controllers
{
    public class EnquiryController : Controller
    {
        //
        // GET: /Enquiry/

        [HttpGet]
        public ActionResult HasReadEnquiryList()
        {
            return View();
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
    }
}
