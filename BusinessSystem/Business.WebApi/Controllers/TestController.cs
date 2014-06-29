using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Business.Serives;
using Business.WebApi.Models;
using System.Web;

namespace Business.WebApi.Controllers
{
    public class TestController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage SaveData(TestQuery query)
        {
            string host_ip = Request.GetClientIpAddress();
            string email = query.email;
            string content = query.content;
            string productName = query.productName ;
            string yourName = query.yourName ;
            string company = query.company ;
            string tel = query.tel;
            string msn = query.msn ;
            string language = query.language ;//访问语言
            string recievedId = query.recievedId ;//接受者
            EnquiryService.EnquirySave(host_ip, email, content, productName, yourName, company, tel, msn);
            List<string> retList = new List<string>();
            retList.Add(host_ip);
            retList.Add(email);
            retList.Add(content);
            retList.Add(productName);
            retList.Add(yourName);
            retList.Add(company);
            retList.Add(tel);
            retList.Add(msn);
            retList.Add(language);
            retList.Add(recievedId);
            var returnObj = new ResultObject<List<string>>();
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

            // Self-hosting. Needs reference to System.ServiceModel.dll. 
            //if (request.Properties.ContainsKey(RemoteEndpointMessage))
            //{
            //    dynamic remoteEndpoint = request.Properties[RemoteEndpointMessage];
            //    if (remoteEndpoint != null)
            //    {
            //        return remoteEndpoint.Address;
            //    }
            //}

            //// Self-hosting using Owin. Needs reference to Microsoft.Owin.dll. 
            //if (request.Properties.ContainsKey(OwinContext))
            //{
            //    dynamic owinContext = request.Properties[OwinContext];
            //    if (owinContext != null)
            //    {
            //        return owinContext.Request.RemoteIpAddress;
            //    }
            //}

            return "未知ip";
        }
    }



}