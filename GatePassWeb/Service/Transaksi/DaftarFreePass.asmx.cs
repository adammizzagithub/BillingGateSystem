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
using BGSApps.Net.Controller.Transaksi;

namespace GatePassWeb.Service.Transaksi
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
    public class DaftarFreePass : System.Web.Services.WebService
    {
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetListDivisi(string kd_cabang)
        {
            return PasStikerCtrl.GetListDivisi(kd_cabang);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetListJenisKendaraan(string kd_cabang)
        {
            return PasStikerCtrl.GetListJenisKendaraan(kd_cabang);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string[] GetFreePassData(string queryString)
        {
            return PasStikerCtrl.GetDataFreePass(queryString);
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public decimal GetFreePassCount(string queryString)
        {
            return PasStikerCtrl.GetCountFreePass(queryString);
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int InsertUpdateFreePass(string jsonobj, string userid, bool isedit, string id)
        {
            return PasStikerCtrl.InsertUpdateFreePass(jsonobj, userid, isedit, id);
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int DeleteFreePass(string id)
        {
            return PasStikerCtrl.DeleteFreePass(id);
        }
    }
}
