using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using BGSApps.Net.Controller.Master;

namespace GatePassWeb.Service.Master.Kendaraan
{
    /// <summary>
    /// Summary description for MstrKendaraan
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.Web.Script.Services.ScriptService]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class MstrKendaraan : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string[] GetKendData(string queryString)
        {
            return KendaraanCtrl.GetDataKend(queryString);
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public decimal GetKendCount(string queryString)
        {
            return KendaraanCtrl.GetCountKend(queryString);
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int InsertUpdateKend(string jsonobj, string userid, bool isedit, string kdkend)
        {
            return KendaraanCtrl.InsertUpdateKend(jsonobj, userid, isedit, kdkend);
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int DeleteKend(string kdkend)
        {
            return KendaraanCtrl.DeleteKend(kdkend);
        }
    }
}
