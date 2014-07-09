using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Core
{
    public class LoginRecord
    {
        public virtual long Id { get; set; }
        public virtual string LoginUser { get; set; }
        public virtual string IpAddress { get; set; }
        public virtual string LoginStatus { get; set; }
        public virtual DateTime LoginTime { get; set; }
    }

    public class LoginRecordFactory
    {
        public static LoginRecord Create(string loginUser,string ipString)
        {
            return new LoginRecord()
            {
                LoginUser = loginUser,
                IpAddress = ipString,
                LoginTime = DateTime.Now,
                LoginStatus ="成功"
            };
        }
    }
}
