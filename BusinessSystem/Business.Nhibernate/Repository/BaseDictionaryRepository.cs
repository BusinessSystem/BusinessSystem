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
                return session.QueryOver<BaseDictionary>().Where(m => m.ValueType == valueType).OrderBy(m=>m.DicId).Asc.List();
            }
        }


        public IList<BaseDictionary> GetAllBaseDictionaries()
        {
            using (var session = GetSession())
            {
                return session.QueryOver<BaseDictionary>().List();
            }
        }

        //通过语言值来获取整条记录
        public BaseDictionary GetDictionaryByValue(string value)
        {
            using (var session = GetSession())
            {
                return session.QueryOver<BaseDictionary>().Where(m => m.Value == value).SingleOrDefault<BaseDictionary>();
            }
        }
    }
}
