﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Business.Core;
using Business.Serives;
using Business.Utils.Info;

namespace Business.Web.Controllers
{
    public class ManagerController : Controller
    {
         
        [HttpGet]
        public ActionResult ManagerList()
        {
            IList<Manager> managers = new List<Manager>();
            for (int i = 0; i < 10; i++)
            {
               // managers.Add(ManagerFactory.Create("楚中刀客", "tianyalang007", "tianyalang007"));
            }
            return View(managers);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Manager manager)
        {
            return Json("JsonStr", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ManagerAdd()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ManagerAdd(string userName, string password, string realName,
            string company, int language)
        {
            short isAutoDistribute = Utils.CoreDefaultValue.False;
            if (!string.IsNullOrEmpty(Request["isAutoDistribute"]))
            {
                isAutoDistribute = Utils.CoreDefaultValue.True;
            }
            Manager manager = ManagerFactory.Create(userName, password, 0, ManagerTypeEnum.Super, realName, company,
                isAutoDistribute, language, "");
            string responseCode = ManageService.Save(manager);
            return Json(InfoTools.GetMsgInfo(responseCode), JsonRequestBehavior.AllowGet);
        }
    }
}
