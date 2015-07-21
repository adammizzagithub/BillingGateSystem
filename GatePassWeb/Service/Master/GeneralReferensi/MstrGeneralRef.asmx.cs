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

namespace GatePassWeb.Service.Master.GeneralReferensi
{
    /// <summary>
    /// Summary description for MstrGeneralRef
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.Web.Script.Services.ScriptService]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class MstrGeneralRef : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string[] GetReferenceData(string queryString)
        {
            return GeneralRefCtrl.GetDataGeneralRef(queryString);
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public decimal GetReferenceCount(string queryString)
        {
            return GeneralRefCtrl.GetCountGeneralRef(queryString);
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int InsertUpdateMstr(string jsonobj, string userid, bool isedit, string oldreffile)
        {
            return GeneralRefCtrl.InsertUpdateMstrRef(jsonobj, userid, isedit, oldreffile);
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int InsertUpdateDtl(string jsonobj, string userid, bool isedit, string oldrefdata)
        {
            return GeneralRefCtrl.InsertUpdateDtlRef(jsonobj, userid, isedit, oldrefdata);
        }
    }
}
