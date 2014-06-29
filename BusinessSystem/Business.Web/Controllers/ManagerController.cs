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
        public ActionResult ManagerEdit(long id,string userName,  string realName,
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
            long parentId = CurrentManager.ParentId;
            if (CurrentManager.ParentId == 0)
            {
                parentId = CurrentManager.Id;
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
            string ipString = GetIP();
            ManageService.ResetPassword(id,CurrentManager,ipString);
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
            string ipString = GetIP();
            string responseCode = ManageService.ChangePassword(CurrentManager, oldPassword, newPassword, confirmPassword, ipString);
            CurrentManager.Password = string.Empty;
            CookieHelper.SaveManagerCookie(CurrentManager);
            return Json(InfoTools.GetMsgInfo(responseCode));
        }

        [HttpGet]
        public ActionResult PwdChangedRecordList()
        {
            int pageIndex = 1;
            int pageSize = 50;
            if (!string.IsNullOrEmpty(Request["pageIndex"]))
            {
                int.TryParse(Request["pageIndex"].ToString(), out pageIndex);
            }
            if (!string.IsNullOrEmpty(Request["pageSize"]))
            {
                int.TryParse(Request["pageSize"].ToString(), out pageSize);
            }
            return View(ManageService.GetPwdChangeRecords(CurrentManager.Id, pageIndex, pageSize));
        }

        [HttpGet]
        public ActionResult ManagerProductAdd(long id)
        {
            ManagerMainSite managerMainSite = ManageService.GetManagerMainSiteById(id);
            return View(managerMainSite);
        }

        [HttpPost]
        public ActionResult ManagerProductAdd(long mainSiteId, string productUrl, string productName,
            string productDescription)
        {
            string result = ManageService.ManagerProductAdd(mainSiteId, productUrl, productName, productDescription,
                CurrentManager.RealName);
            return Json(InfoTools.GetMsgInfo(result), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ManagerProductEdit(long id)
        {
            ViewBag.BaseDictionarys = BaseService.GetBaseDictionaries(ValueTypeEnum.Language);
            ViewBag.CommonManagers = ManageService.GetMmanagerTypeManagers(ManagerTypeEnum.Common);
            ManagerProduct managerProduct = ManageService.GetManagerProductById(id);
            return View(managerProduct);
        }
        [HttpPost]
        public ActionResult ManagerProductEdit(long managerproductId, long language, string productUrl, long managerId)
        {
            string result = ManageService.ManagerProductUpdate(managerproductId, managerId, productUrl, language,
                CurrentManager.RealName);
            return Json(InfoTools.GetMsgInfo(result), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ManagerProductDelete(long id)
        {
            ManageService.ManagerProductDelete(id);
            return Json(InfoTools.GetMsgInfo(ResponseCode.Ok), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ManagerProductList()
        {
            long mainSiteId = 0;
            if (!string.IsNullOrEmpty(Request["mainSiteId"]))
            {
                long.TryParse(Request["mainSiteId"].ToString(), out mainSiteId);
            }
            if (mainSiteId == 0)
            {
                return null;
            }
            ViewBag.managerMainSiteId = mainSiteId;
            int pageIndex = 1;
            int pageSize = 10;
            if (!string.IsNullOrEmpty(Request["pageIndex"]))
            {
                int.TryParse(Request["pageIndex"].ToString(), out pageIndex);
            }
            if (!string.IsNullOrEmpty(Request["pageSize"]))
            {
                int.TryParse(Request["pageSize"].ToString(), out pageSize);
            }
            PageModel<ManagerProduct> managerProducts = ManageService.GetManagerProducts(mainSiteId,pageIndex, pageSize);
            return View(managerProducts);
        }


        [HttpGet]
        public ActionResult ManagerMainSiteAdd()
        {
            IList<BaseDictionary> baseDictionaries = BaseService.GetBaseDictionaries(ValueTypeEnum.Language);
            ViewBag.CommonManagers = ManageService.GetMmanagerTypeManagers(ManagerTypeEnum.Common);
            return View(baseDictionaries);
        }

        [HttpGet]
        public ActionResult ManagerMainSiteEdit(long id)
        {
            ViewBag.BaseDictionaries = BaseService.GetBaseDictionaries(ValueTypeEnum.Language);
            ViewBag.CommonManagers = ManageService.GetMmanagerTypeManagers(ManagerTypeEnum.Common);
             ManagerMainSite managerMainSite = ManageService.GetManagerMainSiteById(id);
             return View(managerMainSite);
        }

        [HttpPost]
        public ActionResult ManagerMainSiteEdit(long mainSiteId, string siteName, long language, string siteUrl,
            long managerId)
        {
            ManagerMainSite managerMainSite = ManageService.GetManagerMainSiteById(mainSiteId);
            if (managerMainSite != null)
            {
                managerMainSite.SiteName = siteName;
                managerMainSite.SiteUrl = siteUrl;
                BaseDictionary baseDictionary = BaseService.GetDictionaryById(language);
                Manager manager = ManageService.GetManagerById(managerId);
                if (baseDictionary == null || manager == null)
                {
                    return Json(InfoTools.GetMsgInfo(ResponseCode.NotFoundData));
                }
                managerMainSite.LanguageId = language;
                managerMainSite.LanguageName = baseDictionary.Value;
                managerMainSite.ManagerName = manager.RealName;
                managerMainSite.ManagerId = managerId;
            }
            return Json(InfoTools.GetMsgInfo(ResponseCode.NotFoundData));
        }

        [HttpPost]
        public ActionResult ManagerMainSiteAdd(string siteName, long language, string siteUrl, long managerId)
        {
            Manager manager = ManageService.GetManagerById(managerId);
            BaseDictionary baseDictionary = BaseService.GetDictionaryById(language);
            if (manager != null && baseDictionary != null)
            {
                ManagerMainSite managerMainSite = ManagerMainSiteFactory.Create(manager.Id, manager.RealName, siteName,
                    siteUrl, baseDictionary.Id, baseDictionary.Value,CurrentManager.RealName);
                string result = ManageService.MainSiteSave(managerMainSite);
                return Json(InfoTools.GetMsgInfo(result));
            }
            return Json(InfoTools.GetMsgInfo(ResponseCode.Ok));
        }

        [HttpGet]
        public ActionResult ManagerMainSiteList()
        {
            int pageIndex = 1;
            int pageSize = 50;
            if (!string.IsNullOrEmpty(Request["pageIndex"]))
            {
                int.TryParse(Request["pageIndex"].ToString(), out pageIndex);
            }
            if (!string.IsNullOrEmpty(Request["pageSize"]))
            {
                int.TryParse(Request["pageSize"].ToString(), out pageSize);
            }
            long managerId = 0;
            if (!string.IsNullOrEmpty(Request["managerId"]))
            {
                long.TryParse(Request["managerId"].ToString(), out managerId);
            }
            long language = 0;
            if (!string.IsNullOrEmpty(Request["language"]))
            {
                long.TryParse(Request["language"].ToString(), out language);
            }
            string siteUrl = Request["siteUrl"];
            ViewBag.CurrentManagerId = managerId;
            ViewBag.CurrentLanguageId = language;
            ViewBag.BaseDictionarys = BaseService.GetBaseDictionaries(ValueTypeEnum.Language);
            ViewBag.CommonManagers = ManageService.GetMmanagerTypeManagers(ManagerTypeEnum.Common);
            PageModel<ManagerMainSite> pageModel = ManageService.GetManagerMainSitePages(managerId, language, siteUrl,
                pageIndex, pageSize);
            return View(pageModel);
        }


        [HttpGet]
        public ActionResult CommonManagerMainSiteList()
        {
            IList<ManagerMainSite> managerMainSites =
                ManageService.GetManagerMainSitesByManagerId(CurrentManager.ParentId == 0
                    ? CurrentManager.Id
                    : CurrentManager.ParentId);
            return View(managerMainSites);
        }


        private string GetIP()
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
