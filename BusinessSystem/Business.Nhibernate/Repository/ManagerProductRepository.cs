using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Core;
using Business.Nhibernate.Base;
using Business.Nhibernate.IRepository;

namespace Business.Nhibernate.Repository
{
    public class ManagerProductRepository:Repository<ManagerProduct>,IManagerProductRepository
    {
        public IList<ManagerProduct> GetManagerProducts(long languageId, long managerId, string product,
            int pageIndex, int pageSize,out int totalCount)
        {
            using (var session = GetSession())
            {
                var query = session.QueryOver<ManagerProduct>().Where(m=>m.Id>0);
                if (managerId != 0)
                {
                    query = query.And(m => m.ManagerId == managerId);
                }
                if (languageId != 0)
                {
                    query = query.And(m => m.Language == languageId);
                }
                if (!string.IsNullOrEmpty(product))
                {
                    query = query.And(m => m.Product == product);
                }
                totalCount = query.RowCount();
                return query.Take(pageSize).Skip((pageIndex - 1)*pageSize).List();
            }
        }
    }
}
