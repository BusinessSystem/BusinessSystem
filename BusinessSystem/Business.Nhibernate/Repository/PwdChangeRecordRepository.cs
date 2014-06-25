using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Core;
using Business.Nhibernate.Base;
using Business.Nhibernate.IRepository;

namespace Business.Nhibernate.Repository
{
    public  class PwdChangeRecordRepository:Repository<PwdChangeRecord>,IPwdChangeRecordRepository
    {
        public IList<PwdChangeRecord> GetPwdChangeRecords(long managerId, int pageIndex, int pageSize)
        {
            using (var session = GetSession())
            {
                return
                    session.QueryOver<PwdChangeRecord>()
                        .Where(m => m.ManagerId == managerId).OrderBy(m=>m.ChangeTime).Desc
                        .Take(pageSize)
                        .Skip((pageIndex - 1)*pageSize)
                        .List();
            }
        }
    }
}
