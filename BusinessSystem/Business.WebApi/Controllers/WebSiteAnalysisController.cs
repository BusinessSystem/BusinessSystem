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
        [HttpGet]
        public HttpResponseMessage GetLanguageTypeList()
        {
            var returnObj = new Business.WebApi.Models.ResultObject<List<string>>();
            List<string> retList = new List<string>();

            //取用户登录成功后保存的session
            string emailAccount = HttpContext.Current.Session["LoginAccount"].ToString();
            Business.Core.Manager manager = null;
            manager = ManageService.GetManagerByUsername(emailAccount);

            if (manager.ParentId != 0)
            {
                manager = ManageService.GetManagerById(manager.ParentId);
            }

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

        /// <summary>
        /// 获取公司和当前查询的市场的客户数和访问产品数
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage GetCompanyInfo(Business.WebApi.Models.WebSiteAnalysisQuery query)
        {
            var returnObj = new Business.WebApi.Models.ResultObject<Business.WebApi.Models.CompanyAndSummaryInfo>();
            Business.WebApi.Models.CompanyAndSummaryInfo model = new Models.CompanyAndSummaryInfo();

            //取用户登录成功后保存的session
            string emailAccount = HttpContext.Current.Session["LoginAccount"].ToString();
            Business.Core.Manager manager = null;
            manager = ManageService.GetManagerByUsername(emailAccount);
            if (manager.ParentId != 0)
            {
                manager = ManageService.GetManagerById(manager.ParentId);
            }

            if (manager != null)
            {
                model.CompanyName = manager.Company;//公司名
                model.PeopleNum = VisitRecordService.GetIpCount(query.Language, manager.UserName);
                model.ProductCount = VisitRecordService.GetVisitRecordCount(query.Language, manager.UserName);
            }

            returnObj.ReturnData = model;
            returnObj.Status = Business.WebApi.Models.ServerStatus.SearchSuccess;
            return Request.CreateResponse<Business.WebApi.Models.ResultObject<Business.WebApi.Models.CompanyAndSummaryInfo>>(HttpStatusCode.OK, returnObj);
        }
    }
}