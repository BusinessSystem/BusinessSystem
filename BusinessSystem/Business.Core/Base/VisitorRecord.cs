using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Core
{
    public class VisitorRecord
    {
        public virtual long Id { get; set; }

        public virtual string PurchaserIp { get; set; }

        public virtual string PurchaserProduct { get; set; }

        public virtual string Language { get; set; }

        public virtual string Country { get; set; }

        public virtual DateTime VisitTime { get; set; }

        public virtual string PurchaserDomain { get; set; }

        public virtual string ManagerEmail { get; set; }
    }

    /// <summary>
    /// 创建对象，创建对象都用此方法，使用统一，可控，避免重复
    /// </summary>
    public class VisitorRecordFactory
    {
        public static VisitorRecord Create(string ip, string product, string language, string country,string domain,string email)
        {
            return new VisitorRecord()
            {
                PurchaserIp = ip,
                PurchaserProduct = product,
                Country = country,
                Language = language,
                VisitTime = DateTime.Now,
                PurchaserDomain=domain,
                ManagerEmail = email
            };
        }
    }
}
