using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Business.Utils
{
    public class SensitiveWordsFilter
    {
        private int _SensitiveWordsCacheExpMin;
        private string _SensitiveWordsCacheKey;

        public SensitiveWordsFilter(string sensitiveWordsTxtPath)
        {
            this._SensitiveWordsCacheKey = "SensitiveWords";
            this._SensitiveWordsCacheExpMin = 120;
            this.SensitiveWordsTxtPath = sensitiveWordsTxtPath;
        }

        public SensitiveWordsFilter(string sensitiveWordsTxtPath, int sensitiveWordsCacheExpMin)
        {
            this._SensitiveWordsCacheKey = "SensitiveWords";
            this._SensitiveWordsCacheExpMin = 120;
            this.SensitiveWordsTxtPath = sensitiveWordsTxtPath;
            this.SensitiveWordsCacheExpMin = sensitiveWordsCacheExpMin;
        }

        public SensitiveWordsFilter(string sensitiveWordsTxtPath, string sensitiveWordsCacheKey)
        {
            this._SensitiveWordsCacheKey = "SensitiveWords";
            this._SensitiveWordsCacheExpMin = 120;
            this.SensitiveWordsTxtPath = sensitiveWordsTxtPath;
            this.SensitiveWordsCacheKey = sensitiveWordsCacheKey;
        }

        public SensitiveWordsFilter(string sensitiveWordsTxtPath, string sensitiveWordsCacheKey, int sensitiveWordsCacheExpMin)
        {
            this._SensitiveWordsCacheKey = "SensitiveWords";
            this._SensitiveWordsCacheExpMin = 120;
            this.SensitiveWordsTxtPath = sensitiveWordsTxtPath;
            this.SensitiveWordsCacheKey = sensitiveWordsCacheKey;
            this.SensitiveWordsCacheExpMin = sensitiveWordsCacheExpMin;
        }

        public string Filting(string orgText)
        {
            if (!string.IsNullOrEmpty(orgText))
            {
                return this.Filting(new System.Text.StringBuilder(orgText), this.GetSensitiveWords()).ToString();
            }
            return "";
        }

        public System.Text.StringBuilder Filting(System.Text.StringBuilder orgText, System.Collections.Generic.IList<string> sensitiveWords)
        {
            if (sensitiveWords.Count > 0)
            {
                foreach (string str in sensitiveWords)
                {
                    orgText.Replace(str.Trim(), "".PadRight(str.Length, '*'));
                }
            }
            return orgText;
        }

        public System.Collections.Generic.IList<string> GetSensitiveWords()
        {
            return this.GetSensitiveWords(this._SensitiveWordsCacheKey, this._SensitiveWordsCacheExpMin);
        }

        public System.Collections.Generic.IList<string> GetSensitiveWords(string cacheKey, int cacheExpMin)
        {
            System.Collections.Generic.IList<string> list = new System.Collections.Generic.List<string>();
            if (!string.IsNullOrEmpty(this.SensitiveWordsTxtPath))
            {
                object fromWebCache = HttpRuntimeCache.GetFromWebCache(cacheKey);
                if (fromWebCache != null)
                {
                    return (System.Collections.Generic.IList<string>)fromWebCache;
                }
                list = FileReader.StaticReadFileBackList(this.SensitiveWordsTxtPath);
                HttpRuntimeCache.AddToWebCache(cacheKey, list, cacheExpMin);
            }
            return list;
        }

        public int SensitiveWordsCacheExpMin
        {
            get
            {
                return this._SensitiveWordsCacheExpMin;
            }
            set
            {
                this._SensitiveWordsCacheExpMin = value;
            }
        }

        public string SensitiveWordsCacheKey
        {
            get
            {
                return this._SensitiveWordsCacheKey;
            }
            set
            {
                this._SensitiveWordsCacheKey = value;
            }
        }

        public string SensitiveWordsTxtPath { get; set; }
    }
}
