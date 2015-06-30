using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using BGSApps.Net.Model.Menu;
using BGSApps.Net.DapperFactory;
using BGSApps.Net.Controller.Helper;

namespace BGSApps.Net.Controller.Menu
{
    public class ListOfJsonMenuConfig
    {
        public virtual List<JsonParamMenu> ListConfig { set; get; }
    }
    public static class MenuSettingCtrl
    {
        #region CRUD
        public static int CreateNewMenu(string obj)
        {
            int res = 0;
            BgsmMenu bgsmMenu = JsonConvert.DeserializeObject<BgsmMenu>(obj);
            using (var database = new DapperLabFactory())
            {
                res = database.InsertRecord(new
                {
                    BGSM_MENU_NAMA = bgsmMenu.Bgsm_Menu_Nama,
                    BGSM_MENU_VURL = bgsmMenu.Bgsm_Menu_Vurl,
                    BGSM_MENU_PURL = bgsmMenu.Bgsm_Menu_Purl,
                    BGSM_MENU_ICON = bgsmMenu.Bgsm_Menu_Icon,
                    CREATION_BY = bgsmMenu.Creation_By,
                    BGSM_MENU_PARENT = -1
                }, "BGSM_MENU"
                 , "BGSM_MENU_NAMA,BGSM_MENU_VURL,BGSM_MENU_PURL,BGSM_MENU_ICON,CREATION_BY,BGSM_MENU_PARENT");
            }
            return res;
        }
        public static int UpdateMenu(string obj)
        {
            int res = 0;
            BgsmMenu bgsmMenu = JsonConvert.DeserializeObject<BgsmMenu>(obj);
            using (var database = new DapperLabFactory())
            {
                res = database.UpdateOrDeleteRecord("update bgsm_menu set BGSM_MENU_NAMA=:nama," +
                    "BGSM_MENU_VURL=:vurl," +
                    "BGSM_MENU_PURL=:purl," +
                    "BGSM_MENU_ICON=:icon,LAST_UPDATE_DATE=:nowdate,LAST_UPDATE_BY=:nowby where BGSM_MENU_ID=:menuid",
                    new
                    {
                        nama = bgsmMenu.Bgsm_Menu_Nama,
                        vurl = bgsmMenu.Bgsm_Menu_Vurl,
                        purl = bgsmMenu.Bgsm_Menu_Purl,
                        icon = bgsmMenu.Bgsm_Menu_Icon,
                        nowdate = DateTime.Now,
                        nowby = bgsmMenu.Last_Update_By,
                        menuid = bgsmMenu.Bgsm_Menu_Id
                    });
            }
            return res;
        }
        public static int DeleteMenu(int menuId)
        {
            int res = 0;
            using (var database = new DapperLabFactory())
            {
                res = database.UpdateOrDeleteRecord("delete from bgsm_menu where bgsm_menu_id=:bgsm_menu_id", new { bgsm_menu_id = menuId });
            }
            return res;
        }
        public static void ExcecuteUpdateConfig(int nourut, int parentid, int menuid, string userid)
        {
            using (var database = new DapperLabFactory())
            {
                database.UpdateOrDeleteRecord("update bgsm_menu set " +
                    "BGSM_MENU_URUT=:nourut," +
                    "BGSM_MENU_PARENT=:parentid," +
                    "LAST_UPDATE_DATE=:nowdate,LAST_UPDATE_BY=:nowby where BGSM_MENU_ID=:menuid",
                    new
                    {
                        nourut = nourut,
                        parentid = parentid,
                        nowdate = DateTime.Now,
                        nowby = userid,
                        menuid = menuid
                    });
            }
        }
        public static void SaveMenuConfiguration(string jsonstring, string userid)
        {
            ListOfJsonMenuConfig configs = JsonConvert.DeserializeObject<ListOfJsonMenuConfig>(jsonstring);
            int i = 0;
            foreach (var config in configs.ListConfig)
            {
                ExcecuteUpdateConfig(i, -1, config.id, userid);
                if (config.children != null)
                {
                    SaveMenuChildConfiguration(config.children, config.id, userid);
                }
                i++;
            }
        }
        public static void SaveMenuChildConfiguration(List<JsonParamMenu> childConfig, int parentid, string userid)
        {
            for (int j = 0; j < childConfig.Count; j++)
            {
                ExcecuteUpdateConfig(j, parentid, childConfig[j].id, userid);
                if (childConfig[j].children != null)
                    SaveMenuChildConfiguration(childConfig[j].children, childConfig[j].id, userid);
            }
        }
        #endregion
        #region LIST PARENT CHILD MENU
        public static string getBindliteralMenu()
        {
            string literalResult = string.Empty;
            literalResult += " <div class='dd'>";
            literalResult += " <ol class='dd-list'>";
            foreach (var menu in getAllMenu())
            {
                if (menu.Childs.Count > 0)
                {
                    literalResult += "<li class='dd-item' data-id=" + menu.Bgsm_Menu_Id + " data-nama='" + menu.Bgsm_Menu_Nama + "'>";
                    literalResult += "<div class='dd-handle'>" + menu.Bgsm_Menu_Nama + "</div>";
                    literalResult += getBindLiteralChild(menu.Childs);
                }
                else
                {
                    literalResult += "<li class='dd-item' data-id=" + menu.Bgsm_Menu_Id + " data-nama='" + menu.Bgsm_Menu_Nama + "'><div class='dd-handle'>" + menu.Bgsm_Menu_Nama + "</div><div class=\"dd-actions\"><a href=\"javascript:;\" class=\"edit-menu\" menu-id=" + menu.Bgsm_Menu_Id + " title=\"Edit\"><img src=\"" + HelperFactory.ResolveUrl("AdminLte/img/edit.png") + " \" alt=\"Edit\"></a>&nbsp;&nbsp;<a href=\"javascript:;\" class=\"delete-menu\" menu-id=" + menu.Bgsm_Menu_Id + " title=\"Delete\"><img src=\"" + HelperFactory.ResolveUrl("AdminLte/img/cross.png") + " \" alt=\"Delete\"></a></div></li>";
                }
            }
            literalResult += " </ol>";
            literalResult += " </div>";
            return literalResult;
        }
        public static string getBindLiteralChild(List<BgsmMenu> childMenus)
        {
            string literalResult = "<ol class='dd-list'>";
            foreach (var menuchild in childMenus)
            {
                if (menuchild.Childs.Count > 0)
                {
                    literalResult += "<li class='dd-item' data-id=" + menuchild.Bgsm_Menu_Id + " data-nama='" + menuchild.Bgsm_Menu_Nama + "'>";
                    literalResult += "<div class='dd-handle'>" + menuchild.Bgsm_Menu_Nama + "</div>";
                    literalResult += getBindLiteralChild(menuchild.Childs);
                }
                else
                {
                    literalResult += "<li class='dd-item' data-id=" + menuchild.Bgsm_Menu_Id + " data-nama='" + menuchild.Bgsm_Menu_Nama + "'><div class='dd-handle'>" + menuchild.Bgsm_Menu_Nama + "</div><div class=\"dd-actions\"><a href=\"javascript:;\" menu-id=" + menuchild.Bgsm_Menu_Id + " class=\"edit-menu\" title=\"Edit\"><img src=\"" + HelperFactory.ResolveUrl("AdminLte/img/edit.png") + " \" alt=\"Edit\"></a>&nbsp;&nbsp;<a href=\"javascript:;\" menu-id=" + menuchild.Bgsm_Menu_Id + " class=\"delete-menu\" title=\"Delete\"><img src=\"" + HelperFactory.ResolveUrl("AdminLte/img/cross.png") + " \" alt=\"Delete\"></a></div></li>";
                }
            }
            literalResult += "</ol>";
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
        #endregion
        #region OBJECT MENU
        public static string getMenuById(int menuid)
        {
            string result = "";
            BgsmMenu menu;
            using (var databse = new DapperLabFactory())
            {
                List<BgsmMenu> list = databse.GetListWithParam<BgsmMenu>("select * from bgsm_menu where bgsm_menu_id = :paramid", new { paramid = menuid }).ToList();
                if (list.Count > 0) menu = list[0];
                else menu = new BgsmMenu();
            }
            result = JsonConvert.SerializeObject(menu, Formatting.Indented);
            return result;
        }
        #endregion

    }
}
