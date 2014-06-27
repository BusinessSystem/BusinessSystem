using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Core;
using Business.Nhibernate.Base;

namespace Business.Nhibernate.IRepository
{
    public interface IManagerMainSiteRepository:IRepository<ManagerMainSite>
    {
        IList<ManagerMainSite> GetManagerMainSitesByManagerId(long managerId);

        IList<ManagerMainSite> GetManagerMainSitePages(long managerId, long languageId, string siteUrl,
            int pageIndex, int pageSize, out int totalCount);
    }
}
