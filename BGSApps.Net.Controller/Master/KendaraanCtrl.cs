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
    public static class KendaraanCtrl
    {
        #region DROPDOWN VALUE
        #endregion
        #region DATATABLE
        public static string[] GetDataKend(string jsonQueryString)
        {
            DataTable dt;
            List<BgsmJnsKend> list = new List<BgsmJnsKend>();
            BgsmJnsKend generalRef;
            QueryStringTable wheres = JsonConvert.DeserializeObject<QueryStringTable>(jsonQueryString);
            using (var database = new DapperLabFactory())
            {
                string WhereClause = Helper.HelperFactory.querySearchBuilder(wheres.datas);
                List<KeyValuePair<string, object>> WhereParameterValue = new List<KeyValuePair<string, object>>();
                foreach (var item in wheres.datas)
                    WhereParameterValue.Add(new KeyValuePair<string, object>(item.columnName, item.columnValue));
                dt = database.GetListViewSuperQuery("bgsm_jns_kend", wheres.orderName + " " + wheres.orderMethod, WhereClause, WhereParameterValue, wheres.offset, wheres.limit);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        generalRef = new BgsmJnsKend();
                        generalRef.KD_CABANG = Convert.ToInt16(dt.Rows[i]["KD_CABANG"]);
                        generalRef.KD_KEND = dt.Rows[i]["KD_KEND"].ToString();
                        generalRef.JNS_KENDARAAN = dt.Rows[i]["JNS_KENDARAAN"].ToString();
                        generalRef.KENA_PPN = dt.Rows[i]["KENA_PPN"].ToString();
                        generalRef.INEX_PPN = dt.Rows[i]["INEX_PPN"].ToString();
                        generalRef.KLAS_TRANS = dt.Rows[i]["KLAS_TRANS"].ToString();
                        generalRef.REF_CODE = dt.Rows[i]["REF_CODE"].ToString();
                        list.Add(generalRef);
                    }
                }
            }
            string[] jsonresult = new string[list.Count];
            for (int i = 0; i < list.Count; i++)
                jsonresult[i] = JsonConvert.SerializeObject(list[i], Formatting.Indented);

            return jsonresult;
        }
        public static decimal GetCountKend(string jsonQueryString)
        {
            decimal result = 0;
            QueryStringTable wheres = JsonConvert.DeserializeObject<QueryStringTable>(jsonQueryString);
            using (var database = new DapperLabFactory())
            {
                string WhereClause = Helper.HelperFactory.querySearchBuilder(wheres.datas);
                List<KeyValuePair<string, object>> WhereParameterValue = new List<KeyValuePair<string, object>>();
                foreach (var item in wheres.datas)
                    WhereParameterValue.Add(new KeyValuePair<string, object>(item.columnName, item.columnValue));
                result = database.GetCountViewSuperQuery("bgsm_jns_kend", WhereClause, WhereParameterValue);
            }
            return result;
        }
        #endregion
        #region CRUD
        public static int InsertUpdateKend(string jsonobj, string userid, bool isedit, string kdkend)
        {
            int result = 0;
            BgsmJnsKend genref = JsonConvert.DeserializeObject<BgsmJnsKend>(jsonobj);
            using (var database = new DapperLabFactory())
            {
                if (!isedit)
                {
                    result = database.InsertRecord(new
                    {
                        KD_CABANG = genref.KD_CABANG,
                        KD_KEND = genref.KD_KEND,
                        JNS_KENDARAAN = genref.JNS_KENDARAAN,
                        KENA_PPN = genref.KENA_PPN,
                        LAST_UPDATE_DATE = DateTime.Now,
                        LAST_UPDATE_BY = userid,
                        PROGRAME_NAME = "BGAPP",
                        INEX_PPN = genref.INEX_PPN,
                        KLAS_TRANS = genref.KLAS_TRANS,
                        REF_CODE = genref.REF_CODE
                    },
                    "BGSM_JNS_KEND",
                    "KD_CABANG, KD_KEND, JNS_KENDARAAN, KENA_PPN, LAST_UPDATE_DATE, LAST_UPDATE_BY, PROGRAME_NAME, INEX_PPN, KLAS_TRANS, REF_CODE");
                }
                else
                {
                    result = database.UpdateOrDeleteRecord("update BGSM_JNS_KEND set KD_KEND=:kdkend," +
                                                           "JNS_KENDARAAN=:jnskend, " +
                                                           "KENA_PPN=:kenappn, " +
                                                           "INEX_PPN=:inexppn, " +
                                                           "KLAS_TRANS=:klastrans, " +
                                                           "REF_CODE=:refcode, " +
                                                           "LAST_UPDATE_DATE=:lastdate, " +
                                                           "LAST_UPDATE_BY=:lastby WHERE KD_KEND=:oldkdkend",
                                                           new
                                                           {
                                                               kdkend = genref.KD_KEND,
                                                               jnskend = genref.JNS_KENDARAAN,
                                                               kenappn = genref.KENA_PPN,
                                                               inexppn = genref.INEX_PPN,
                                                               klastrans = genref.KLAS_TRANS,
                                                               refcode = genref.REF_CODE,
                                                               lastdate = DateTime.Now,
                                                               lastby = userid,
                                                               oldkdkend = kdkend
                                                           });
                }
            }
            return result;
        }
        public static int DeleteKend(string kdkend)
        {
            int result = 0;
            using (var database = new DapperLabFactory())
            {
                result = database.UpdateOrDeleteRecord("delete from BGSM_JNS_KEND where KD_KEND=:kdkend ", new { kdkend = kdkend });
            }
            return result;
        }
        #endregion
    }
}
