using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Core;
using Business.Nhibernate.Base;

namespace Business.Nhibernate.IRepository
{
    public interface IIntentionRepository : IRepository<Intention>
    {
        IList<Intention> GetAllIntentions();
    }

    public interface IUserDefinedRepository : IRepository<UserDefined>
    {
        IList<UserDefined> GetAllUserDefineds();
    }

    public interface IVisitorRecordRepository : IRepository<VisitorRecord>
    {

    }

    public interface IBaseDictionaryRepository : IRepository<BaseDictionary>
    {

    }
}
