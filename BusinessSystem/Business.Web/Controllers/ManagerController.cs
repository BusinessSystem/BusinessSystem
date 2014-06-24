using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Business.Core;
using Business.Serives;
using Business.Utils;
using Business.Utils.Info;
using Business.Web.Framework;
using Business.Web.PageModel;

namespace Business.Web.Controllers
{
    public class ManagerController :AdminBaseController
    {

        [HttpGet]
        public ActionResult ManagerList()
        {
            int pageIndex = 1;
            int pageSize = 10;
            if (!string.IsNullOrEmpty(Request["pageIndex"]))
            {
                 int.TryParse(Request["pageIndex"].ToString(),out pageIndex);
            }

            if (!string.IsNullOrEmpty(Request["pageSize"]))
            {
                int.TryParse(Request["pageSize"].ToString(), out pageSize);
            }

            ViewBag.ManagerTypes = EnumTools.GetEnumDescriptions<ManagerTypeEnum>();
            ManagerTypeEnum managerType = CurrentManager.ManagerType;
            if (!string.IsNullOrEmpty(Request["managerType"]))
            {
                managerType = (ManagerTypeEnum) short.Parse(Request["managerType"].ToString());
            }
            ViewBag.CurrentManagerType = managerType;
            PageModel<Manager> pageModel = ManageService.GetManagerPages(managerType, pageIndex, pageSize, CurrentManager.ParentId);
            return View(pageModel);
        }




        [HttpGet]
        public ActionResult ManagerAdd()
        {
            PageManager pageManager = new PageManager();
            pageManager.BaseDictionaries = BaseService.GetBaseDictionaries(ValueTypeEnum.Language);
            pageManager.ManagerTypes = EnumTools.GetEnumDescriptions<ManagerTypeEnum>();
            return View(pageManager);
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
            PageManager pageManager = new PageManager();
            pageManager.BaseDictionaries = BaseService.GetBaseDictionaries(ValueTypeEnum.Language);
            pageManager.ManagerTypes = EnumTools.GetEnumDescriptions<ManagerTypeEnum>();
            pageManager.ManagerBase = ManageService.GetManagerById(id);
            return View(pageManager);
        }

        [HttpPost]
        public ActionResult ManagerEdit(long id,string userName, string password, string realName,
            string company, int language,ManagerTypeEnum managerType)
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
                manager.ManagerType = managerType;
                manager.EncryptPassword();
                responseCode = ManageService.Save(CurrentManager,manager);
            }
            return Json(InfoTools.GetMsgInfo(responseCode), JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult ManagerAdd(string userName, string password, string realName,
            string company, int language, ManagerTypeEnum managerType)
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

            Manager manager = ManagerFactory.Create(userName, password, parentId, managerType, realName, company,
                isAutoDistribute, language, CurrentManager.UserName);
            manager.EncryptPassword();
            string responseCode = ManageService.Save(CurrentManager,manager);
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
