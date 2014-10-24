using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Business.Web.Models
{
    public class WebSiteAnalysisInfo
    {
        /// <summary>
        /// 采购商所在位置（IP）
        /// </summary>
        public string VIp;
        /// <summary>
        /// 采购商所看产品
        /// </summary>
        public string ProductName;
        /// <summary>
        /// 采购商所在位置地图显示
        /// </summary>
        public string VCountry;
        /// <summary>
        /// 浏览时间（北京时间）
        /// </summary>
        public string VTime;
    }
}