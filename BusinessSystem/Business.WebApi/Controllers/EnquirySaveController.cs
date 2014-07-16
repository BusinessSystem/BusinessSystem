using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Business.Serives;
using Business.WebApi.Models;
using System.Web;
using QQWry.NET;

namespace Business.WebApi.Controllers
{
    public class EnquirySaveController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage SaveData([FromUri]EnquirySavaQuery query)
        {
            string host_ip = Request.GetClientIpAddress();

            string basePath = System.AppDomain.CurrentDomain.BaseDirectory;//纯真IP数据文件路径..
            QQWry.NET.QQWryLocator2 qqWry2 = new QQWry.NET.QQWryLocator2(basePath+"\\Models\\QQWry.dat");
            QQWry.NET.IPLocation ip2 = qqWry2.Query(host_ip);  //查询一个IP地址

            string country = ip2.Country;
            string email = query.email;
            string content = query.content;
            string productName = query.productName ;
            string yourName = query.yourName ;
            string company = query.company ;
            string tel = query.tel;
            string msn = query.msn ;
            string language = query.language ;//访问语言
            string recievedId = query.recievedId ;//接受者
            EnquiryService.EnquirySave(host_ip, email, content, productName, yourName, company, tel, msn,language,country,recievedId);
            List<string> retList = new List<string>();
            var returnObj = new ResultObject<List<string>>();
            retList.Add("leave message success!");
            returnObj.ReturnData = retList;
            returnObj.Status = ServerStatus.SaveSuccess;
            return Request.CreateResponse<ResultObject<List<string>>>(HttpStatusCode.OK, returnObj);
        }

        
    }


    public static class HttpRequestMessageExtensions
    {
        private const string HttpContext = "MS_HttpContext";
        private const string RemoteEndpointMessage = "System.ServiceModel.Channels.RemoteEndpointMessageProperty";
        private const string OwinContext = "MS_OwinContext";

        public static string GetClientIpAddress(this HttpRequestMessage request)
        {
            // Web-hosting. Needs reference to System.Web.dll
            if (request.Properties.ContainsKey(HttpContext))
            {
                dynamic ctx = request.Properties[HttpContext];
                if (ctx != null)
                {
                    return ctx.Request.UserHostAddress;
                }
            }
            return "未知ip";
        }
    }



}