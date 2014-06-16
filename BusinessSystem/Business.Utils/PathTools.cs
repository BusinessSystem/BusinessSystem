using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Business.Utils
{
    public class PathTools
    {
        public static string GetConsoleAppPath()
        {
            return System.AppDomain.CurrentDomain.BaseDirectory;
        }

        public static string GetWebRootPath()
        {
            return System.AppDomain.CurrentDomain.BaseDirectory;
        }

        /// <summary>
        /// 相对路径返回绝对路径
        /// </summary>
        /// <param name="defaultFile">默认文件,请带扩展名</param>
        /// <param name="path">配置路径,为null返回当前目录的默认文件,</param>
        /// <returns></returns>
        public static string GetFullPath(string defaultFile, string path)
        {
            if (string.IsNullOrEmpty(path))
                path = Path.GetFullPath(defaultFile);
            else if (!Path.IsPathRooted(path))
            {
                path = Path.GetFullPath(path);
                if (!Path.HasExtension(path))
                {
                 path= Path.Combine(path, defaultFile);
                }
            }
            return path;
        }
    }
}
