using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Business.Utils
{
    public class TypeTools
    {
        /// <summary>
        /// 根据父类获取所有子类
        /// </summary>
        /// <param name="baseType"></param>
        /// <returns></returns>
        public static List<Type> GetAllSubClass(Type baseType)
        {
            var subTypeQuery = from t in Assembly.GetAssembly(baseType).GetTypes()
                               where IsSubClassOf(t, baseType)
                               select t;

            return subTypeQuery.ToList();
        }
        /// <summary>
        /// 获取该类（包括其子类）中赋予了泛型参数指定的属性的所有方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="baseType"></param>
        /// <returns></returns>
        public static List<MethodInfo> GetMethodsByAttribute<T>(Type baseType) where T : Attribute
        {
            List<MethodInfo> result = new List<MethodInfo>();
         List<Type> typeList=    GetAllSubClass(baseType);
            typeList.Add(baseType);
            foreach (Type item in typeList)
            {
                MethodInfo[] infos = item.GetMethods();
                result.AddRange(infos.Where(memberInfo => Attribute.IsDefined(memberInfo, typeof(T))));
            }
            return result;
        }

        private static bool IsSubClassOf(Type type, Type baseType)
        {
            var b = type.BaseType;
            while (b != null)
            {
                if (b == baseType)
                {
                    return true;
                }
                b = b.BaseType;
            }
            return false;
        }

      
    }
}
