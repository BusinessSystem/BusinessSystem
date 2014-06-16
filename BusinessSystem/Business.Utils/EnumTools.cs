using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Business.Utils
{
    public class EnumTools
    {
        /// <summary>
        /// 获取枚举类子项描述信息
        /// </summary>
        /// <param name="enumSubitem">枚举类子项</param>        
        public static string GetEnumDescription(Enum enumSubitem)
        {
            string strValue = enumSubitem.ToString();

            FieldInfo fieldinfo = enumSubitem.GetType().GetField(strValue);
            Object[] objs = fieldinfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (objs.Length == 0)
            {
                return strValue;
            }
            DescriptionAttribute da = (DescriptionAttribute)objs[0];
            return da.Description;

        }
        /// <summary>
        /// 获取枚举类所有描述与自身的字典集
        /// </summary>
        public static Dictionary<T, string> GetEnumDescriptions<T>()
        {
            Dictionary<T, string> result = new Dictionary<T, string>();
            Type enumType = typeof(T);
            Array arr = enumType.GetEnumValues();
            foreach (var item in arr)
            {
                if (item is Enum)
                {
                    string itemDesc = GetEnumDescription((Enum)item);
                    result.Add((T)item, itemDesc);
                }
            }
            return result;

        }



    }
}
