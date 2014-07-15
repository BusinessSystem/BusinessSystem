using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Business.WebApi.Models
{
    public class WebSiteAnalysisQuery:PagerOrder
    {
        /// <summary>
        /// 要查询的ip
        /// </summary>
        public string VIp{ get; set; }
        /// <summary>
        /// 网站语言类型
        /// </summary>
        public string Language { get; set; }
    }
}