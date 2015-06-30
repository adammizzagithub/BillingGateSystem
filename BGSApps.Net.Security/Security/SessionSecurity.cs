using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BGSApps.Net.Model.Menu;
using BGSApps.Net.DapperFactory;

namespace BGSApps.Net.Security.Security
{
    public static class SessionSecurity
    {
        public static List<BgsmMenu> getStartConfigRoute()
        {
            List<BgsmMenu> routeStarts = new List<BgsmMenu>();
            using (var database = new DapperLabFactory())
            {
                routeStarts = database.GetListNoParam<BgsmMenu>("select * from bgsm_menu order by bgsm_menu_urut asc").ToList();
            }
            return routeStarts;
        }
    }
}
