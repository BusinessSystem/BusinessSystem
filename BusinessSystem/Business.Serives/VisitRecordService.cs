using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Nhibernate.IRepository;
using Business.Nhibernate.Repository;
using Business.Core;
using Business.Core.VisitRecord;

namespace Business.Serives
{
    public class VisitRecordService
    {
        private static IVisitorRecordRepository visitorRepository = new VisitorRecordRepository();

        public static List<WebSiteAnalysisInfo> GetVisitRecordList(WebSiteAnalysisQuery analysisQuery, string currentAccount, out int recordCount)
        {
            return visitorRepository.GetVisitorRecordsList(analysisQuery, currentAccount,out recordCount);
        }

        public static void VisitRecordSave(string purchaserIp, string purchaserProduct, string language, string country, string purchaserDomain, string managerEmail)
        {
            VisitorRecord record = VisitorRecordFactory.Create(purchaserIp,purchaserProduct,language,country,purchaserDomain,managerEmail);
            visitorRepository.Save(record);
        }

        /// <summary>
        /// 获取访问了指定语言的网站的人数
        /// </summary>
        /// <param name="language">语言</param>
        /// <param name="mainAccount">主帐号</param>
        public static int GetIpCount(string language, string mainAccount)
        {
            int ipCount = visitorRepository.GetVisitorRecordClientNumCount(language, mainAccount);
            return ipCount;
        }

        /// <summary>
        /// 获取访问了指定语言的网站的产品页面的次数
        /// </summary>
        /// <param name="language"></param>
        /// <param name="mainAccount"></param>
        /// <returns></returns>
        public static int GetVisitRecordCount(string language, string mainAccount)
        {
            int vCount = visitorRepository.GetVisitorRecordCount(language, mainAccount);
            return vCount;
        }

    }
}
