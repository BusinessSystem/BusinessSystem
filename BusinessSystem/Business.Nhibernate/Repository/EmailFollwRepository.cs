using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Core;
using Business.Nhibernate.Base;
using Business.Nhibernate.IRepository;

namespace Business.Nhibernate.Repository
{
    public class EmailFollwRepository : Repository<EmailFollow>,IEmailFollowRepository
    {
        public IList<EmailFollow> GetEmailFollowsByTransId(long transId)
        {
            using (var session = GetSession())
            {
                return session.QueryOver<EmailFollow>().Where(m => m.EmailTransId == transId).List();
            }
        }
    }
}
