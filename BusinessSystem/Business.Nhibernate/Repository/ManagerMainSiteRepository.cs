using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Core;
using Business.Nhibernate.Base;
using Business.Nhibernate.IRepository;

namespace Business.Nhibernate.Repository
{
    public class ManagerMainSiteRepository:Repository<ManagerMainSite>,IManagerMainSiteRepository
    {
        public IList<ManagerMainSite> GetManagerMainSitesByManagerId(long managerId)
        {
            using (var session = GetSession())
            {
                return session.QueryOver<ManagerMainSite>().Where(m => m.ManagerId == managerId).List();
            }
        }

        public IList<ManagerMainSite> GetManagerMainSitePages(long managerId, long languageId, string siteUrl,
            int pageIndex, int pageSize, out int totalCount)
        {

            using (var session = GetSession())
            {
                var query = session.QueryOver<ManagerMainSite>().Where(m => m.Id > 0);
                if (managerId != 0)
                {
                    query = query.And(m => m.ManagerId == managerId);
                }
                if (languageId != 0)
                {
                    query = query.And(m => m.LanguageId == languageId);
                }
                if (!string.IsNullOrEmpty(siteUrl))
                {
                    query = query.And(m => m.SiteUrl == siteUrl);
                }
                totalCount = query.RowCount();
                return query.Take(pageSize).Skip((pageIndex - 1)*pageSize).List();
            }
        }
    }
}
