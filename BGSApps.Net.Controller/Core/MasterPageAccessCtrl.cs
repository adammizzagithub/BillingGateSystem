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
        public static string getBindliteralMenu(int roleAsId)
        {
            string literalResult = string.Empty;
            foreach (var menu in getAllMenu(roleAsId))
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
        public static List<BgsmMenu> getAllMenu(int roleAsId)
        {
            List<BgsmMenu> menus = new List<BgsmMenu>();
            using (var database = new DapperLabFactory())
            {
                menus = database.GetListWithParam<BgsmMenu>("select BM.* from " +
                                                            "BGSM_AKSES_CONTROL BAC," +
                                                            "BGSM_MENU BM " +
                                                            "where " +
                                                            "BM.BGSM_MENU_ID = BAC.BGSM_CONTROL_MENUID AND " +
                                                                "BAC.BGSM_CONTROL_AKSESID = :roleid AND BM.BGSM_MENU_PARENT = :parent " +
                                                                "ORDER BY BM.BGSM_MENU_URUT asc", new { roleid = roleAsId, parent = -1 }).ToList();
                getRecursiveMenu(roleAsId, menus);
            }
            return menus;
        }
        public static void getRecursiveMenu(int roleAsId, List<BgsmMenu> menus)
        {
            foreach (var item in menus)
            {
                List<BgsmMenu> nestedChilds = new List<BgsmMenu>();
                using (var database = new DapperLabFactory())
                {
                    nestedChilds = database.GetListWithParam<BgsmMenu>("select BM.* from " +
                                                            "BGSM_AKSES_CONTROL BAC," +
                                                            "BGSM_MENU BM " +
                                                            "where " +
                                                            "BM.BGSM_MENU_ID = BAC.BGSM_CONTROL_MENUID AND " +
                                                                "BAC.BGSM_CONTROL_AKSESID = :roleid AND BM.BGSM_MENU_PARENT = :parent " +
                                                                "ORDER BY BM.BGSM_MENU_URUT asc", new { roleid = roleAsId, parent = item.Bgsm_Menu_Id }).ToList();
                }
                item.Childs = nestedChilds;
                if (item.Childs.Count != 0)
                    getRecursiveMenu(roleAsId, item.Childs);
            }
        }
    }
}
