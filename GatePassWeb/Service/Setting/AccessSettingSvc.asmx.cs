using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using BGSApps.Net.Controller.Core;

namespace GatePassWeb.Service.Setting
{
    /// <summary>
    /// Summary description for AccessSettingSvc
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.Web.Script.Services.ScriptService]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class AccessSettingSvc : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int CreateNewRole(string obj)
        {
            return AccessSettingCtrl.CreateNewRole(obj);
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int DeleteRole(int RoleId)
        {
            return AccessSettingCtrl.DeleteRole(RoleId);
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetAllRoles()
        {
            return AccessSettingCtrl.GetRoles();
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetRolesByHakAksesId(int HakAksesId)
        {
            return AccessSettingCtrl.GetRolesByHakAksesId(HakAksesId);
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int CreateControlByRole(string json)
        {
            return AccessSettingCtrl.CreateControlByRole(json);
        }
    }
}
