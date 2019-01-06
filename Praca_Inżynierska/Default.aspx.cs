using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Praca_Inżynierska
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            IfLogged();
        }
        protected void IfLogged()
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Account/Manage");
            }
            else
            {
                Response.Redirect("~/Account/Login");
            }
        }
    }
}