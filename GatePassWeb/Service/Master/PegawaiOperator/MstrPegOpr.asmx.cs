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


namespace GatePassWeb.Service.Master.PegawaiOperator
{
    /// <summary>
    /// Summary description for MstrPegOpr
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.Web.Script.Services.ScriptService]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class MstrPegOpr : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string[] GetPegOprData(string queryString)
        {
            return PegawaiOperatorCtrl.GetDataPegOpr(queryString);
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public decimal GetPegOprCount(string queryString)
        {
            return PegawaiOperatorCtrl.GetCountPegOpr(queryString);
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetLovGate(string kdcabang)
        {
            return PegawaiOperatorCtrl.GetGeneralRefGate(kdcabang);
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int InsertUpdatePegOpr(string jsonobj, string userid, bool isedit, string nippeg)
        {
            return PegawaiOperatorCtrl.InsertUpdatePegOpr(jsonobj, userid, isedit, nippeg);
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int DeletePegOpr(string nippeg)
        {
            return PegawaiOperatorCtrl.DeletePegOpr(nippeg);
        }
    }
}
