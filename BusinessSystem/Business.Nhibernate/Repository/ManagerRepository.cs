using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Core;
using Business.Nhibernate.Base;
using Business.Nhibernate.IRepository;

namespace Business.Nhibernate.Repository
{
    public class ManagerRepository : Repository<Manager>, IManagerRepository
    {
        /// <summary>
        /// 判断用户名是否存在
        /// </summary>
        /// <param name="manager"></param>
        /// <returns>存在 返回true</returns>
        public bool IsExist(Manager manager)
        {
            using (var session = GetSession())
            {
                return
                    session.QueryOver<Manager>()
                        .Where(m => m.UserName == manager.UserName && m.Id != manager.Id)
                        .RowCount() > 0;
            }
        }
    }
}
