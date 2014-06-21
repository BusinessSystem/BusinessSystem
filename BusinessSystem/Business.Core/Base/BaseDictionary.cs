using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Business.Core
{
    /// <summary>
    /// 字典
    /// </summary>
    public class BaseDictionary
    {
        public virtual long Id { get; set; }
        public virtual ValueTypeEnum ValueType { get; set; }
        public virtual int DicId { get; set; }
        public virtual string Value { get; set; }
        public virtual string Description { get; set; }
        public virtual string Operator { get; set; }
        public virtual DateTime OperatorTime { get; set; }
        public virtual string OperatorDescritpion { get; set; }
    }

    public class BaseDictionaryFactory
    {
        public static BaseDictionary Create(ValueTypeEnum valueType, int dicId, string val, string description, string operat, string operatorDescription)
        {
            return new BaseDictionary()
            {
                ValueType = valueType,
                DicId = dicId,
                Value = val,
                Description = description,
                Operator = operat,
                OperatorTime =DateTime.Now,
                OperatorDescritpion = operatorDescription
            };
        }
    }

    public enum ValueTypeEnum : short
    {
        [Description("语言")]
         Language=1,
        [Description("亚洲")]
        Asia=2,
        [Description("欧洲")]
        Europe =3,
        [Description("大洋州")]
        Australia=4,
        [Description("非洲")]
        Africa =5,
        [Description("北美洲")]
        NorthAmerica =6,
        [Description("南美洲")]
        SouthAmerica=7
    }
}
