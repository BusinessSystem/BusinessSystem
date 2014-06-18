using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Core;
using Business.Nhibernate.Base;
using Business.Nhibernate.IRepository;

namespace Business.Nhibernate.Repository
{
    public class IntentionRepository : Repository<Intention>, IIntentionRepository
    {
        public IList<Intention> GetAllIntentions()
        {
            using (var session = GetSession())
            {
                return session.QueryOver<Intention>().List();
            }
        }
    }
}
