using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;

namespace Business.Utils
{
    public class HttpRuntimeCache
    {
        public static void AddToWebCache(string cachingKey, object obj, int minitesTimeOut)
        {
            HttpRuntime.Cache.Add(cachingKey, obj, null, System.DateTime.Now.AddMinutes((double)minitesTimeOut), System.TimeSpan.Zero, CacheItemPriority.Normal, null);
        }

        public static object GetFromWebCache(string cachingKey)
        {
            return HttpRuntime.Cache.Get(cachingKey);
        }
    }
}
