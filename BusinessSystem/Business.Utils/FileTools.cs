using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
 

namespace Business.Utils
{
    public class FileTools
    {
        public static bool TryReadFromFile(string fileName, out string content)
        {
            content = string.Empty;
            try
            {
                StreamReader rs = new StreamReader(fileName, Encoding.Default);

                content = rs.ReadToEnd();

                return true;
            }
            catch (Exception ex)
            {
                //ServerLogger.Error(string.Format("TryReadFromFile: {0}, Error: {1}", fileName, ex.ToString()));

                return false;
            }
        }

        /// <summary>
        /// 内容写入指定的文件，没有改文件时创建文件，内容存在则清空
        /// </summary>
        /// <param name="fileName">文件路径(包含文件名)</param>
        /// <param name="content">写入文件的内容</param>
        /// <returns></returns>
        public static bool TryWriteFile(string fileName,string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                return false;
            }
            try
            {
                FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                byte[] data = System.Text.Encoding.UTF8.GetBytes(content);
                fs.Seek(0, SeekOrigin.Begin);
                fs.SetLength(0);//清空
                fs.Write(data, 0, data.Length);
                fs.Flush();
                fs.Close();
                return true;
            }
            catch (Exception ex)
            {
               // ServerLogger.Error(string.Format("TryWriteFile: {0}, Error: {1}", fileName, ex.ToString()));
                return false;
            }
        }
    }
}
