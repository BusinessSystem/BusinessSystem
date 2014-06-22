using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Core;

namespace Business.Serives
{
    public class SystemDictionary
    {
        private static SystemDictionary instance = null;
        private static object objLock=new object();
        
        private Dictionary<long, BaseDictionary> sysDictionary = null;
        private SystemDictionary()
        {
            sysDictionary = new Dictionary<long, BaseDictionary>();
        }

        public static SystemDictionary GetInstance
        {
            get
            {
                if (instance == null)
                {
                    lock (objLock)
                    {
                        if (instance == null)
                        {
                            instance = new SystemDictionary();
                        }
                    }
                }
                return instance;
            }
        }

        public void Add(BaseDictionary baseDictionary)
        {
            sysDictionary.Add(baseDictionary.Id, baseDictionary);
        }

        public BaseDictionary GetBaseDictionary(long key)
        {
            if (sysDictionary.ContainsKey(key))
            {
                return sysDictionary[key];
            }
            BaseDictionary baseDictionary = BaseService.GetDictionaryById(key);
            if (baseDictionary != null)
            {
                sysDictionary.Add(baseDictionary.Id, baseDictionary);
                return sysDictionary[key];
            }
            return new BaseDictionary();
        }
    }
}
