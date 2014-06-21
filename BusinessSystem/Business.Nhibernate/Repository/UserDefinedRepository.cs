using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Core;
using Business.Nhibernate.Base;
using Business.Nhibernate.IRepository;

namespace Business.Nhibernate.Repository
{
    public class UserDefinedRepository : Repository<UserDefined>, IUserDefinedRepository
    {
        public IList<UserDefined> GetAllUserDefineds()
        {
            using (var session = GetSession())
            {
                return session.QueryOver<UserDefined>().List();
            }
        }
    }
}
