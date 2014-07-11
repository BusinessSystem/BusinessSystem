using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
                ManageService.SaveLoginRecord(LoginRecordFactory.Create(userName, GetIp()));
                CookieHelper.SaveManagerCookie(manager, remember);
            }
            return Json(InfoTools.GetMsgInfo(result));
        }

        [HttpGet]
        public ActionResult LoginOut()
        {
            CookieHelper.ClearLoginCookie();
            return Redirect("/html/index.html");
        }

        private string GetIp()
        {
            string ip = string.Empty;
            if (!string.IsNullOrEmpty(System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"]))
                ip = Convert.ToString(System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]);
            if (string.IsNullOrEmpty(ip))
                ip = Convert.ToString(System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
            return ip;
        }

    }
}
