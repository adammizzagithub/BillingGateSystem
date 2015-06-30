using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BGSApps.Net.Model.Menu;
using BGSApps.Net.DapperFactory;
using BGSApps.Net.Controller.Menu;

namespace BGSApps.Net.Controller.Core
{
    public static class MasterPageAccessCtrl
    {
        public static string getBindliteralMenu()
        {
            string literalResult = string.Empty;
            foreach (var menu in getAllMenu())
            {
                if (menu.Childs.Count > 0)
                {
                    literalResult += "<li class='treeview'>";
                    literalResult += "<a href='#'><i class='" + menu.Bgsm_Menu_Icon + "'></i> <span>" + menu.Bgsm_Menu_Nama + "</span> <i class='fa fa-angle-left pull-right'></i></a>";
                    literalResult += getBindLiteralChild(menu.Childs);
                    literalResult += "</li>";
                }
                else
                {
                    literalResult += "<li><a href='/" + menu.Bgsm_Menu_Vurl + "'><i class='" + menu.Bgsm_Menu_Icon + "'></i><span>" + menu.Bgsm_Menu_Nama + "</span></a></li>";
                }
            }
            return literalResult;
        }
        public static string getBindLiteralChild(List<BgsmMenu> childMenus)
        {
            string literalResult = "<ul class='treeview-menu' style='display: none;'>";
            foreach (var menuchild in childMenus)
            {
                if (menuchild.Childs.Count > 0)
                {
                    literalResult += "<li>";
                    literalResult += "<a href='#'><i class='" + menuchild.Bgsm_Menu_Icon + "'></i> <span>" + menuchild.Bgsm_Menu_Nama + "</span> <i class='fa fa-angle-left pull-right'></i></a>";
                    literalResult += getBindLiteralChild(menuchild.Childs);
                    literalResult += "</li>";
                }
                else
                {
                    literalResult += "<li><a href='/" + menuchild.Bgsm_Menu_Vurl + "'><i class='" + menuchild.Bgsm_Menu_Icon + "'></i><span>" + menuchild.Bgsm_Menu_Nama + "</span></a></li>";
                }
            }
            literalResult += "</ul>";
            return literalResult;
        }
        public static List<BgsmMenu> getAllMenu()
        {
            List<BgsmMenu> menus = new List<BgsmMenu>();
            using (var database = new DapperLabFactory())
            {
                menus = database.GetListWithParam<BgsmMenu>("select * from bgsm_menu where bgsm_menu_parent = :param1 order by bgsm_menu_urut asc", new { param1 = -1 }).ToList();
                getRecursiveMenu(menus);
            }
            return menus;
        }
        public static void getRecursiveMenu(List<BgsmMenu> menus)
        {
            foreach (var item in menus)
            {
                List<BgsmMenu> nestedChilds = new List<BgsmMenu>();
                using (var database = new DapperLabFactory())
                {
                    nestedChilds = database.GetListWithParam<BgsmMenu>("select * from bgsm_menu where bgsm_menu_parent = :param1 order by bgsm_menu_urut asc", new { param1 = item.Bgsm_Menu_Id }).ToList();
                }
                item.Childs = nestedChilds;
                if (item.Childs.Count != 0)
                    getRecursiveMenu(item.Childs);
            }
        }
    }
}
