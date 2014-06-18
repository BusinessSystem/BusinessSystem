using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Business.Core;
using Business.Serives;

namespace Business.Web.Controllers
{
    public class BaseController : Controller
    {
        //
        // GET: /Base/

        [HttpGet]
        public ActionResult IntentionList()
        {
            return View(BaseService.GetIntentions());
        }

        [HttpPost]
        public ActionResult IntentionAdd(string description)
        {
            if (!string.IsNullOrEmpty(description))
            {
                Intention intention = IntentionFactory.Create(description, 1234);
                BaseService.SaveIntention(intention);
            }
            return RedirectToAction("IntentionList");
        }

        [HttpPost]
        public ActionResult IntentionEdit()
        {
            return null;
        }
    }
}
