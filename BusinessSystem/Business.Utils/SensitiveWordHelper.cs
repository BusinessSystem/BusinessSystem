using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Business.Utils
{
    public static class SensitiveWordHelper
    {
        /// <summary>
        /// 过滤敏感词
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ReplaceSensitiveWord(string input)
        {
            string swPath = ConfigHelper.GetAppSetting(ConfigHelper.Sensitivewordpath);

            if (File.Exists(swPath))
            {
                SensitiveWordsFilter sw = new SensitiveWordsFilter(swPath);

                IList<string> wordList = sw.GetSensitiveWords();

                if (wordList.Count > 0)
                {
                    return sw.Filting(input);
                }
            }

            return input;
        }
    }
}
