using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Business.WebApi.Htmls.CommonForm
{
    public partial class WebSiteAnalysis : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["LoginAccount"] ==null || String.IsNullOrWhiteSpace(Session["LoginAccount"].ToString()))
                {
                    Response.Redirect("LoginForm.aspx");
                    return;
                }

            }
        }
    }
}