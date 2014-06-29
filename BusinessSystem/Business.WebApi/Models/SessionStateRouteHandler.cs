using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace Business.WebApi.Models
{
    /****************************************************************************************************************
     *Descripttion: So know we need only to plug our newly created SessionControllerHandler to routing workflow. 
     * To do it we need to implement module which will inherits IRouteHandler interface and in GetHttpHandler method 
     * just return new instance of session controller handler.
     ****************************************************************************************************************/
    public class SessionStateRouteHandler : IRouteHandler
    {
        IHttpHandler IRouteHandler.GetHttpHandler(RequestContext requestContext)
        {
            return new SessionableControllerHandler(requestContext.RouteData);
        }
    }
}