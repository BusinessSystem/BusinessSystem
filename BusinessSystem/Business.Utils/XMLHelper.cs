using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace WeiXinLib
{
    public class XmlHelper
    {
        /// <summary>
        /// 反序列化为对象
        /// </summary>
        /// <param name="T">对象类型</param>
        /// <param name="s">对象序列化后的Xml字符串</param>
        /// <returns></returns>
        public static T Deserialize<T>(string s)
        {
            using (StringReader sr = new StringReader(s))
            {
                //XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces();
                //xmlSerializerNamespaces.Add("", "");
                XmlSerializer xz = new XmlSerializer(typeof (T));
                return (T) xz.Deserialize(sr);
            }
        }

        /// <summary>
        /// 将指定XML文件实例化为对象T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlFilePath"></param>
        /// <returns></returns>
        public static T DeserializeWithPath<T>(string xmlFilePath)
        {
            T t = default(T);
            if (!string.IsNullOrEmpty(xmlFilePath) && File.Exists(xmlFilePath))
            {
                using (StreamReader sr = new StreamReader(xmlFilePath))
                {
                    XmlSerializer xz = new XmlSerializer(typeof (T));
                    return (T) xz.Deserialize(sr);
                }

            }
            return t;
        }


        /// <summary>
        /// 序列化一个对象成XML并写入指定路径的文件
        /// </summary>
        /// <typeparam name="T">要进行序列化的对象类型</typeparam>
        /// <param name="t">要进行序列化的对象</param>
        /// <param name="xmlFilePath">指定路径的文件</param>
        /// <returns></returns>
        public static bool Serialize<T>(T t, string xmlFilePath, string rootName = "", string rootNamespace = "",string prefix="",string ns="")
        {
            if (string.IsNullOrEmpty(xmlFilePath))
                return false;

            XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces();
            xmlSerializerNamespaces.Add(prefix,ns);

            try
            {
                FileInfo fi = new FileInfo(xmlFilePath);
                if (fi.Directory != null && !fi.Directory.Exists)
                    fi.Directory.Create();

                using (StreamWriter sw = File.CreateText(xmlFilePath))
                {
                    XmlRootAttribute xmlRootAttribute = new XmlRootAttribute();
                    xmlRootAttribute.ElementName = rootName;
                    xmlRootAttribute.Namespace = rootNamespace;
                    XmlSerializer xz = new XmlSerializer(t.GetType(), xmlRootAttribute);
                    xz.Serialize(sw, t, xmlSerializerNamespaces);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 将实体序列化为XML格式的字体串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string SerializeString<T>(T t)
        {
            try
            {
                XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces();
                xmlSerializerNamespaces.Add("", "");
                using (StringWriter sw = new StringWriter())
                {
                    XmlSerializer xz = new XmlSerializer(t.GetType());
                    xz.Serialize(sw, t, xmlSerializerNamespaces);
                    return sw.ToString();
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
