using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Praca_Inżynierska
{
    public partial class SiteMaster : MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;

        protected void Page_Init(object sender, EventArgs e)
        {
            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            checkRole();

            checkIfLogged();
            
        }

        protected void checkIfLogged()
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                checkRole();
            }
            else
            {
                planNav.Visible = false;
                importnav.Visible = false;
                grupynav.Visible = false;
                zajecianav.Visible = false;

            }
        }
        protected void checkRole()
        {
            String userName = HttpContext.Current.User.Identity.Name;
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT Rola FROM AspNetUsers WHERE UserName = @userName "))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@userName", userName);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            if (reader.GetInt32(reader.GetOrdinal("Rola")) == 1)
                            {
                                planNav.Visible = false;
                                importnav.Visible = false;
                                grupynav.Visible = false;
                                zajecianav.Visible = false;
                            }
                            else if (reader.GetInt32(reader.GetOrdinal("Rola")) == 2)
                            {
                                planNav.Visible = false;
                                importnav.Visible = true;
                                grupynav.Visible = true;
                                zajecianav.Visible = false;
                            }
                            else if (reader.GetInt32(reader.GetOrdinal("Rola")) == 3)
                            {
                                planNav.Visible = true;
                                importnav.Visible = false;
                                grupynav.Visible = false;
                                zajecianav.Visible = true;
                            }
                            else if (reader.GetInt32(reader.GetOrdinal("Rola")) == 4)
                            {
                                planNav.Visible = false;
                                importnav.Visible = false;
                                grupynav.Visible = false;
                                zajecianav.Visible = true;
                            }
                            else if (reader.GetInt32(reader.GetOrdinal("Rola")) == 5)
                            {
                                planNav.Visible = false;
                                importnav.Visible = false;
                                grupynav.Visible = false;
                                zajecianav.Visible = true;
                            }
                        }
                    }

                }
            }
        }

        protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }
    }

}