using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Business.Core;
using Business.Web.Framework;

namespace Business.Web.Controllers
{
    public class AdminBaseController : Controller
    {
        private static bool? _disableAdmin;

        public bool DisableAdmin
        {
            get
            {
                bool result;
                if (_disableAdmin == null)
                {
                    string tmp = ConfigurationManager.AppSettings["DisableAdmin"];

                    if (bool.TryParse(tmp, out result) == false)
                    {
                        result = false;
                    }
                    _disableAdmin = result;
                    return result;
                }
                result = (bool)_disableAdmin;
                return result;
            }
        }
        
        public Manager CurrentManager { get; set; }

        public bool IsManagerLogin()
        {
            return CookieHelper.GetManagerFromCookie() != null;
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //获取管理员登录信息
            CurrentManager = CookieHelper.GetManagerFromCookie();

            string actionName = filterContext.RouteData.Values["Action"].ToString();
            string controllerName = filterContext.RouteData.Values["Controller"].ToString() + "Controller";
            //加载权限设定
            //if (PermissionManager.IsInit == false)
            //    PermissionManager.Init();

            //检查登录不需要判断
            if (actionName == "CheckUser")
            {
                base.OnActionExecuting(filterContext);
                return;
            }
            //检查登录状态
            if (IsManagerLogin())
            {
                base.OnActionExecuting(filterContext);
            }
            else
            {
                filterContext.Result = Redirect("/index.html");
            }

        }

        protected override void OnException(ExceptionContext filterContext)
        {
            Exception ex = filterContext.Exception;
            string url = string.Empty;
            if (Request != null)
                url = Request.RawUrl;
           // ServerLogger.Error(string.Format("------------------------\r\n管理程序请求错误，描述：{0},\r\n请求地址{1},\r\n详细{2}\r\n", ex.Message, url, ex.StackTrace));
        }

        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (DisableAdmin)
            {
                throw new Exception("非法请求");
            }
            base.OnAuthorization(filterContext);
        }

    }
}
