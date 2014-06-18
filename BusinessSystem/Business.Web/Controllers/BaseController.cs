using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Business.Web.Controllers
{
    public class BaseController : Controller
    {
        //
        // GET: /Base/

        [HttpGet]
        public ActionResult IntentionList()
        {
            return View();
        }

        [HttpPost]
        public ActionResult IntentionAdd()
        {
            return null;
        }

        [HttpPost]
        public ActionResult IntentionEdit()
        {
            return null;
        }
    }
}
