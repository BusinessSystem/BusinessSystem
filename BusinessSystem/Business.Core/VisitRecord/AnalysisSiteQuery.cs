using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Core.VisitRecord
{
    public class AnalysisSiteQuery : PagerOrder
    {
        /// <summary>
        /// 要查询的ip
        /// </summary>
        public string VIp { get; set; }
        /// <summary>
        /// 网站语言类型
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// 登录的账号
        /// </summary>
        public string LoginAccount { get; set; }

    }
}
