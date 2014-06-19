﻿using System;
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
        public virtual int Id { get; set; }
        public virtual ValueTypeEnum ValueType { get; set; }
        public virtual int DicId { get; set; }
        public virtual string Value { get; set; }
        public virtual string Description { get; set; }
        public virtual string Operator { get; set; }
        public virtual string OperatorTime { get; set; }
        public virtual string OperatorDescritpion { get; set; }
    }

    public class BaseDictionaryFactory
    {
        public static BaseDictionary Create(ValueTypeEnum valueType, int dicId, string val, string description, string operat, string operatorTime, string operatorDescription)
        {
            return new BaseDictionary()
            {
                ValueType = valueType,
                DicId = dicId,
                Value = val,
                Description = description,
                Operator = operat,
                OperatorTime = operatorTime,
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
        [Description("Antarctica")]
        Antarctica=5,
        [Description("非洲")]
        Africa =6,
        [Description("北美洲")]
        NorthAmerica =7,
        [Description("南美洲")]
        SouthAmerica=8
    }
}
