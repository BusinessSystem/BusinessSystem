using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Utils
{
    public class FileReader
    {
        private string FilePath = string.Empty;

        public FileReader(string filePath)
        {
            this.FilePath = filePath;
        }

        public System.Text.StringBuilder ReaderFile()
        {
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            try
            {
                if (!(this.FilePath != string.Empty))
                {
                    return builder;
                }
                System.IO.StreamReader reader = new System.IO.StreamReader(this.FilePath);
                string str = string.Empty;
                while (reader.ReadLine() != null)
                {
                    builder.Append(str);
                }
            }
            catch (System.Exception)
            {
            }
            return builder;
        }

        public System.Collections.Generic.IList<string> ReadFileBackList()
        {
            return this.ReadFileBackList(this.FilePath);
        }

        public System.Collections.Generic.IList<string> ReadFileBackList(string filePath)
        {
            System.Collections.Generic.IList<string> list = new System.Collections.Generic.List<string>();
            try
            {
                if (!(filePath != string.Empty))
                {
                    return list;
                }
                System.IO.StreamReader reader = new System.IO.StreamReader(filePath);
                string str = string.Empty;
                while (reader.ReadLine() != null)
                {
                    list.Add(str);
                }
            }
            catch (System.Exception)
            {
            }
            return list;
        }

        public static System.Text.StringBuilder StaticReaderFile(string filePath)
        {
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            try
            {
                if (!(filePath != string.Empty))
                {
                    return builder;
                }
                System.IO.StreamReader reader = new System.IO.StreamReader(filePath);
                string str = string.Empty;
                while ((str = reader.ReadLine()) != null)
                {
                    builder.Append(str);
                }
            }
            catch (System.Exception)
            {
            }
            return builder;
        }

        public static System.Collections.Generic.IList<string> StaticReadFileBackList(string filePath)
        {
            System.Collections.Generic.IList<string> list = new System.Collections.Generic.List<string>();
            try
            {
                if (!(filePath != string.Empty))
                {
                    return list;
                }
                using (System.IO.StreamReader reader = new System.IO.StreamReader(filePath,Encoding.UTF8))
                {
                    string str;
                    while ((str = reader.ReadLine()) != null)
                    {
                        list.Add(str);
                    }
                }
            }
            catch (Exception)
            {
            }
            return list;
        }
    }
}
