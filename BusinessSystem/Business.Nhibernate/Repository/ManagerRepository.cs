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

        public IList<Manager> GetManagersByPage(ManagerTypeEnum managerType, int pageIndex, int pageSize, long parentId,
            out int totalCount)
        {
            using (var session = GetSession())
            {

                var query = session.QueryOver<Manager>()
                    .Where(m => m.ManagerType == managerType);
                if (parentId != 0)
                {
                    query = query.And(m => m.ParentId == parentId);
                }
                totalCount = query.RowCount();
                return
                    query.OrderBy(m => m.Id)
                        .Desc.Take(pageSize)
                        .Skip((pageIndex - 1)*pageSize)
                        .List();
            }
        }


        public Manager GetSuperManager()
        {
            using (var session = GetSession())
            {
                return
                    session.QueryOver<Manager>()
                        .Where(m => m.ManagerType == ManagerTypeEnum.Super).Take(1).SingleOrDefault();
                         
            }
        }

        public Manager GetManagerByUserName(string userName)
        {
            using (var session = GetSession())
            {
                return
                    session.QueryOver<Manager>()
                        .Where(m => m.UserName == userName).Take(1).SingleOrDefault();
            }
        }

        public IList<Manager> GetChildManagers(long parentId)
        {
            using (var session = GetSession())
            {
                return
                    session.QueryOver<Manager>()
                        .Where(m => m.ParentId == parentId).List();
            }
        }
    }
}
