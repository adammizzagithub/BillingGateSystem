using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BGSApps.Net.Security.Security;

namespace GatePassWeb
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string signOut = Request.QueryString["sLogOut"] == null ? "" : Request.QueryString["sLogOut"];
            /* LOG OUT MODE */
            if (signOut.Equals("sgnout"))
            {
                if (IsPostBack)
                {
                    string username = Request.Form["txtusername"].ToString();
                    string password = Request.Form["txtpassword"].ToString();
                    bool canLogin = Pp3UserService.isValidCanLogin(username, password, this.Page);
                    //bool canLogin = true;
                    if (canLogin)
                    {
                        Session.Timeout = 120;
                        Response.Redirect("/dashboard");
                    }
                    else
                    {
                        lblMessage.Text = "<div class='alert alert-danger alert-dismissable'>" +
                       "<button type='button' class='close' data-dismiss='alert' aria-hidden='true'>×</button>" +
                       "<h4><i class='icon fa fa-ban'></i> User tidak memiliki Otorisasi</h4>" +
                       "</div>";
                    }
                }
                else
                {
                    int res = SessionSecurity.updateLastLoginUser(Session["UserName"].ToString());
                    if (res == 1)
                    {
                        Session.Clear();
                        signOut = "";
                    }
                }
            }
            if (Session["UserName"] != null)
            {
                Response.Redirect("/dashboard");
            }
            else
            {
                if (IsPostBack)
                {
                    string username = Request.Form["txtusername"].ToString();
                    string password = Request.Form["txtpassword"].ToString();
                    bool canLogin = Pp3UserService.isValidCanLogin(username, password, this.Page);
                    //bool canLogin = true;
                    if (canLogin)
                    {
                        Session.Timeout = 120;
                        Response.Redirect("/dashboard");
                    }
                    else
                    {
                        lblMessage.Text = "<div class='alert alert-danger alert-dismissable'>" +
                       "<button type='button' class='close' data-dismiss='alert' aria-hidden='true'>×</button>" +
                       "<h4><i class='icon fa fa-ban'></i> User tidak memiliki Otorisasi</h4>" +
                       "</div>";
                    }
                }
            }
        }
    }
}