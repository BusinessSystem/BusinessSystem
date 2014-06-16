using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace Business.Utils.Info
{
    public static class InfoTools
    {
        const string xmlFile = "Info.xml";

        const string FileNameSpace = "TeaTop.EC.AppBase";


        private static Dictionary<string, InfoBase> infosCache = new Dictionary<string, InfoBase>();

        private static XDocument InfoXmlDoc = null;

        private static string path = GetConfigPath();

        private static InfoBase defaultInfoBase = InfoBase.CreateDefault();


        static InfoTools()
        {
            InfoXmlDoc = LoadErrXml();
            AddToCache(InfoXmlDoc);
        }


        private static string GetConfigPath()
        {
            if (string.IsNullOrEmpty(path))
            {
                string basePath = AppDomain.CurrentDomain.BaseDirectory;

                if (!basePath.EndsWith("\\"))
                    basePath = basePath + "\\";

                List<string> searchPathList = new List<string>()
                {
                    String.Concat(basePath, @"bin\Info\") + xmlFile,
                    String.Concat(basePath, @"bin\debug\Info\") + xmlFile,
                    String.Concat(basePath, @"Info\") + xmlFile,
                    basePath + xmlFile
                };
                path = searchPathList.FirstOrDefault(m => File.Exists(m));
            }
            return path;
        }

        private static XDocument LoadErrXml()
        {
            XDocument configXML = new XDocument();

            if (File.Exists(path))
                configXML = XDocument.Load(path);
            return configXML;
        }

        private static void AddToCache(XDocument xDoc)
        {
            List<InfoBase> listUserErrs = GetInfoBaseList(xDoc);

            infosCache = new Dictionary<string, InfoBase>();

            if ((listUserErrs != null) && (listUserErrs.Count > 0))
            {
                foreach (InfoBase info in listUserErrs)
                {
                    if (!infosCache.ContainsKey(info.Code))
                    {
                        infosCache.Add(info.Code, info);
                    }
                }
            }
        }

        private static List<InfoBase> GetInfoBaseList(XDocument configXML)
        {
            List<InfoBase> rtnList = null;

            if (configXML != null)
            {
                if (configXML.Root != null)
                {
                    List<XElement> eles = configXML.Root.Elements().ToList();
                    rtnList = new List<InfoBase>();

                    eles.ForEach(ele => rtnList.Add(
                        new InfoBase()
                        {
                            Code = ele.Attribute("Code").Value,
                            Level = ele.Attribute("Level").Value,
                            Message = ele.Attribute("Message").Value,
                            Description = ele.Attribute("Description").Value
                        })
                        );
                }
            }
            return rtnList;
        }



        private static InfoBase GetInfo(string code)
        {

            InfoBase info;
            if (!infosCache.TryGetValue(code, out info))
            {
                info = defaultInfoBase;
                info.Description = code;
            }

            return info;

        }

        public static MsgInfo GetMsgInfo(string code, object data)
        {
            return new MsgInfo(GetInfo(code), data);
        }

        public static MsgInfo GetMsgInfo(string code)
        {
            return GetMsgInfo(code, null);
        }

    }
}
