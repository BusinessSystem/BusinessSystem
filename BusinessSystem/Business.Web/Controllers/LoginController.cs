using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using Business.Web.Models;
using Business.Serives;
using Business.Core;

namespace Business.Web.Controllers
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
                Manager manager = null;
                string result = ManageService.Login(query.username, query.userpwd, out manager);
                if (result == ResponseCode.Ok)//正确
                {
                    //设置验证成功,保存用户名到session中
                    HttpContext.Current.Session["LoginAccount"] = query.username;
                    returnObj.Status = ServerStatus.Success;
                }
                else
                {
                    returnObj.Status = ServerStatus.SearchFailed;
                }

            }
            else
            {
                returnObj.Status = ServerStatus.SearchFailed;
            }


            returnObj.ReturnData = retList;

            return Request.CreateResponse<ResultObject<List<string>>>(HttpStatusCode.OK, returnObj);
        }
    }
}