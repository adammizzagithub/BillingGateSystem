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
    public static class TarifCtrl
    {
        #region DROPDOWN VALUE
        public static string GetGeneralRefPly(string kdcabang)
        {
            List<BgsmGeneralRefDetail> details;
            using (var database = new DapperLabFactory())
            {
                details = database.GetListWithParam<BgsmGeneralRefDetail>("select ID_REF_DATA,KET_REF_DATA from BGSM_GENERAL_REF_DETAIL WHERE KD_CABANG = :CAB AND ID_REF_FILE = :JNSPLY", new { CAB = kdcabang, JNSPLY = "JNSPLY" }).ToList();
            }
            return JsonConvert.SerializeObject(details, Formatting.Indented);
        }
        public static string GetKendaraan(string kdcabang)
        {
            List<BgsmJnsKend> details;
            using (var database = new DapperLabFactory())
            {
                details = database.GetListWithParam<BgsmJnsKend>("select KD_KEND,JNS_KENDARAAN from BGSM_JNS_KEND WHERE KD_CABANG = :CAB", new { CAB = kdcabang }).ToList();
            }
            return JsonConvert.SerializeObject(details, Formatting.Indented);
        }
        #endregion
        #region DATATABLE
        public static string[] GetDataTarif(string jsonQueryString)
        {
            DataTable dt;
            List<BgsmMstTarif> list = new List<BgsmMstTarif>();
            BgsmMstTarif generalRef;
            QueryStringTable wheres = JsonConvert.DeserializeObject<QueryStringTable>(jsonQueryString);
            using (var database = new DapperLabFactory())
            {
                string WhereClause = Helper.HelperFactory.querySearchBuilder(wheres.datas);
                List<KeyValuePair<string, object>> WhereParameterValue = new List<KeyValuePair<string, object>>();
                foreach (var item in wheres.datas)
                    WhereParameterValue.Add(new KeyValuePair<string, object>(item.columnName, item.columnValue));
                dt = database.GetListViewSuperQuery("V_MSTR_TARIF", wheres.orderName + " " + wheres.orderMethod, WhereClause, WhereParameterValue, wheres.offset, wheres.limit);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        generalRef = new BgsmMstTarif();
                        generalRef.KD_CABANG = Convert.ToInt16(dt.Rows[i]["KD_CABANG"]);
                        generalRef.KD_TARIF = dt.Rows[i]["KD_TARIF"].ToString();
                        generalRef.KD_PLY = dt.Rows[i]["KD_PLY"].ToString();
                        generalRef.NO_BA = dt.Rows[i]["NO_BA"].ToString();
                        generalRef.TGL_TMT = Convert.ToDateTime(dt.Rows[i]["TGL_TMT"]).ToString("dd/MM/yyyy") == "01/01/0001" ? "" : Convert.ToDateTime(dt.Rows[i]["TGL_TMT"]).ToString("dd/MM/yyyy");
                        generalRef.KET_TARIF = dt.Rows[i]["KET_TARIF"].ToString();
                        generalRef.NM_PLY = dt.Rows[i]["NM_PLY"].ToString();
                        generalRef.Details = GetDetailFromTarifMaster(generalRef.KD_CABANG, generalRef.KD_TARIF);
                        list.Add(generalRef);
                    }
                }
            }
            string[] jsonresult = new string[list.Count];
            for (int i = 0; i < list.Count; i++)
                jsonresult[i] = JsonConvert.SerializeObject(list[i], Formatting.Indented);

            return jsonresult;
        }
        public static decimal GetCountTarif(string jsonQueryString)
        {
            decimal result = 0;
            QueryStringTable wheres = JsonConvert.DeserializeObject<QueryStringTable>(jsonQueryString);
            using (var database = new DapperLabFactory())
            {
                string WhereClause = Helper.HelperFactory.querySearchBuilder(wheres.datas);
                List<KeyValuePair<string, object>> WhereParameterValue = new List<KeyValuePair<string, object>>();
                foreach (var item in wheres.datas)
                    WhereParameterValue.Add(new KeyValuePair<string, object>(item.columnName, item.columnValue));
                result = database.GetCountViewSuperQuery("V_MSTR_TARIF", WhereClause, WhereParameterValue);
            }
            return result;
        }
        public static List<BgsmMstTarifD> GetDetailFromTarifMaster(int kdcabang, string kdtarif)
        {
            List<BgsmMstTarifD> list = new List<BgsmMstTarifD>();
            using (var dapper = new DapperLabFactory())
            {
                list = dapper.GetListWithParam<BgsmMstTarifD>("select * from V_MSTR_TARIF_D WHERE KD_CABANG = :kdcabang AND KD_TARIF = :kdtarif ORDER BY NO_SEQ ASC", new { kdcabang = kdcabang, kdtarif = kdtarif }).ToList();
            }
            return list;
        }
        #endregion
        #region CRUD
        public static int InsertUpdateTarifMaster(string jsonobj, string userid, bool isedit, string oldkdtrf)
        {
            int result = 0;
            BgsmMstTarif genref = JsonConvert.DeserializeObject<BgsmMstTarif>(jsonobj);
            using (var database = new DapperLabFactory())
            {
                if (!isedit)
                {
                    result = database.InsertRecord(new
                    {
                        KD_CABANG = genref.KD_CABANG,
                        KD_TARIF = genref.KD_TARIF,
                        KD_PLY = genref.KD_PLY,
                        NO_BA = genref.NO_BA,
                        TGL_TMT = DateTime.Parse(genref.TGL_TMT),
                        KET_TARIF = genref.KET_TARIF,
                        LAST_UPDATE_DATE = DateTime.Now,
                        LAST_UPDATE_BY = userid,
                        PROGRAME_NAME = "BGAPP"
                    },
                    "BGSM_MST_TARIF",
                    "KD_CABANG, KD_TARIF, KD_PLY, NO_BA, TGL_TMT, KET_TARIF, LAST_UPDATE_DATE, LAST_UPDATE_BY, PROGRAME_NAME");
                }
                else
                {
                    result = database.UpdateOrDeleteRecord("update bgsm_mst_tarif set KD_TARIF=:kdtarif," +
                                                           "KD_PLY=:kdply, " +
                                                           "NO_BA=:noba, " +
                                                           "TGL_TMT=:tgltmt, " +
                                                           "KET_TARIF=:kettarif, " +
                                                           "LAST_UPDATE_DATE=:lastdate, " +
                                                           "LAST_UPDATE_BY=:lastby WHERE KD_TARIF=:oldkdtrf",
                                                           new
                                                           {
                                                               kdtarif = genref.KD_TARIF,
                                                               kdply = genref.KD_PLY,
                                                               noba = genref.NO_BA,
                                                               tgltmt = DateTime.Parse(genref.TGL_TMT),
                                                               kettarif = genref.KET_TARIF,
                                                               lastdate = DateTime.Now,
                                                               lastby = userid,
                                                               oldkdtrf = oldkdtrf
                                                           });
                }
            }
            return result;
        }
        public static int DeleteTarifMaster(string kdtarif)
        {
            int result = 0;
            using (var database = new DapperLabFactory())
            {
                result = database.UpdateOrDeleteRecord("delete from BGSM_MST_TARIF where kd_tarif=:kdtarif ", new { kdtarif = kdtarif });
                if (result == 1)
                    database.UpdateOrDeleteRecord("delete from BGSM_MST_TARIF_D where kd_tarif=:kdtarif ", new { kdtarif = kdtarif });
            }
            return result;
        }
        public static int InsertUpdateTarifDetail(string jsonobj, string userid, bool isedit, int noseq)
        {
            int result = 0;
            BgsmMstTarifD genref = JsonConvert.DeserializeObject<BgsmMstTarifD>(jsonobj);
            using (var database = new DapperLabFactory())
            {
                if (!isedit)
                {
                    result = database.InsertRecord(new
                    {
                        KD_CABANG = genref.KD_CABANG,
                        KD_TARIF = genref.KD_TARIF,
                        NO_SEQ = GetSeqCount(genref.KD_CABANG, genref.KD_TARIF) + 1,
                        JNS_KEND = genref.JNS_KEND,
                        TARIF = genref.TARIF,
                        KETERANGAN = genref.KETERANGAN,
                        LAST_UPDATE_DATE = DateTime.Now,
                        LAST_UPDATE_BY = userid,
                        PROGRAME_NAME = "BGAPP"
                    },
                    "BGSM_MST_TARIF_D",
                    "KD_CABANG, KD_TARIF, NO_SEQ, JNS_KEND, TARIF, KETERANGAN, LAST_UPDATE_DATE, LAST_UPDATE_BY, PROGRAME_NAME");
                }
                else
                {
                    result = database.UpdateOrDeleteRecord("update BGSM_MST_TARIF_D set " +
                                                           "JNS_KEND=:jnskend, " +
                                                           "TARIF=:tarif, " +
                                                           "KETERANGAN=:ket, " +
                                                           "LAST_UPDATE_DATE=:lastdate, " +
                                                           "LAST_UPDATE_BY=:lastby WHERE KD_TARIF=:oldkdtrf AND NO_SEQ=:noseq",
                                                           new
                                                           {
                                                               jnskend = genref.JNS_KEND,
                                                               tarif = genref.TARIF,
                                                               ket = genref.KETERANGAN,
                                                               lastdate = DateTime.Now,
                                                               lastby = userid,
                                                               oldkdtrf = genref.KD_TARIF,
                                                               noseq = noseq
                                                           });
                }
            }
            return result;
        }
        public static int DeleteTarifDetail(string kdtarif, int noseq)
        {
            int result = 0;
            using (var database = new DapperLabFactory())
            {
                result = database.UpdateOrDeleteRecord("delete from BGSM_MST_TARIF_D where kd_tarif=:kdtarif AND NO_SEQ=:noseq", new { kdtarif = kdtarif, noseq = noseq });
            }
            return result;
        }
        private static int GetSeqCount(int kdcabang, string kdtarif)
        {
            int count = 0;
            using (var database = new DapperLabFactory())
            {
                count = database.GetScalarWithParam<int>("select max(NO_SEQ) from BGSM_MST_TARIF_D where kd_tarif=:kdtarif AND kd_CABANG =:cabang", new { kdtarif = kdtarif, cabang = kdcabang });
            }
            return count;
        }
        #endregion
    }
}
