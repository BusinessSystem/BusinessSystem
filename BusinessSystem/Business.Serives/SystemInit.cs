using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Core;

namespace Business.Serives
{
    /// <summary>
    ///系统随程序启动程序 
    /// </summary>
    public class SystemApplication
    {
        public static void Start()
        {
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
    }
}
