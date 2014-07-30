using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Business.Serives;

namespace Business.WebApi.Controllers
{
    public class CompanyInfoController : ApiController
    {
        /// <summary>
        /// 获取公司和当前查询的市场的客户数和访问产品数
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetCompanyInfo(string LoginAccount, string Language)
        {
            var returnObj = new Business.WebApi.Models.ResultObject<Business.WebApi.Models.CompanyAndSummaryInfo>();
            Business.WebApi.Models.CompanyAndSummaryInfo model = new Models.CompanyAndSummaryInfo();

            //取用户登录成功后保存的session
            Business.Core.Manager manager = null;
            manager = ManageService.GetManagerByUsername(LoginAccount);
            if (manager.ParentId != 0)
            {
                manager = ManageService.GetManagerById(manager.ParentId);
            }

            if (manager != null)
            {
                model.CompanyName = manager.Company;//公司名
                model.PeopleNum = VisitRecordService.GetIpCount(Language, manager.UserName);
                model.ProductCount = VisitRecordService.GetVisitRecordCount(Language, manager.UserName);
            }

            returnObj.ReturnData = model;
            returnObj.Status = Business.WebApi.Models.ServerStatus.SearchSuccess;
            return Request.CreateResponse<Business.WebApi.Models.ResultObject<Business.WebApi.Models.CompanyAndSummaryInfo>>(HttpStatusCode.OK, returnObj);
        }
    }
}