using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Routing;
using Microsoft.AspNet.FriendlyUrls;
using BGSApps.Net.Model.Menu;
using BGSApps.Net.Security.Security;

namespace GatePassWeb
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.EnableFriendlyUrls();
            routes.MapPageRoute("", "auth-login", "~/Login.aspx");
            routes.MapPageRoute("", "register-new-user", "~/RegisterUser.aspx");
            routes.MapPageRoute("", "lock-screen", "~/LockScreen.aspx");
            foreach (var config in SessionSecurity.getStartConfigRoute())
            {
                if (config.Bgsm_Menu_Vurl != null)
                    routes.MapPageRoute("", config.Bgsm_Menu_Vurl, config.Bgsm_Menu_Purl);
            }
        }
    }
}
