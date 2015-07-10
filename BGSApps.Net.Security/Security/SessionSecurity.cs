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
        public static int updateLastLoginUser(string username)
        {
            int res = 0;
            using (var database = new DapperLabFactory())
            {
                res = database.UpdateOrDeleteRecord("update bgsm_user set bgsm_user_lastlogin = :lastlogin where bgsm_user_username=:username", new { lastlogin = DateTime.Now, username = username });
            }
            return res;
        }
    }
}
