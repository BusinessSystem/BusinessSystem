using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Business.Web.Controllers
{
    public class ManagerController : Controller
    {
        //
        // GET: /Manager/
        [HttpGet]
        public ActionResult ManagerList()
        {
            return View();
        }

    }
}
