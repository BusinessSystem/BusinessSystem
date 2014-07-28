using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
        public virtual long ParentId { get; set; }
        public virtual ManagerTypeEnum ManagerType { get; set; }
        public virtual string RealName { get; set; }
        public virtual string Company { get; set; }
        public virtual short IsAutoDistribute { get; set; }
        public virtual int Language { get; set; }
        public virtual string Creator { get; set; }
        public virtual DateTime CreateTime { get; set; }
        public virtual string BindEmail { get; set; }//绑定客户邮箱

        public virtual void  EncryptPassword()
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
            cookieTable.Add("pId", ParentId);
            cookieTable.Add("rnm", RealName);
            cookieTable.Add("cp", Company);
            cookieTable.Add("managerId", ManagerType);
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
                ParentId = long.Parse(cookieTable["pId"].ToString()),
                RealName = cookieTable["rnm"].ToString(),
                Company = cookieTable["cp"].ToString(),
                ManagerType = (ManagerTypeEnum)short.Parse(cookieTable["managerId"].ToString())
            };
            return manager;
        }
    }

    public enum ManagerTypeEnum : short
    {
        [Description("管理员")] Super = 1,
        [Description("普通用户")] Common
    }

    public class ManagerFactory
    {
        public static Manager Create(string userName, string password, long parentId, ManagerTypeEnum managerType
            , string realName, string company, short isAutoDistribute, int language, string creator,string bindEmail)
        {
            return new Manager()
            {
                UserName = userName,
                Password = password,
                ParentId = parentId,
                ManagerType = managerType,
                RealName=realName,
                Company = company,
                IsAutoDistribute = isAutoDistribute,
                Language=language,
                Creator = creator,
                CreateTime = DateTime.Now,
                BindEmail = bindEmail
            };
        }
    }
}
