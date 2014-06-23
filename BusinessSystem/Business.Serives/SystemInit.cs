using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using Business.Core;
using Business.Nhibernate.IRepository;
using Business.Nhibernate.Repository;

namespace Business.Serives
{
    /// <summary>
    ///系统随程序启动程序 
    /// </summary>
    public class SystemApplication
    {
        public static void Start()
        {
            CreateSystemAdmin();
            LoadSysDictionary();
        }

        /// <summary>
        /// 加载字典常驻内存(大大减少不必要的读库操作)
        /// </summary>
        private static void LoadSysDictionary()
        {
            IList<BaseDictionary> allDictionaries = BaseService.GetAllBaseDictionaries();
            foreach (BaseDictionary baseDictionary in allDictionaries)
            {
                SystemDictionary.GetInstance.Add(baseDictionary);
            }
        }

        private static void CreateSystemAdmin()
        {
            IManagerRepository managerRepository = new ManagerRepository();
            if (managerRepository.GetManagerByUserName("systemadmin") == null)
            {
                Manager adminManager = ManagerFactory.Create("systemadmin", "admin2014", 0, ManagerTypeEnum.Super,
                    "超级管理员", "ETW", Utils.CoreDefaultValue.False, 0, "系统");
                adminManager.EncryptPassword();
                managerRepository.Save(adminManager);
            }

        }
    }
}
