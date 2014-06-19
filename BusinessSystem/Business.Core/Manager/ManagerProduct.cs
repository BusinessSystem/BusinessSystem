using System;
using System.Collections.Generic;
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

        public virtual long ManagerId { get; set; }

        public virtual string Product { get; set; }

        public virtual string Language { get; set; }

        public virtual string Operator { get; set; }

        public virtual DateTime OperateTime { get; set; }
    }

    public class ManagerProductFactory
    {
        public static ManagerProduct Create(long managerId,string product,string language,string operate)
        {
            return new ManagerProduct()
            {
                ManagerId = managerId,
                Product = product,
                Language = language,
                Operator = operate,
                OperateTime = DateTime.Now
            };
        }
    }
}
