using BGSApps.Net.Security.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GatePassWeb
{
    public partial class LockScreen : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("/auth-login");
            }
            else
            {
                if (IsPostBack)
                {
                    string password = Request.Form["txtlockpswd"].ToString();
                    bool canLogin = Pp3UserService.cekPasswordWhenLock(password);
                    if (canLogin)
                    {
                        Session["islock"] = null;
                        if (Request.Cookies["lastpath"] != null)
                        {
                            string value = Request.Cookies["lastpath"].Value;
                            Response.Redirect(value);
                        }
                    }
                    else
                    {
                        Response.Write("<script type='text/javascript'>alert('Invalid password');</script>");
                    }
                }
                else
                {
                    if (Session["islock"] == null)
                        Session["islock"] = true;
                }
            }
        }
    }
}