using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Business.Serives;

namespace Business.WebApi.Controllers
{
    public class AanlysisSiteController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage GetLanguageTypeList(string LoginAccount)
        {
            var returnObj = new Business.WebApi.Models.ResultObject<List<string>>();
            List<string> retList = new List<string>();

            //取用户登录成功后保存的session
            string emailAccount = LoginAccount;
            Business.Core.Manager manager = null;
            manager = ManageService.GetManagerByUsername(emailAccount);

            if (manager.ParentId != 0)
            {
                manager = ManageService.GetManagerById(manager.ParentId);
            }

            if (manager != null)
            {
                List<Business.Core.ManagerMainSite> mangerMainSiteList = ManagerMainSiteService.GetManagerMainSitesByManagerId(manager.Id);
                foreach (var tmp in mangerMainSiteList)
                {
                    retList.Add(tmp.LanguageName);
                }
            }

            returnObj.ReturnData = retList;
            returnObj.Status = Business.WebApi.Models.ServerStatus.SearchSuccess;
            return Request.CreateResponse<Business.WebApi.Models.ResultObject<List<string>>>(HttpStatusCode.OK, returnObj);
        }

        
        [HttpPost]
        public HttpResponseMessage GetInfoListByIp(Business.Core.VisitRecord.AnalysisSiteQuery query)
        {
            /*****根据客户的邮箱账号，网站语言，和输入查询的ip来查询出访问信息*****/
            //取用户登录成功后保存的session
            string emailAccount = query.LoginAccount;
            //获取传过来的的网站语言
            string language = query.Language;

            Business.Core.VisitRecord.WebSiteAnalysisQuery anaysisQuery = new Core.VisitRecord.WebSiteAnalysisQuery();
            anaysisQuery.IsPager = query.IsPager;
            anaysisQuery.Language = query.Language;
            anaysisQuery.OrderByDesc = query.OrderByDesc;
            anaysisQuery.OrderByValue = query.OrderByValue;
            anaysisQuery.PageIndex = query.PageIndex;
            anaysisQuery.PageSize = query.PageSize;
            anaysisQuery.VIp = query.VIp;
            Business.Core.Manager manager = null;
            manager = ManageService.GetManagerByUsername(emailAccount);

            if (manager.ParentId != 0)
            {
                manager = ManageService.GetManagerById(manager.ParentId);
            }

            var returnObj = new Business.WebApi.Models.ResultObject<List<Business.Core.VisitRecord.WebSiteAnalysisInfo>>();
            int recordcount = 0;
            returnObj.ReturnData = VisitRecordService.GetVisitRecordList(anaysisQuery, manager.UserName, out recordcount);
            returnObj.RecordCount = recordcount;
            returnObj.Status = Business.WebApi.Models.ServerStatus.Success;
            return Request.CreateResponse<Business.WebApi.Models.ResultObject<List<Business.Core.VisitRecord.WebSiteAnalysisInfo>>>(HttpStatusCode.OK, returnObj);
        }
    }
}