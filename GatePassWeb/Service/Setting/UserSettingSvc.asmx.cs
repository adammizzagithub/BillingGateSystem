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
    /// Summary description for UserSettingSvc
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.Web.Script.Services.ScriptService]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class UserSettingSvc : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetReference(int code)
        {
            if (code == 0)
                return UserSettingCtrl.GetCabangSelect();
            else
                return UserSettingCtrl.GetRolesSelect();
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int UpdateUser(string obj)
        {
            return UserSettingCtrl.UpdateUser(obj);
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int ActivateUser(int userId, string newvalue)
        {
            return UserSettingCtrl.ActivateUser(userId, newvalue);
        }
    }
}
