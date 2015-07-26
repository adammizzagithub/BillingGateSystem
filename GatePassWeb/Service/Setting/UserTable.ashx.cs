using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BGSApps.Net.Controller.Core;

namespace GatePassWeb.Service.Setting
{
    public class UserTable : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int iDisplayLength = Convert.ToInt32(context.Request.QueryString["iDisplayLength"]);
            int iDisplayStart = Convert.ToInt32(context.Request.QueryString["iDisplayStart"]);
            string sSearch = context.Request.QueryString["sSearch"];
            int sEcho = Convert.ToInt32(context.Request.QueryString["sEcho"]);
            string rec = UserSettingCtrl.getTableWithJSON(sEcho, iDisplayStart, iDisplayLength, sSearch);
            context.Response.ContentType = "text/html";
            context.Response.Write(rec);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}