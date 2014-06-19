using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Serives
{
    /// <summary>
    ///系统随程序启动程序 
    /// </summary>
    public class SystemApplication
    {
        public void Start()
        {
            LoadSysDictionary();
        }

        /// <summary>
        /// 加载字典常驻内存(大大减少不必要的读库操作)
        /// </summary>
        private void LoadSysDictionary()
        {

        }
    }
}
