using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using Business.Serives;

namespace Business.WebApi.Controllers
{
    public class WebSiteAnalysisController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage GetInfoListByIp(Business.Core.VisitRecord.WebSiteAnalysisQuery anaysisQuery)
        {
            /*****根据客户的邮箱账号，网站语言，和输入查询的ip来查询出访问信息*****/
            //取用户登录成功后保存的session
            string emailAccount = HttpContext.Current.Session["LoginAccount"].ToString();
            //获取传过来的的网站语言
            string language = anaysisQuery.Language;

            var returnObj = new Business.WebApi.Models.ResultObject<List<Business.Core.VisitRecord.WebSiteAnalysisInfo>>();
            int recordcount=0;
            returnObj.ReturnData = VisitRecordService.GetVisitRecordList(anaysisQuery, emailAccount, out recordcount);
            returnObj.RecordCount = recordcount;
            returnObj.Status = Business.WebApi.Models.ServerStatus.Success;
            return Request.CreateResponse<Business.WebApi.Models.ResultObject<List<Business.Core.VisitRecord.WebSiteAnalysisInfo>>>(HttpStatusCode.OK, returnObj);
        }

        [HttpGet]
        public HttpResponseMessage GetLanguageTypeList()
        {
            var returnObj = new Business.WebApi.Models.ResultObject<List<string>>();
            List<string> retList = new List<string>();

            //取用户登录成功后保存的session
            string emailAccount = HttpContext.Current.Session["LoginAccount"].ToString();
            Business.Core.Manager manager = null;
            manager = ManageService.GetManagerByUsername(emailAccount);
            if (manager != null)
            {
                List<Business.Core.ManagerMainSite> mangerMainSiteList =  ManagerMainSiteService.GetManagerMainSitesByManagerId(manager.Id);
                foreach (var tmp in mangerMainSiteList)
                {
                    retList.Add(tmp.LanguageName);
                }
            }

            returnObj.ReturnData = retList;
            returnObj.Status = Business.WebApi.Models.ServerStatus.SearchSuccess;
            return Request.CreateResponse<Business.WebApi.Models.ResultObject<List<string>>>(HttpStatusCode.OK, returnObj);
        }
    }
}