using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Business.Core;
using Business.Nhibernate.IRepository;
using Business.Nhibernate.Repository;

namespace Business.Serives
{
    public class ManageService
    {
        private static readonly IManagerRepository managerRepository = new ManagerRepository();
        private static readonly IPwdChangeRecordRepository pwdChangeRecordRepository = new PwdChangeRecordRepository();
        private static readonly IManagerProductRepository managerProductRepository = new ManagerProductRepository();
        private static readonly IManagerMainSiteRepository managerMainSiteRepository = new ManagerMainSiteRepository();
        private static readonly ILoginRecordRepository loginRecordRepository = new LoginRecordRepository();
     
        public static PageModel<Manager> GetManagerPages(ManagerTypeEnum managerType, int pageIndex, int pageSize,
            long parentId)
        {
            int totalCount = 0;
            IList<Manager> managers = managerRepository.GetManagersByPage(managerType, pageIndex, pageSize, parentId,
                out totalCount);
            return new PageModel<Manager>(managers, pageIndex, pageSize, totalCount);
        }

        public static PageModel<Manager> GetManagerPages(int pageIndex, int pageSize,long parentId)
        {
            int totalCount = 0;
            IList<Manager> managers = managerRepository.GetManagersByPage(pageIndex, pageSize, parentId,
                out totalCount);
            return new PageModel<Manager>(managers, pageIndex, pageSize, totalCount);
        }

        public static Manager GetSuperManager()
        {
            return managerRepository.GetSuperManager();
        }

        public static Manager GetManagerById(long Id)
        {
            return managerRepository.GetById(Id);
        }
        public static Manager GetManagerByUsername(string userName)
        {
            return managerRepository.GetManagerByUserName(userName);
        }

        public static string Login(string userName, string password, out Manager resultManager)
        {
            Manager manager = managerRepository.GetManagerByUserName(userName);
            resultManager = manager;
            if (manager == null)
            {
                return ResponseCode.Managaer.UserNameError;
            }
            if (!manager.MatchPassword(password))
            {
                return ResponseCode.Managaer.UserPasswordError;
            }
            return ResponseCode.Ok;
        }

        //站体分析登录验证
        public static string LoginWebSiteAnalysis(string username, string pwd, out Manager resultmanager)
        {
            Manager manager = managerRepository.GetManagerByUserName(username);
            resultmanager = manager;
            if (manager == null)
            {
                return ResponseCode.Managaer.UserNameError;
            }
            if (!manager.MatchPassword(pwd))
            {
                return ResponseCode.Managaer.UserPasswordError;
            }
            if (manager.ManagerType == ManagerTypeEnum.Super)
            {
                return ResponseCode.Managaer.MangerNoPermission;
            }
            return ResponseCode.Ok;
        }
            
        public static string Save(Manager currentManager, Manager manager)
        {
            if (string.IsNullOrEmpty(manager.UserName))
            {
                return ResponseCode.Managaer.UserNullOrEmpty;
            }
            if (string.IsNullOrEmpty(manager.BindEmail))
            {
                return ResponseCode.Managaer.BindEmailIsNullOrEmpty;
            }
          
            if (string.IsNullOrEmpty(manager.Password))
            {
                return ResponseCode.Managaer.PasswordNullOrEmpty;
            }
            if (manager.Language == 0)
            {
                return ResponseCode.Managaer.LanguageNullOrEmpty;
            }
            if (string.IsNullOrEmpty(manager.RealName))
            {
                return ResponseCode.Managaer.RealNameNullOrEmpty;
            }
            if (string.IsNullOrEmpty(manager.Company))
            {
                return ResponseCode.Managaer.CompanyNullOrEmpty;
            }
            if (!Utils.CheckTools.IsValidEmail(manager.BindEmail))
            {
                return ResponseCode.Managaer.IsNotEmail;
            }
            if (currentManager.ManagerType == ManagerTypeEnum.Common && currentManager.ParentId != 0)
            {
                return ResponseCode.Managaer.ComonChildNoPermission;
            }
            if (currentManager.ManagerType == ManagerTypeEnum.Common && manager.ManagerType == ManagerTypeEnum.Super)
            {
                return ResponseCode.Managaer.CommonPermission;
            }
            if (currentManager.ManagerType == ManagerTypeEnum.Super && currentManager.ParentId != 0 &&
                manager.ManagerType == ManagerTypeEnum.Super)
            {
                return ResponseCode.Managaer.SuperChildNoPermission;
            }
            if (currentManager.ManagerType == ManagerTypeEnum.Super && manager.ManagerType == ManagerTypeEnum.Common &&
                manager.Id == 0)
            {
                manager.ParentId = 0;
            }
            if (managerRepository.IsExist(manager))
            {
                return ResponseCode.Managaer.UserNameHasExist;
            }
            
            managerRepository.Save(manager);
            return ResponseCode.Ok;
        }

        public static void DeleteManager(long id)
        {
            Manager manager = managerRepository.GetById(id);
            if (manager != null)
            {
                managerRepository.Delete(manager);
            }
        }

        public static void ResetPassword(long id, Manager currentManager, string ipString)
        {
            Manager manager = managerRepository.GetById(id);
            if (manager != null)
            {
                PwdChangeRecord pwdChangeRecord = PwdChangeRecordFactory.Create(id, manager.RealName, "重置密码",
                    "123456", ipString, currentManager.RealName);
                manager.Password = "123456";
                manager.EncryptPassword();
                managerRepository.Save(manager);
                pwdChangeRecordRepository.Save(pwdChangeRecord);
            }
        }

        public static string ChangePassword(Manager manager, string oldPassword, string newPassword,
            string confirmPassword, string ipstring)
        {
            if (string.IsNullOrEmpty(oldPassword))
            {
                return ResponseCode.Managaer.OldPasswordNullOrEmpty;
            }
            if (string.IsNullOrEmpty(newPassword))
            {
                return ResponseCode.Managaer.NewPasswordNullOrEmpty;
            }
            if (newPassword != confirmPassword)
            {
                return ResponseCode.Managaer.ConfirmPasswordError;
            }
            Manager manage = managerRepository.GetById(manager.Id);
            if (manage != null)
            {
                if (!manage.MatchPassword(oldPassword))
                {
                    return ResponseCode.Managaer.OldPasswordError;
                }
                PwdChangeRecord pwdChangeRecord = PwdChangeRecordFactory.Create(manager.Id, manager.RealName,
                    oldPassword,
                    newPassword, ipstring, manage.RealName);
                manage.Password = newPassword;
                manage.EncryptPassword();
                managerRepository.Save(manage);
                pwdChangeRecordRepository.Save(pwdChangeRecord);
                manager = manage;
                return ResponseCode.Ok;
            }
            return ResponseCode.NotFoundData;
        }


        public static IList<Manager> GetChildManagers(long parentId)
        {
            return managerRepository.GetChildManagers(parentId);
        }
        
        public static IList<Manager> GetMmanagerTypeManagers(ManagerTypeEnum managerType)
        {
            return managerRepository.GetManagerTypeManagers(managerType);
        }
        public static IList<Manager> GetMainManagerTypeManagers(ManagerTypeEnum managerType)
        {
            return managerRepository.GetMainManagerTypeManagers(managerType);
        }
        public static IList<PwdChangeRecord> GetPwdChangeRecords(long managerId, int pageIndex, int pageSize)
        {
            return pwdChangeRecordRepository.GetPwdChangeRecords(managerId, pageIndex, pageSize);
        }

        public static string ManagerProductAdd(long mainSiteId, string productUrl, string productName,
            string productDescription, string operate)
        {
            if (mainSiteId == 0)
            {
                return ResponseCode.NotFoundData;
            }
            if (string.IsNullOrEmpty(productUrl))
            {
                return ResponseCode.NotFoundData;
            }
            if (string.IsNullOrEmpty(productName))
            {
                return ResponseCode.NotFoundData;
            }
            if (string.IsNullOrEmpty(operate))
            {
                return ResponseCode.NotFoundData;
            }
            //TODO:此处需要修改
            ManagerProduct managerProduct = ManagerProductFactory.Create(mainSiteId, productName, productUrl,
                productDescription, operate);
            managerProductRepository.Save(managerProduct);
            return ResponseCode.Ok;
        }

        [Obsolete]
        public static PageModel<ManagerProduct> GetManagerProducts(long languageId, long managerId, string product,
            int pageIndex, int pageSize)
        {
            int totalCount = 0;
            IList<ManagerProduct> managerProducts = managerProductRepository.GetManagerProducts(languageId, managerId,
                product,
                pageIndex, pageSize, out totalCount);
            return new PageModel<ManagerProduct>(managerProducts, pageIndex, pageSize, totalCount);
        }

        public static string ManagerProductUpdate( long managerproductId, long mainSiteId, string productUrl, string productName,string productDescription,string operate)
        {
            if (managerproductId == 0)
            {
                return ResponseCode.NotFoundData;
            }
            if (mainSiteId == 0)
            {
                return ResponseCode.NotFoundData;
            }
            if (string.IsNullOrEmpty(productUrl))
            {
                return ResponseCode.NotFoundData;
            }
            if (string.IsNullOrEmpty(productName))
            {
                return ResponseCode.NotFoundData;
            }
      
            ManagerProduct managerProduct = managerProductRepository.GetById(managerproductId);
            if (managerProduct == null)
            {
                return ResponseCode.NotFoundData;
            }
            if (managerProduct.ManagerMainSiteId != mainSiteId)
            {
                 return ResponseCode.NotFoundData;
            }
            managerProduct.ProductName= productName;
            managerProduct.ProductName = productName;
            managerProduct.ProductDescription = productDescription;
            managerProduct.Operator = operate;
            managerProduct.OperateTime = DateTime.Now;
            managerProductRepository.Save(managerProduct);
            return ResponseCode.Ok;
        }


        public static void ManagerProductDelete(long id)
        {
            ManagerProduct managerProduct = managerProductRepository.GetById(id);
            if (managerProduct != null)
            {
                managerProductRepository.Delete(managerProduct);
            }
        }


        public static ManagerProduct GetManagerProductById(long id)
        {
            return managerProductRepository.GetById(id);
        }

        //TODO: 提示消息不完整
        public static string MainSiteSave(ManagerMainSite mainSite)
        {
            if (string.IsNullOrEmpty(mainSite.SiteUrl))
            {
                return ResponseCode.NotFoundData;
            }
            if (string.IsNullOrEmpty(mainSite.SiteName))
            {
                return ResponseCode.NotFoundData;
            }
            managerMainSiteRepository.Save(mainSite);
            return ResponseCode.Ok;
        }

 
        public static ManagerMainSite GetManagerMainSiteById(long id)
        {
            return managerMainSiteRepository.GetById(id);
        }

        public static IList<ManagerMainSite> GetManagerMainSitesByManagerId(long managerId)
        {
            return managerMainSiteRepository.GetManagerMainSitesByManagerId(managerId);
        }

        public static void MainSiteDelete(long id)
        {
            ManagerMainSite managerMainSite = managerMainSiteRepository.GetById(id);
            if (managerMainSite != null)
            {
                managerMainSiteRepository.Delete(managerMainSite);
            }
        }

        public static PageModel<ManagerMainSite> GetManagerMainSitePages(long managerId, long languageId, string siteUrl,
            int pageIndex, int pageSize)
        {
            int totalCount = 0;
            IList<ManagerMainSite> managerMainSites = managerMainSiteRepository.GetManagerMainSitePages(
                managerId, languageId, siteUrl, pageIndex, pageSize, out totalCount);
            return new PageModel<ManagerMainSite>(managerMainSites, pageIndex, pageSize, totalCount);
        }

        public static PageModel<ManagerProduct> GetManagerProducts(long mainSiteId, int pageIndex, int pageSize)
        {
            int totalCount = 0;
            IList<ManagerProduct> managerProducts = managerProductRepository.GetManagerProducts(mainSiteId, pageIndex,
                pageSize, out totalCount);
            return new PageModel<ManagerProduct>(managerProducts, pageIndex, pageSize, totalCount);
        }

        public static void SaveLoginRecord(LoginRecord loginRecord)
        {
            loginRecordRepository.Save(loginRecord);
        }

        public static PageModel<LoginRecord> GetLoginRecords(string loginUser, int pageIndex, int pageSize)
        {
            int totalCount = 0;
            IList<LoginRecord> loginRecords = loginRecordRepository.GetLoginRecords(loginUser, pageIndex, pageSize,
                out totalCount);
            return new PageModel<LoginRecord>(loginRecords,pageIndex,pageSize,totalCount);
        } 

    }
}
