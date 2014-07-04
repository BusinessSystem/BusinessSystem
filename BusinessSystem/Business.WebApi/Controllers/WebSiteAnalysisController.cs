using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Business.WebApi.Models;
using System.Web;

namespace Business.WebApi.Controllers
{
    public class WebSiteAnalysisController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage GetInfoListByIp(WebSiteAnalysisQuery query)
        {
            /*****根据客户的邮箱账号，网站语言，和输入查询的ip来查询出访问信息*****/
            //取用户登录成功后保存的session
            string emailAccount = HttpContext.Current.Session["LoginAccount"].ToString();
            //获取传过来的的网站语言
            string language = query.Language;

            var returnObj = new ResultObject<List<WebSiteAnalysisInfo>>();
            List<WebSiteAnalysisInfo> retList = new List<WebSiteAnalysisInfo>();
            for (int i = 2; i < 15; ++i)
            {
                WebSiteAnalysisInfo model = new WebSiteAnalysisInfo();
                model.VIp = "192.168.1."+i.ToString();
                model.ProductName = "http://www.baidu.com";
                model.VCountry = "中国";
                model.VTime = "2014-07-03";

                retList.Add(model);
            }

            returnObj.Status = ServerStatus.Success;
            returnObj.ReturnData = retList;
            returnObj.RecordCount = 13;
            return Request.CreateResponse<ResultObject<List<WebSiteAnalysisInfo>>>(HttpStatusCode.OK, returnObj);
        }

        [HttpGet]
        public HttpResponseMessage GetLanguageTypeList()
        {
            var returnObj = new ResultObject<List<string>>();
            List<string> retList = new List<string>();
            retList.Add("英语");
            retList.Add("俄语");
            retList.Add("韩语");
            retList.Add("日语");

            returnObj.ReturnData = retList;
            returnObj.Status = ServerStatus.SearchSuccess;
            return Request.CreateResponse<ResultObject<List<string>>>(HttpStatusCode.OK, returnObj);
        }
    }
}