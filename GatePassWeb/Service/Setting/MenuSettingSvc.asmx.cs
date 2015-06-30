using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using BGSApps.Net.Controller.Menu;

namespace GatePassWeb.Service.Setting
{
    /// <summary>
    /// Summary description for MenuSettingSvc
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.Web.Script.Services.ScriptService]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class MenuSettingSvc : System.Web.Services.WebService
    {
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int CreateNewMenu(string obj)
        {
            return MenuSettingCtrl.CreateNewMenu(obj);
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int UpdateMenu(string obj)
        {
            return MenuSettingCtrl.UpdateMenu(obj);
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int DeleteMenu(int menuid)
        {
            return MenuSettingCtrl.DeleteMenu(menuid);
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string getMenuTreeView()
        {
            return MenuSettingCtrl.getBindliteralMenu();
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string getMenuViaId(int menuid)
        {
            return MenuSettingCtrl.getMenuById(menuid);
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void SaveMenuConfiguration(string jsonstring, string userid)
        {
            MenuSettingCtrl.SaveMenuConfiguration(jsonstring, userid);
        }
    }
}
