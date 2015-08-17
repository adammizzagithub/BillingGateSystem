using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using BGSApps.Net.Controller.Transaksi;

namespace GatePassWeb.Service.Transaksi.RekapPasHarian
{
    /// <summary>
    /// Summary description for RekapharianSvc
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.Web.Script.Services.ScriptService]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class RekapharianSvc : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetGate()
        {
            return RekapPasHarianCtrl.GetMasterGateLov();
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string[] GetPeriode()
        {
            return RekapPasHarianCtrl.GetMasterPeriodeLov();
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string[] GetRekapQuery(string tanggal, string kdgate)
        {
            return RekapPasHarianCtrl.GetRekapHarianTrans(tanggal, kdgate);
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public decimal GetTotalBruto(string tanggal, string kdgate)
        {
            return RekapPasHarianCtrl.GetTotalBurto(tanggal, kdgate);
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public decimal GetTotalNeto(string tanggal, string kdgate)
        {
            return RekapPasHarianCtrl.GetTotalNeto(tanggal, kdgate);
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int SimpanRekapHarian(string objectdata)
        {
            return RekapPasHarianCtrl.SimpanRekapHarian(objectdata);
        }
    }
}
