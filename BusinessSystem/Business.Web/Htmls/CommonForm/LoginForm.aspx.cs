using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Business.Web.Htmls.CommonForm
{
    public partial class LoginForm : System.Web.UI.Page
    {
        protected string language = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["code"] = "";
            if (!IsPostBack)
            {
                language = Server.UrlDecode(Request["languageType"]);
            }
        }
    }
}