using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Business.Utils;

namespace Business.Core
{
    public class Manager
    {
        public virtual long Id { get; set; }
        public virtual string UserName { get; set; }
        public virtual string Password { get; set; }
        public virtual string ScretKey { get; set; }
        public virtual string SupplierName { get; set; }
        public virtual string SupplierMobile { get; set; }
        public virtual string SupplierCountry { get; set; }
        public virtual short SuperManager { get; set; }
        public virtual DateTime CreateTime { get; set; }

        public virtual void EncryptPassword()
        {
            Password = EncryptTools.GetMD5_32(Password);
        }

        public virtual bool MatchPassword(string plainPassword)
        {
            return this.Password == EncryptTools.GetMD5_32(plainPassword);
        }

        public virtual string GetManagerCookieString()
        {
            Hashtable cookieTable = new Hashtable();
            cookieTable.Add("id", Id);
            cookieTable.Add("mn", UserName);
            cookieTable.Add("managerId", SuperManager);
            return JsonConvert.SerializeObject(cookieTable);
        }

        public static Manager GetFromCookieString(string cookieString)
        {
            if (string.IsNullOrEmpty(cookieString)) return null;
            Hashtable cookieTable = JsonConvert.DeserializeObject<Hashtable>(cookieString);
            if (cookieTable == null) return null;
            Manager manager = new Manager
            {
                Id = int.Parse(cookieTable["id"].ToString()),
                UserName = cookieTable["mn"].ToString(),
                SuperManager = short.Parse(cookieTable["managerId"].ToString())
            };
            return manager;
        }
    }

    public class ManagerFactory
    {
        public static Manager Create(string userName, string password, string scretKey)
        {
            return new Manager()
            {
                UserName = userName,
                Password = password,
                ScretKey = scretKey,
                SupplierName = string.Empty,
                SupplierMobile = string.Empty,
                SupplierCountry = string.Empty,
                CreateTime = DateTime.Now
            };
        }
    }
}
