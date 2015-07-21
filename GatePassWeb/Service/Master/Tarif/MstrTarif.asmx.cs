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

namespace GatePassWeb.Service.Master.Tarif
{
    /// <summary>
    /// Summary description for MstrTarif
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.Web.Script.Services.ScriptService]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class MstrTarif : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string[] GetTarifData(string queryString)
        {
            return TarifCtrl.GetDataTarif(queryString);
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public decimal GetTarifCount(string queryString)
        {
            return TarifCtrl.GetCountTarif(queryString);
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetLovJenisPas(string kdcabang)
        {
            return TarifCtrl.GetGeneralRefPly(kdcabang);
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetLovKendaraan(string kdcabang)
        {
            return TarifCtrl.GetKendaraan(kdcabang);
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int InsertUpdateMstr(string jsonobj, string userid, bool isedit, string oldkdtrf)
        {
            return TarifCtrl.InsertUpdateTarifMaster(jsonobj, userid, isedit, oldkdtrf);
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int DeleteMstr(string kdtrf)
        {
            return TarifCtrl.DeleteTarifMaster(kdtrf);
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int InsertUpdateDetail(string jsonobj, string userid, bool isedit, int noseq)
        {
            return TarifCtrl.InsertUpdateTarifDetail(jsonobj, userid, isedit, noseq);
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int DeleteDetail(string kdtarif, int noseq)
        {
            return TarifCtrl.DeleteTarifDetail(kdtarif, noseq);
        }
    }
}
