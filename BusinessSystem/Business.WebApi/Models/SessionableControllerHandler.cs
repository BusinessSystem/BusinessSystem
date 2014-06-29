using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Web.Http.WebHost;
using System.Web.Routing;

namespace Business.WebApi.Models
{
    /******************************************************************************************************
     * Description:To enforce session in WebApi we need to use IRequiresSessionState markable attribute which is only 
     * need for notifying ASP environment about providing session state on a specific module.
     ******************************************************************************************************/
    public class SessionableControllerHandler : HttpControllerHandler, IRequiresSessionState
    {
        public SessionableControllerHandler(RouteData routeData): base(routeData)
        { 
        }
    }
}