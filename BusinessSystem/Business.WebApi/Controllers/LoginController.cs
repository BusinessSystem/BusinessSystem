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
    public class LoginController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage CheckLogin(LoginQuery query)
        {
            var returnObj = new ResultObject<List<string>>();
            List<string> retList = new List<string>();

            //检验验证码正确与否
            string valiad_right = HttpContext.Current.Session["code"] as string;

            if (string.Compare(query.valiad, valiad_right) == 0)
            {
                //检验用户名正确与否
                if (true)
                {
                    //进行验证

                    //设置验证成功,保存用户名到session中
                    HttpContext.Current.Session["LoginAccount"] = "lingwu@163.com";
                    returnObj.Status = ServerStatus.Success;
                }
                else
                {
                    returnObj.Status = ServerStatus.SearchFailed;
                }

            }
            
            
            returnObj.ReturnData = retList;
            
            return Request.CreateResponse<ResultObject<List<string>>>(HttpStatusCode.OK, returnObj);
        }
    }
}