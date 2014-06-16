using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace Business.Utils
{
    public class BadWordParse
    {
        private static readonly HashSet<string> Hash = new HashSet<string>();
        private static readonly byte[] FastCheck = new byte[char.MaxValue];
        private static readonly BitArray CharCheck = new BitArray(char.MaxValue);
        private static int _maxWordLength = 0;
        private static int _minWordLength = int.MaxValue;
        private static bool _isHave = false;
        private static string _replaceString = "*";
        private char _splitString = '|';
        private static string _newWord;
        private static string _badWordFilePath;
        private const string FilePath = "SensitiveFileName";

        #region 属性

        /// <summary>
        /// 是否含有脏字
        /// </summary>
        public bool IsHave
        {
            get { return _isHave; }
        }

        /// <summary>
        /// 替换后字符串
        /// </summary>
        public string ReplaceString
        {
            set { _replaceString = value; }
        }

        /// <summary>
        /// 脏字字典切割符
        /// </summary>
        public char SplitString
        {
            set { _splitString = value; }
        }

        /// <summary>
        /// 更新后的字符串
        /// </summary>
        public string NewWord
        {
            get { return _newWord; }
        }

        /// <summary>
        /// 脏字字典文档路径
        /// </summary>
        public string BadWordFilePath
        {
            get { return _badWordFilePath; }
            set { _badWordFilePath = value; }
        } 
        #endregion

        #region 初始化
        /// <summary>
        /// 初始化第三
        /// </summary>
        public static void Init()
        {
            string path = PathTools.GetConsoleAppPath();
            string fileName = ConfigurationManager.AppSettings[FilePath];
            _badWordFilePath = path+fileName;
            string srList = string.Empty;
            string content;
            if (File.Exists(_badWordFilePath))
            {
                StreamReader sr = new StreamReader(_badWordFilePath, Encoding.GetEncoding("gb2312"));
                srList = sr.ReadToEnd();
                sr.Close();
                sr.Dispose();
            }
            string[] badwords = srList.Split('|');
            foreach (string word in badwords)
            {
                _maxWordLength = Math.Max(_maxWordLength, word.Length);
                _minWordLength = Math.Min(_minWordLength, word.Length);
                for (int i = 0; i < 7 && i < word.Length; i++)
                {
                    FastCheck[word[i]] |= (byte)(1 << i);
                }

                for (int i = 7; i < word.Length; i++)
                {
                    FastCheck[word[i]] |= 0x80;
                }

                if (word.Length == 1)
                {
                    CharCheck[word[0]] = true;
                }
                else
                {
                    Hash.Add(word);
                }
            }
        } 
        #endregion

        #region 查找敏感词
        /// <summary>
        /// 查找敏感词
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public bool HasBadWord(string text)
        {
            int index = 0;

            while (index < text.Length)
            {


                if ((FastCheck[text[index]] & 1) == 0)
                {
                    while (index < text.Length - 1 && (FastCheck[text[++index]] & 1) == 0) ;
                }

                //单字节检测
                if (_minWordLength == 1 && CharCheck[text[index]])
                {
                    return true;
                }


                //多字节检测
                for (int j = 1; j <= Math.Min(_maxWordLength, text.Length - index - 1); j++)
                {
                    //快速排除
                    if ((FastCheck[text[index + j]] & (1 << Math.Min(j, 7))) == 0)
                    {
                        break;
                    }

                    if (j + 1 >= _minWordLength)
                    {
                        string sub = text.Substring(index, j + 1);

                        if (Hash.Contains(sub))
                        {
                            return true;
                        }
                    }
                }
                index++;
            }
            return false;
        } 
        #endregion

        #region 替换文本
        /// <summary>
        /// 替换敏感词
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ReplaceBadWord(string text)
        {
            int index = 0;

            for (index = 0; index < text.Length; index++)
            {
                if ((FastCheck[text[index]] & 1) == 0)
                {
                    while (index < text.Length - 1 && (FastCheck[text[++index]] & 1) == 0) ;
                }

                //单字节检测
                if (_minWordLength == 1 && CharCheck[text[index]])
                {
                    //return true;
                    _isHave = true;
                    text = text.Replace(text[index], _replaceString[0]);
                    continue;
                }
                //多字节检测
                for (int j = 1; j <= Math.Min(_maxWordLength, text.Length - index - 1); j++)
                {

                    //快速排除
                    if ((FastCheck[text[index + j]] & (1 << Math.Min(j, 7))) == 0)
                    {
                        break;
                    }

                    if (j + 1 >= _minWordLength)
                    {
                        string sub = text.Substring(index, j + 1);

                        if (Hash.Contains(sub))
                        {

                            //替换字符操作
                            _isHave = true;
                            char cc = _replaceString[0];
                            string rp = _replaceString.PadRight((j + 1), cc);
                            text = text.Replace(sub, rp);
                            //记录新位置
                            index += j;
                            break;
                        }
                    }
                }
            }
            _newWord = text;
            return text;
        } 
        #endregion

    }

}