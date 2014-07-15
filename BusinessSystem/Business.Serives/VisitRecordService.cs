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

    }
}
