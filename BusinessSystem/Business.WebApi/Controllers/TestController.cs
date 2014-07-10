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
        [HttpGet]
        //public HttpResponseMessage SaveData(TestQuery query)
        public HttpResponseMessage SaveData([FromUri]TestDemo query)
        {
            string host_ip = Request.GetClientIpAddress();
            string email = query.email;
            //string content = query.content;
            //string productName = query.productName ;
            //string yourName = query.yourName ;
            //string company = query.company ;
            //string tel = query.tel;
            //string msn = query.msn ;
            string language = query.language ;//访问语言
            //string recievedId = query.recievedId ;//接受者
            //EnquiryService.EnquirySave(host_ip, email, content, productName, yourName, company, tel, msn);
            List<string> retList = new List<string>();
            retList.Add(host_ip);
            retList.Add(email);
            //retList.Add(content);
            //retList.Add(productName);
            //retList.Add(yourName);
            //retList.Add(company);
            //retList.Add(tel);
            //retList.Add(msn);
            retList.Add(language);
            //retList.Add(recievedId);
            var returnObj = new ResultObject<List<string>>();
            returnObj.ReturnData = retList;
            returnObj.Status = ServerStatus.SaveSuccess;
            return Request.CreateResponse<ResultObject<List<string>>>(HttpStatusCode.OK, returnObj);
        }

    }

}