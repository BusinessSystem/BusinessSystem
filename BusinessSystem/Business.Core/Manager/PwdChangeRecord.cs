using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Core
{
    /// <summary>
    /// 密码修改记录
    /// </summary>
    public class PwdChangeRecord
    {
        public virtual long Id { get; set; }
        public virtual string ChangeUserName { get; set; }
        public virtual string OldPassword { get; set; }
        public virtual string NewPassword { get; set; }
        public virtual string ChangeIp { get; set; }
        public virtual string Operator { get; set; }
        public virtual DateTime ChangeTime { get; set; }
    }

    public class PwdChangeRecordFactory
    {
        public static PwdChangeRecord Create(string changeUserName,string oldPwd,string newPwd,string changeIp,string operat)
        {
            return new PwdChangeRecord()
            {
                ChangeUserName = changeUserName,
                OldPassword = oldPwd,
                NewPassword = newPwd,
                ChangeIp = changeIp,
                Operator = operat,
                ChangeTime = DateTime.Now
            };
        }
    }
}
