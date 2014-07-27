using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Business.WebApi.Models
{
    public class CompanyAndSummaryInfo
    {
        /// <summary>
        /// 公司名
        /// </summary>
        public string CompanyName;
        /// <summary>
        /// 访问人数
        /// </summary>
        public int PeopleNum;
        /// <summary>
        /// 对应语言的访问产品数
        /// </summary>
        public int ProductCount;
    }
}