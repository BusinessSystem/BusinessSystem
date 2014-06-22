using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Business.Web.Controllers
{
    public class SharedController : AdminBaseController
    {
        
        [ChildActionOnly]
        public ActionResult _Navbar()
        {
            return PartialView(CurrentManager);
        }
        [ChildActionOnly]
        public ActionResult _NavList()
        {
            return PartialView();
        }
    }
}
