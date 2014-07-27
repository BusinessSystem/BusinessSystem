using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Core;
using Business.Nhibernate.IRepository;
using NHibernate.Hql.Ast.ANTLR.Util;
using Business.Nhibernate.Base;
using Business.Core.VisitRecord;

namespace Business.Nhibernate.Repository
{
    public class VisitorRecordRepository:Repository<VisitorRecord>,IVisitorRecordRepository
    {

        /// <summary>
        /// 获取访问产品的次数，根据客户访问的站体类型
        /// </summary>
        /// <param name="language"></param>
        /// <param name="mainAccount"></param>
        /// <returns></returns>
        public int GetVisitorRecordCount(string language, string mainAccount)
        {
            using (var session = GetSession())
            {
                var query = session.QueryOver<VisitorRecord>().Where(m => m.Id > 0);
                if (!string.IsNullOrWhiteSpace(mainAccount))
                {
                    query = query.And(m => m.ManagerEmail == mainAccount);
                }
                if (!string.IsNullOrWhiteSpace(language))
                {
                    query = query.And(m => m.Language == language);
                }
                return query.RowCount();        
            }

        }

        /// <summary>
        /// 获取访问产品的客户数，根据客户访问的站体类型
        /// </summary>
        /// <param name="language"></param>
        /// <param name="mainAccount"></param>
        /// <returns></returns>
        public int GetVisitorRecordClientNumCount(string language, string mainAccount)
        {
            using (var session = GetSession())
            {
                string sqlstr = @"select count(distinct PurchaserIp) from t_visitorrecord where
                                  ManagerEmail='" +mainAccount+"' and Language='"+language+"'";
                var obj = session.CreateSQLQuery(sqlstr).UniqueResult();
                int coun = 0;
                if (obj != null)
                {
                    coun = Convert.ToInt32(obj.ToString());
                }
                return coun;
            }
        }

        //public IList<VisitorRecord> GetAllVisitorRecordsByEmail(string managerEmail)
        //{
        //    using (var session = GetSession())
        //    {
        //        return session.QueryOver<VisitorRecord>().Where(m => m.ManagerEmail == managerEmail).List();
        //    }
        //}

        //public IList<VisitorRecord> GetVisitorRecordsList(long managerId, long languageId, string siteUrl,
        //    int pageIndex, int pageSize, out int totalCount)
        public List<WebSiteAnalysisInfo> GetVisitorRecordsList(WebSiteAnalysisQuery recordQuery, string currentAccount, out int recordCount)
        {
            using (var session = GetSession())
            {
                var query = session.QueryOver<VisitorRecord>().Where(m => m.Id > 0);
                if (!string.IsNullOrWhiteSpace(currentAccount))
                {
                    query = query.And(m => m.ManagerEmail == currentAccount);
                }
                if (!string.IsNullOrWhiteSpace(recordQuery.VIp))
                {
                    query = query.And(m => m.PurchaserIp == recordQuery.VIp);
                }
                if (!string.IsNullOrWhiteSpace(recordQuery.Language))
                {
                    query = query.And(m => m.Language==recordQuery.Language);
                }
                recordCount = query.RowCount();
                //recordCount = 11;
                List<VisitorRecord> visitorRecordList = query.Take(recordQuery.PageSize).Skip((recordQuery.PageIndex-1) * recordQuery.PageSize).List().ToList<VisitorRecord>();
                List<WebSiteAnalysisInfo> webSiteAnalysisInfoList = new List<WebSiteAnalysisInfo>();
                foreach (VisitorRecord model in visitorRecordList)
                {
                    WebSiteAnalysisInfo tmpModel = new WebSiteAnalysisInfo();
                    tmpModel.VIp = model.PurchaserIp;
                    tmpModel.VCountry = model.Country;
                    tmpModel.ProductName = model.PurchaserProduct;
                    tmpModel.VTime = model.VisitTime.ToString();
                    webSiteAnalysisInfoList.Add(tmpModel);
                }
                return webSiteAnalysisInfoList;
            }
        }
    }
}
