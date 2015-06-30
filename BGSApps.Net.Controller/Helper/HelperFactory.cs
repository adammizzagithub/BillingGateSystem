using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;

namespace BGSApps.Net.Controller.Helper
{
    public static class HelperFactory
    {
        public static string ResolveUrl(string url)
        {
            Page page = HttpContext.Current.Handler as Page;

            if (page == null)
            {
                return url;
            }

            return (page).ResolveUrl(url);
        }
    }
}
