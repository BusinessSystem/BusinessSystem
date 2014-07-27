using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Nhibernate.Base;
using Business.Core;
using Business.Core.VisitRecord;

namespace Business.Nhibernate.IRepository
{
    public interface IVisitorRecordRepository:IRepository<VisitorRecord>
    {
        int GetVisitorRecordCount(string language, string mainAccount);

        int GetVisitorRecordClientNumCount(string language, string mainAccount);

        //IList<VisitorRecord> GetAllVisitorRecordsByEmail(string managerEmail);
        List<WebSiteAnalysisInfo> GetVisitorRecordsList(WebSiteAnalysisQuery recordQuery, string currentAccount, out int recordCount);

    }
}
