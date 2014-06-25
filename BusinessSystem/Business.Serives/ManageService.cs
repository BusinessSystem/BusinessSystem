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
        private static IManagerRepository managerRepository = new ManagerRepository();
        private static IPwdChangeRecordRepository pwdChangeRecordRepository = new PwdChangeRecordRepository();
        private static IManagerProductRepository managerProductRepository = new ManagerProductRepository();

        public static PageModel<Manager> GetManagerPages(ManagerTypeEnum managerType, int pageIndex, int pageSize,
            long parentId)
        {
            int totalCount = 0;
            IList<Manager> managers = managerRepository.GetManagersByPage(managerType, pageIndex, pageSize, parentId,
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

        public static string Save(Manager currentManager, Manager manager)
        {
            if (string.IsNullOrEmpty(manager.UserName))
            {
                return ResponseCode.Managaer.UserNullOrEmpty;
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

        public static IList<PwdChangeRecord> GetPwdChangeRecords(long managerId, int pageIndex, int pageSize)
        {
            return pwdChangeRecordRepository.GetPwdChangeRecords(managerId, pageIndex, pageSize);
        }

        public static string ManagerProductAdd(long managerId,string productUrl,long language,string operate)
        {
            if (managerId == 0)
            {
                return ResponseCode.NotFoundData;
            }
            if (string.IsNullOrEmpty(productUrl))
            {
                return ResponseCode.NotFoundData;
            }
            if (language==0)
            {
                return ResponseCode.NotFoundData;
            }
            if (string.IsNullOrEmpty(operate))
            {
                return ResponseCode.NotFoundData;
            }
            ManagerProduct managerProduct = ManagerProductFactory.Create(managerId, productUrl, language, operate);
            managerProductRepository.Save(managerProduct);
            return ResponseCode.Ok;
        }

        public static IList<ManagerProduct> GetManagerProducts(long languageId, long managerId, string product,
            int pageIndex, int pageSize)
        {
            return managerProductRepository.GetManagerProducts(languageId, managerId, product, pageIndex, pageSize);
        }

        public static string ManagerProductUpdate(long id,long managerId, string productUrl, long language, string operate)
        {
            if (managerId == 0)
            {
                return ResponseCode.NotFoundData;
            }
            if (string.IsNullOrEmpty(productUrl))
            {
                return ResponseCode.NotFoundData;
            }
            if ( language==0)
            {
                return ResponseCode.NotFoundData;
            }
            if (string.IsNullOrEmpty(operate))
            {
                return ResponseCode.NotFoundData;
            }
            ManagerProduct managerProduct = managerProductRepository.GetById(id);
            if (managerProduct == null)
            {
                return ResponseCode.NotFoundData;
            }
            managerProduct.ManagerId = managerId;
            managerProduct.Operator = operate;
            managerProduct.Language = language;
            managerProduct.Product = productUrl;
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
    }
}
