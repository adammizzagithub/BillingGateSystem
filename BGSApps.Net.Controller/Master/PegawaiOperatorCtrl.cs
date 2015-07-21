using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BGSApps.Net.Model.Master;
using BGSApps.Net.Model.Additional;
using BGSApps.Net.DapperFactory;
using Newtonsoft.Json;


namespace BGSApps.Net.Controller.Master
{
    public static class PegawaiOperatorCtrl
    {
        #region DROPDOWN VALUE
        public static string GetGeneralRefGate(string kdcabang)
        {
            List<BgsmGeneralRefDetail> details;
            using (var database = new DapperLabFactory())
            {
                details = database.GetListWithParam<BgsmGeneralRefDetail>("select ID_REF_DATA,KET_REF_DATA from BGSM_GENERAL_REF_DETAIL WHERE KD_CABANG = :CAB AND ID_REF_FILE = :MSTGATE", new { CAB = kdcabang, MSTGATE = "MSTGATE" }).ToList();
            }
            return JsonConvert.SerializeObject(details, Formatting.Indented);
        }
        #endregion
        #region DATATABLE
        public static string[] GetDataPegOpr(string jsonQueryString)
        {
            DataTable dt;
            List<BgsmPegOpr> list = new List<BgsmPegOpr>();
            BgsmPegOpr generalRef;
            QueryStringTable wheres = JsonConvert.DeserializeObject<QueryStringTable>(jsonQueryString);
            using (var database = new DapperLabFactory())
            {
                string WhereClause = Helper.HelperFactory.querySearchBuilder(wheres.datas);
                List<KeyValuePair<string, object>> WhereParameterValue = new List<KeyValuePair<string, object>>();
                foreach (var item in wheres.datas)
                    WhereParameterValue.Add(new KeyValuePair<string, object>(item.columnName, item.columnValue));
                dt = database.GetListViewSuperQuery("V_MSTR_PEGOPR", wheres.orderName + " " + wheres.orderMethod, WhereClause, WhereParameterValue, wheres.offset, wheres.limit);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        generalRef = new BgsmPegOpr();
                        generalRef.KD_CABANG = Convert.ToInt16(dt.Rows[i]["KD_CABANG"]);
                        generalRef.NIP_PEG = dt.Rows[i]["NIP_PEG"].ToString();
                        generalRef.NM_PEG = dt.Rows[i]["NM_PEG"].ToString();
                        generalRef.KD_BAGIAN = dt.Rows[i]["KD_BAGIAN"].ToString();
                        generalRef.KD_GATE = dt.Rows[i]["KD_GATE"].ToString();
                        generalRef.REC_STAT = dt.Rows[i]["REC_STAT"].ToString();
                        generalRef.NM_GATE = dt.Rows[i]["NM_GATE"].ToString();
                        list.Add(generalRef);
                    }
                }
            }
            string[] jsonresult = new string[list.Count];
            for (int i = 0; i < list.Count; i++)
                jsonresult[i] = JsonConvert.SerializeObject(list[i], Formatting.Indented);

            return jsonresult;
        }
        public static decimal GetCountPegOpr(string jsonQueryString)
        {
            decimal result = 0;
            QueryStringTable wheres = JsonConvert.DeserializeObject<QueryStringTable>(jsonQueryString);
            using (var database = new DapperLabFactory())
            {
                string WhereClause = Helper.HelperFactory.querySearchBuilder(wheres.datas);
                List<KeyValuePair<string, object>> WhereParameterValue = new List<KeyValuePair<string, object>>();
                foreach (var item in wheres.datas)
                    WhereParameterValue.Add(new KeyValuePair<string, object>(item.columnName, item.columnValue));
                result = database.GetCountViewSuperQuery("V_MSTR_PEGOPR", WhereClause, WhereParameterValue);
            }
            return result;
        }
        #endregion
        #region CRUD
        public static int InsertUpdatePegOpr(string jsonobj, string userid, bool isedit, string nippeg)
        {
            int result = 0;
            BgsmPegOpr genref = JsonConvert.DeserializeObject<BgsmPegOpr>(jsonobj);
            using (var database = new DapperLabFactory())
            {
                if (!isedit)
                {
                    result = database.InsertRecord(new
                    {
                        KD_CABANG = genref.KD_CABANG,
                        NIP_PEG = genref.NIP_PEG,
                        NM_PEG = genref.NM_PEG.ToUpper(),
                        KD_BAGIAN = "",
                        KD_GATE = genref.KD_GATE,
                        REC_STAT = genref.REC_STAT,
                        LAST_UPDATE_DATE = DateTime.Now,
                        LAST_UPDATE_BY = userid,
                        PROGRAME_NAME = "BGAPP",
                        PASSWORD = ""
                    },
                    "BGSM_PEG_OPR",
                    "KD_CABANG, NIP_PEG, NM_PEG, KD_BAGIAN, KD_GATE, REC_STAT, LAST_UPDATE_DATE, LAST_UPDATE_BY, PROGRAME_NAME, PASSWORD");
                }
                else
                {
                    result = database.UpdateOrDeleteRecord("update BGSM_PEG_OPR set NIP_PEG=:nippeg," +
                                                           "NM_PEG=:nmpeg, " +
                                                           "KD_BAGIAN=:kdbag, " +
                                                           "KD_GATE=:kdgate, " +
                                                           "REC_STAT=:recstat, " +
                                                           "LAST_UPDATE_DATE=:lastdate, " +
                                                           "LAST_UPDATE_BY=:lastby WHERE NIP_PEG=:oldnippeg",
                                                           new
                                                           {
                                                               nippeg = genref.NIP_PEG,
                                                               nmpeg = genref.NM_PEG,
                                                               kdbag = genref.KD_BAGIAN,
                                                               kdgate = genref.KD_GATE,
                                                               recstat = genref.REC_STAT,
                                                               lastdate = DateTime.Now,
                                                               lastby = userid,
                                                               oldnippeg = nippeg
                                                           });
                }
            }
            return result;
        }
        public static int DeletePegOpr(string nippeg)
        {
            int result = 0;
            using (var database = new DapperLabFactory())
            {
                result = database.UpdateOrDeleteRecord("delete from BGSM_PEG_OPR where NIP_PEG=:nippeg ", new { nippeg = nippeg });
            }
            return result;
        }
        #endregion
    }
}
