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
                return Json(InfoTools.GetMsgInfo(ResponseCode.Ok));
            }
            return Json(InfoTools.GetMsgInfo(ResponseCode.DataError));
        }

        [HttpGet]
        public ActionResult IntentionDelete(long id)
        {
            BaseService.DeleteIntention(id);
            return Json(InfoTools.GetMsgInfo(ResponseCode.Ok));
        }


        [HttpGet]
        public ActionResult UserDefinedList()
        {
            return View(BaseService.GetUserDefineds());
        }

        [HttpPost]
        public ActionResult UserDefinedAdd(string description)
        {
            if (!string.IsNullOrEmpty(description))
            {
                UserDefined userDefined = UserDefinedFactory.Create(description, 1234);
                BaseService.SaveUserDefined(userDefined);
            }
            return RedirectToAction("UserDefinedList");
        }

        [HttpPost]
        public ActionResult UserDefinedEdit(long id, string description)
        {
            if (id != 0 && !string.IsNullOrEmpty(description))
            {
                UserDefined userDefined = BaseService.GetUserDefinedById(id);
                userDefined.Description = description;
                BaseService.SaveUserDefined(userDefined);
                return Json(InfoTools.GetMsgInfo(ResponseCode.Ok));
            }
            return Json(InfoTools.GetMsgInfo(ResponseCode.DataError));
        }

        [HttpPost]
        public ActionResult UserDefinedDelete(long id)
        {
            BaseService.DeleteUserDefined(id);
            return Json(InfoTools.GetMsgInfo(ResponseCode.Ok));
        }
    }
}
