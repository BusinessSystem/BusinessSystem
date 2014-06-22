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
    public class ManagerController :AdminBaseController
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
        public ActionResult Index()
        {
            return Redirect("/Manager/ManagerDetail/" + CurrentManager.Id);
        }

        [HttpGet]
        public ActionResult ManagerDetail(long id)
        {
            return View(ManageService.GetManagerById(id));
        }

        [HttpGet]
        public ActionResult ManagerEdit(long id)
        {
            return View(ManageService.GetManagerById(id));
        }

        [HttpPost]
        public ActionResult ManagerEdit(long id,string userName, string password, string realName,
            string company, int language)
        {
            short isAutoDistribute = Utils.CoreDefaultValue.False;
            if (!string.IsNullOrEmpty(Request["isAutoDistribute"]))
            {
                isAutoDistribute = Utils.CoreDefaultValue.True;
            }
            string responseCode = ResponseCode.NotFoundData;
            Manager manager = ManageService.GetManagerById(id);
            if (manager != null)
            {
                manager.UserName = userName;
                manager.Password = password;
                manager.RealName = realName;
                manager.Company = company;
                manager.Language = language;
                manager.IsAutoDistribute = isAutoDistribute;
                manager.EncryptPassword();
                responseCode = ManageService.Save(manager);
            }
            return Json(InfoTools.GetMsgInfo(responseCode), JsonRequestBehavior.AllowGet);
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
            long parentId = 0;
            if (CurrentManager.ManagerType ==ManagerTypeEnum.Common&&CurrentManager.ParentId == 0)
            {
                parentId = CurrentManager.ParentId;
            }

            Manager manager = ManagerFactory.Create(userName, password, parentId, ManagerTypeEnum.Common, realName, company,
                isAutoDistribute, language, CurrentManager.UserName);
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

        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(string oldPassword, string newPassword, string confirmPassword)
        {
            string responseCode = ManageService.ChangePassword(CurrentManager, oldPassword, newPassword, confirmPassword);
            CurrentManager.Password = string.Empty;
            CookieHelper.SaveManagerCookie(CurrentManager);
            return Json(InfoTools.GetMsgInfo(responseCode));
        }
    }
}
