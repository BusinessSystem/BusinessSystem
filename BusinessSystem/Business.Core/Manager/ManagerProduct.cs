using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;

namespace Business.Core
{
    /// <summary>
    /// 客户和他的URL对应表
    /// </summary>
    public class ManagerProduct
    {
        public virtual long Id { get; set; }
        
        public virtual long ManagerMainSiteId { get; set; }

        public virtual string ProductUrl { get; set; }

        public virtual string ProductName { get; set; }

        public virtual string ProductDescription { get; set; }
        
        public virtual string Operator { get; set; }

        public virtual DateTime OperateTime { get; set; }
    }


    public class ManagerProductFactory
    {
        public static ManagerProduct Create(long managerMainSiteId, string productName, string productUrl,
            string productDescription, string operate)
        {
            return new ManagerProduct()
            {
                ManagerMainSiteId = managerMainSiteId,
                ProductUrl = productUrl,
                ProductName = productName,
                ProductDescription = productDescription,
                Operator = operate,
                OperateTime = DateTime.Now
            };
        }
    }


}
