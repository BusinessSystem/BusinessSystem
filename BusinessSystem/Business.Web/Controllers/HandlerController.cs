using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Business.Core;
using Business.Serives;
using Business.Utils.Info;
using Business.Web.Framework;

namespace Business.Web.Controllers
{
    public class HandlerController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View("~/Views/Manager/Login.cshtml");
        }

        [HttpPost]
        public ActionResult Login(string userName, string password, bool remember)
        {
            Manager manager = null;
            string result = ManageService.Login(userName, password,out manager);
            if (result == ResponseCode.Ok)
            {
                CookieHelper.SaveManagerCookie(manager,remember);
            }
            return Json(InfoTools.GetMsgInfo(result));
        }


    }
}
