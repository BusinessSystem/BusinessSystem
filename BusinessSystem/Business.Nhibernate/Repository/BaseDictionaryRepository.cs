using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Core;
using Business.Nhibernate.Base;
using Business.Nhibernate.IRepository;

namespace Business.Nhibernate.Repository
{
    public class BaseDictionaryRepository:Repository<BaseDictionary>,IBaseDictionaryRepository
    {
        public IList<BaseDictionary> GetBaseDictionaries(ValueTypeEnum valueType)
        {
            using (var session=GetSession())
            {
                return session.QueryOver<BaseDictionary>().Where(m => m.ValueType == valueType).List();
            }
        }
    }
}
