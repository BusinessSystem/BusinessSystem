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
        IList<Intention> GetAllIntentions(long mainManagerId);
    }

    public interface IUserDefinedRepository : IRepository<UserDefined>
    {
        IList<UserDefined> GetAllUserDefineds(long mainManagerId);
    }

    //public interface IVisitorRecordRepository : IRepository<VisitorRecord>
    //{
    //    IList<VisitorRecord> GetAllVisitorRecordsByEmail(string managerEmail)
    //}

    public interface IBaseDictionaryRepository : IRepository<BaseDictionary>
    {
        IList<BaseDictionary> GetBaseDictionaries(ValueTypeEnum valueType);
        IList<BaseDictionary> GetAllBaseDictionaries();
        BaseDictionary GetDictionaryByValue(string value);//add by luoyaqi
    }
}
