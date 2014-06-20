using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Business.Core;
using Business.Serives;
using Business.Utils.Info;

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
        public ActionResult IntentionEdit(long id, string description)
        {
            if (id != 0 && !string.IsNullOrEmpty(description))
            {
                Intention intention = BaseService.GetIntentionById(id);
                intention.Description = description;
                BaseService.SaveIntention(intention);
            }
            return Json(InfoTools.GetMsgInfo("0"));
        }

        [HttpGet]
        public ActionResult IntentionDelete(long id)
        {
            BaseService.DeleteIntention(id);
            return RedirectToAction("IntentionList");
        }
    }
}
