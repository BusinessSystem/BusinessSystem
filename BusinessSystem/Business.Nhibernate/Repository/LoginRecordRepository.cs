using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Core;
using Business.Nhibernate.IRepository;
using Business.Nhibernate.Base;

namespace Business.Nhibernate.Repository
{
    public class LoginRecordRepository :Repository<LoginRecord>, ILoginRecordRepository
    {
        public IList<LoginRecord> GetLoginRecords(string loginUser, int pageIndex, int pageSize,out int totalCount)
        {
            using (var session = GetSession())
            {
                var query = session.QueryOver<LoginRecord>();
                if (!string.IsNullOrEmpty(loginUser))
                {
                    query = query.And(m => m.LoginUser == loginUser);
                }
                totalCount = query.RowCount();
                return query.OrderBy(m => m.Id).Desc.Take(pageSize).Skip((pageIndex - 1)*pageSize).List();
            }
        }
    }
}
