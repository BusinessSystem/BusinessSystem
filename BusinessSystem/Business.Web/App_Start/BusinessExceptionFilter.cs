using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using Business.Utils;

namespace Business.Web.App_Start
{
    public class BusinessExceptionFilter:ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnException(actionExecutedContext);
            LogHelper.Error("apierror", actionExecutedContext.Exception);
        }
    }
}