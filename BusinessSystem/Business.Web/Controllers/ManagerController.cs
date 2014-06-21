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
    public class ManagerController :Controller
    {
         
        [HttpGet]
        public ActionResult ManagerList()
        {
            IList<Manager> managers = ManageService.GetManagersByPage(ManagerTypeEnum.Super, 1, 10, 0);
            
            return View(managers);
        }

    
       

        [HttpGet]
        public ActionResult ManagerAdd()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ManagerDetail()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ManagerEdit(long id)
        {
            return View(ManageService.GetManagerById(id));
        }

        [HttpPost]
        public ActionResult ManagerEdit(string userName, string password, string realName,
            string company, int language)
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
            manager.EncryptPassword();
            string responseCode = ManageService.Save(manager);
            return Json(InfoTools.GetMsgInfo(responseCode), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ManagerDelete(long id)
        {
             ManageService.DeleteManager(id);
             return Json(InfoTools.GetMsgInfo(ResponseCode.Ok), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ResetPassword(long id)
        {
            ManageService.ResetPassword(id);
            return Json(InfoTools.GetMsgInfo(ResponseCode.Ok), JsonRequestBehavior.AllowGet);
        }
    }
}
