using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Business.WebApi.Models;

namespace Business.WebApi.Controllers
{
    public class WebSiteAnalysisController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage GetInfoListByIp(WebSiteAnalysisQuery query)
        {
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
    }
}