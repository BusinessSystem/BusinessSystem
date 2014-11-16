using System.Web;
using System.Web.Mvc;
using Business.Utils;

namespace Business.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

       
    }

    public class ErrorAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContent) {
            var a = 1;
            LogHelper.Error("", filterContent.Exception);
        
        }

    }
}