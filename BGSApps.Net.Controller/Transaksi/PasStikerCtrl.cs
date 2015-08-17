using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BGSApps.Net.Model.Master;
using BGSApps.Net.Model.Transaksi;
using BGSApps.Net.Model.Additional;
using BGSApps.Net.DapperFactory;
using Newtonsoft.Json;

namespace BGSApps.Net.Controller.Transaksi
{
    public static class PasStikerCtrl
    {
        #region DROPDOWN VALUE
        public static string GetListDivisi(string kd_cabang)
        {
            List<SafrDivisi> details;
            using (var database = new DapperLabFactory())
            {
                details = database.GetListWithParam<SafrDivisi>("select KODE_DIVISI,NAMA_DIVISI from SAFR_DIVISI WHERE KD_CABANG = :CAB", new { CAB = kd_cabang}).ToList();
            }
            return JsonConvert.SerializeObject(details, Formatting.Indented);
        }
        #endregion

        #region DROPDOWN VALUE
        public static string GetListJenisKendaraan(string kd_cabang)
        {
            List<BgsmJnsKend> details;
            using (var database = new DapperLabFactory())
            {
                details = database.GetListWithParam<BgsmJnsKend>("select KD_KEND,JNS_KENDARAAN from BGSM_JNS_KEND WHERE KD_CABANG = :CAB", new { CAB = kd_cabang}).ToList();
            }
            return JsonConvert.SerializeObject(details, Formatting.Indented);
        }
        #endregion

        #region DATATABLE
        public static string[] GetDataFreePass(string jsonQueryString)
        {
            DataTable dt;
            List<BgstDaftarPasStiker> list = new List<BgstDaftarPasStiker>();
            BgstDaftarPasStiker generalRef;
            QueryStringTable wheres = JsonConvert.DeserializeObject<QueryStringTable>(jsonQueryString);
            using (var database = new DapperLabFactory())
            {
                string WhereClause = Helper.HelperFactory.querySearchBuilder(wheres.datas);
                List<KeyValuePair<string, object>> WhereParameterValue = new List<KeyValuePair<string, object>>();
                foreach (var item in wheres.datas)
                    WhereParameterValue.Add(new KeyValuePair<string, object>(item.columnName, item.columnValue));
                dt = database.GetListViewSuperQuery("V_BGST_DAFTAR_STIKER", wheres.orderName + " " + wheres.orderMethod, WhereClause, WhereParameterValue, wheres.offset, wheres.limit);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        generalRef = new BgstDaftarPasStiker();
                        generalRef.ID = Convert.ToInt64(dt.Rows[i]["ID"]);
                        generalRef.NO_STIKER = dt.Rows[i]["NO_STIKER"].ToString();
                        generalRef.DIVISI = dt.Rows[i]["DIVISI"].ToString();
                        generalRef.NAMA_DIVISI = dt.Rows[i]["NAMA_DIVISI"].ToString();
                        generalRef.INSTANSI = dt.Rows[i]["INSTANSI"].ToString();
                        generalRef.NO_ID = dt.Rows[i]["NO_ID"].ToString();
                        generalRef.NAMA = dt.Rows[i]["NAMA"].ToString();
                        generalRef.NOPOL_2 = dt.Rows[i]["NOPOL_2"].ToString();
                        generalRef.NOPOL_4 = dt.Rows[i]["NOPOL_4"].ToString();
                        generalRef.KETERANGAN = dt.Rows[i]["KETERANGAN"].ToString();
                        generalRef.NO_RFID = dt.Rows[i]["NO_RFID"].ToString();
                        generalRef.JNS_KEND = dt.Rows[i]["JNS_KEND"].ToString();
                        generalRef.JNS_KENDARAAN = dt.Rows[i]["JNS_KENDARAAN"].ToString();
                        //generalRef.TGL_AKHIR = dt.Rows[i]["TGL_AKHIR"].ToString("dd/MM/yyyy") == "01/01/0001" ? "" : dt.Rows[i]["TGL_AKHIR"].ToString("dd/MM/yyyy");
                        generalRef.TGL_AKHIR = Convert.ToDateTime(dt.Rows[i]["TGL_AKHIR"]).ToString("dd/MM/yyyy") == "01/01/0001" ? "" : Convert.ToDateTime(dt.Rows[i]["TGL_AKHIR"]).ToString("dd/MM/yyyy");
                        list.Add(generalRef);
                    }
                }
            }
            string[] jsonresult = new string[list.Count];
            for (int i = 0; i < list.Count; i++)
                jsonresult[i] = JsonConvert.SerializeObject(list[i], Formatting.Indented);

            return jsonresult;
        }
        public static decimal GetCountFreePass(string jsonQueryString)
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
        public static int InsertUpdateFreePass(string jsonobj, string userid, bool isedit, string id)
        {
            int result = 0;
            BgstDaftarPasStiker genref = JsonConvert.DeserializeObject<BgstDaftarPasStiker>(jsonobj);
            using (var database = new DapperLabFactory())
            {
                if (!isedit)
                {
                    result = database.InsertRecord(new
                    {
                        KD_CABANG = genref.KD_CABANG,
                        NO_STIKER = genref.NO_STIKER,
                        DIVISI = genref.DIVISI,
                        INSTANSI = genref.INSTANSI,
                        NO_ID = genref.NO_ID,
                        NAMA = genref.NAMA,
                        NOPOL_2 = genref.NOPOL_2,
                        NOPOL_4 = genref.NOPOL_4,
                        KETERANGAN = genref.KETERANGAN,
                        TGL_ENTRY = DateTime.Now,
                        LAST_UPDATE_DATE = DateTime.Now,
                        LAST_UPDATE_BY = userid,
                        PROGRAME_NAME = "BGAPP",
                        NO_RFID = genref.NO_RFID,
                        TGL_AKHIR = genref.TGL_AKHIR,
                        JNS_KEND = genref.JNS_KEND
                    },
                    "BGST_DAFTAR_PAS_STIKER",
                    "KD_CABANG, NO_STIKER, DIVISI, INSTANSI, NO_ID, NAMA, NOPOL_2, NOPOL_4, KETERANGAN, TGL_ENTRY, LAST_UPDATE_DATE, PROGRAME_NAME, NO_RFID, TGL_AKHIR, JNS_KEND");
                }
                else
                {
                    result = database.UpdateOrDeleteRecord("update BGST_DAFTAR_PAS_STIKER set NO_STIKER=:no_stiker," +
                                                           "DIVISI=:divisi, " +
                                                           "INSTANSI=:instansi, " +
                                                           "NO_ID=:no_id, " +
                                                           "NAMA=:nama, " +
                                                           "NOPOL_2=:nopol_2, " +
                                                           "NOPOL_4=:nopol_4, " +
                                                           "KETERANGAN=:keterangan, " +
                                                           "LAST_UPDATE_DATE=:lastdate, " +
                                                           "LAST_UPDATE_BY=:lastby, " +
                                                           "NO_RFID=:no_rfid, " +
                                                           "TGL_AKHIR=:tgl_akhir, " +
                                                           "JNS_KEND=:jns_kend " +
                                                           "WHERE ID=:id",
                                                           new
                                                           {
                                                               no_stiker = genref.NO_STIKER,
                                                               divisi = genref.DIVISI,
                                                               instansi = genref.INSTANSI,
                                                               no_id = genref.NO_ID,
                                                               nama = genref.NAMA,
                                                               nopol_2 = genref.NOPOL_2,
                                                               nopol_4 = genref.NOPOL_4,
                                                               keterangan = genref.KETERANGAN,
                                                               lastdate = DateTime.Now,
                                                               lastby = userid,
                                                               no_rfid = genref.NO_RFID,
                                                               tgl_akhir = genref.TGL_AKHIR,
                                                               jns_kend = genref.JNS_KEND,
                                                               id = id
                                                           });
                }
            }
            return result;
        }
        public static int DeleteFreePass(string id)
        {
            int result = 0;
            using (var database = new DapperLabFactory())
            {
                result = database.UpdateOrDeleteRecord("delete from BGST_DAFTAR_PAS_STIKER where ID=:id ", new { id = id });
            }
            return result;
        }
        #endregion
    }
}



