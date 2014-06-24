using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Core;
using Business.Nhibernate.Base;

namespace Business.Nhibernate.IRepository
{
    public interface IManagerRepository : IRepository<Manager>
    {
        /// <summary>
        /// 判断用户名是否存在
        /// </summary>
        /// <param name="manager"></param>
        /// <returns>存在 返回true</returns>
        bool IsExist(Manager manager);

        IList<Manager> GetManagersByPage(ManagerTypeEnum managerType, int pageIndex, int pageSize, long parentId,
            out int totalCount);

        Manager GetManagerByUserName(string userName);

        Manager GetSuperManager();

        IList<Manager> GetChildManagers(long parentId);
    }

    public interface IManagerProductRepository : IRepository<ManagerProduct>
    {
        
    }

    public interface IPwdChangeRecordRepository : IRepository<PwdChangeRecord>
    {

    }
}
