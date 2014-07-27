using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Business.Serives;
using System.Web;

namespace Business.WebApi.Controllers
{
    public class VisitorRecordController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage visitorRecordSave([FromUri]Business.WebApi.Models.VisitorRecordSaveQuery query)
        {
            string purchaserIp = Request.GetClientIpAddress();
            string purchaserProduct = string.Empty;
            string language = string.Empty;
            string purchaserDomain = string.Empty;
            string targetEmail = string.Empty;

            if (!string.IsNullOrWhiteSpace(query.PurchaserProduct))
            {
                purchaserProduct = query.PurchaserProduct;
            }
            if (!string.IsNullOrWhiteSpace(query.Language))
            {
                language = query.Language;
            }
            if (!string.IsNullOrWhiteSpace(query.PurchaserDomain))
            {
                purchaserDomain = query.PurchaserDomain;
            }
            if (!string.IsNullOrWhiteSpace(query.TargetEmail))
            {
                targetEmail = query.TargetEmail;
            }

            string basePath = System.AppDomain.CurrentDomain.BaseDirectory;//纯真IP数据文件路径..
            QQWry.NET.QQWryLocator2 qqWry2 = new QQWry.NET.QQWryLocator2(basePath + "\\Models\\QQWry.dat");
            QQWry.NET.IPLocation ip2 = qqWry2.Query(purchaserIp);  //查询一个IP地址
            string puchaserCountry = ip2.Country;

            Business.Serives.VisitRecordService.VisitRecordSave(purchaserIp, purchaserProduct, language, puchaserCountry, purchaserDomain, targetEmail);
            List<string> retList = new List<string>();
            var returnObj = new Business.WebApi.Models.ResultObject<List<string>>();
            //retList.Add("save record success!");
            returnObj.ReturnData = retList;
            returnObj.Status = Business.WebApi.Models.ServerStatus.SaveSuccess;
            return Request.CreateResponse<Business.WebApi.Models.ResultObject<List<string>>>(HttpStatusCode.OK, returnObj);
        }

        [HttpPost]
        public HttpResponseMessage GetInfoListByIp(Business.Core.VisitRecord.WebSiteAnalysisQuery anaysisQuery)
        {
            /*****根据客户的邮箱账号，网站语言，和输入查询的ip来查询出访问信息*****/
            //取用户登录成功后保存的session
            string emailAccount = HttpContext.Current.Session["LoginAccount"].ToString();
            //获取传过来的的网站语言
            string language = anaysisQuery.Language;

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