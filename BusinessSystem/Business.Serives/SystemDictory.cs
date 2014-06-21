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

        private Dictionary<int, BaseDictionary> sysDictionary = null;
        private SystemDictionary()
        {
            sysDictionary = new Dictionary<int, BaseDictionary>();
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
            //sysDictionary.Add(baseDictionary.Id, baseDictionary);
        }

        public BaseDictionary GetBaseDictionary(int key)
        {
            return sysDictionary[key];
        }
    }
}
