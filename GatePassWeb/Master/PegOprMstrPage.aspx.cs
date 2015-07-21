using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GatePassWeb.Master
{
    public partial class PegOprMstrPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("/auth-login");
            }
            if (Convert.ToBoolean(Session["islock"]) == true)
            {
                Response.Redirect("/lock-screen");
            }
            else
            {

            }
        }
    }
}