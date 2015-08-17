using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BGSApps.Net.Model.Master;
using BGSApps.Net.Model.Transaksi;
using BGSApps.Net.DapperFactory;
using Newtonsoft.Json;
using System.Data;
using Oracle.DataAccess.Client;
using System.Globalization;

namespace BGSApps.Net.Controller.Transaksi
{
    public static class RekapPasHarianCtrl
    {
        private class GelondonganData
        {
            public virtual List<BgstRkpallPasHr> data { get; set; }
        }
        #region FORM INPUT
        #region DROPDOWN DATA
        public static string GetMasterGateLov()
        {
            List<BgsmGeneralRefDetail> gate = new List<BgsmGeneralRefDetail>();
            using (var database = new DapperLabFactory())
            {
                gate = database.GetListWithParam<BgsmGeneralRefDetail>("SELECT * FROM BGSM_GENERAL_REF_DETAIL WHERE ID_REF_FILE = :MSTGATE", new { MSTGATE = "MSTGATE" }).ToList();
            }
            return JsonConvert.SerializeObject(gate, Formatting.Indented);
        }
        public static string[] GetMasterPeriodeLov()
        {
            string[] res;
            List<PeriodeBuku> buku = new List<PeriodeBuku>();
            using (var database = new DapperLabFactory())
            {
                buku = database.GetListNoParam<PeriodeBuku>("select * from v_bgst_periode_buku").ToList();
            }
            res = new string[buku.Count];
            for (int i = 0; i < res.Length; i++)
                res[i] = JsonConvert.SerializeObject(buku[i], Formatting.Indented);
            return res;
        }
        #endregion
        #region REKAP QUERY
        public static string[] GetRekapHarianTrans(string tgl, string kdgate)
        {
            string[] res;
            List<VBgstTransHrNew> buku = new List<VBgstTransHrNew>();
            using (var database = new DapperLabFactory())
            {
                buku = database.GetListWithParam<VBgstTransHrNew>("select * from V_BGST_TRANS_HR_NEW where tgl_trans = to_date(:tgl,'dd/MM/yyyy') and gate = :gate", new { tgl = tgl, gate = kdgate }).ToList();
            }
            res = new string[buku.Count];
            for (int i = 0; i < res.Length; i++)
                res[i] = JsonConvert.SerializeObject(buku[i], Formatting.Indented);
            return res;
        }
        public static decimal GetTotalBurto(string tgl, string kdgate)
        {
            decimal result = 0;
            using (var database = new DapperLabFactory())
            {
                result = database.GetScalarWithParam<decimal>("select sum(pndp_gross) as jum_bruto from V_BGST_TRANS_HR_NEW where tgl_trans = to_date(:tgl,'dd/MM/yyyy') and gate = :gate", new { tgl = tgl, gate = kdgate });
            }
            return result;
        }
        public static decimal GetTotalNeto(string tgl, string kdgate)
        {
            decimal result = 0;
            using (var database = new DapperLabFactory())
            {
                result = database.GetScalarWithParam<decimal>("select sum(pndp_net) as jum_bruto from V_BGST_TRANS_HR_NEW where tgl_trans = to_date(:tgl,'dd/MM/yyyy') and gate = :gate", new { tgl = tgl, gate = kdgate });
            }
            return result;
        }
        #endregion
        #region SIMPAN REKAP
        public static int SimpanRekapHarian(string datasInsert)
        {
            int result = 0;
            using (var database = new DapperLabFactory())
            {
                GelondonganData datas = JsonConvert.DeserializeObject<GelondonganData>(datasInsert);
                using (var connection = database.GetConnection())
                {
                    connection.Open();
                    using (var trx = connection.BeginTransaction())
                    {
                        try
                        {
                            if (datas.data.Count > 0)
                            {
                                for (int i = 0; i < datas.data.Count; i++)
                                {
                                    database.InsertRecord(new
                                    {
                                        KD_CABANG = datas.data[i].KdCabang,
                                        NO_RKP = "",
                                        TGL_TRANS = datas.data[i].TglTrans,
                                        SHIFT = "S",
                                        GATE = datas.data[i].Gate,
                                        KD_OPERATOR = "",
                                        TAHUN = datas.data[i].Tahun,
                                        BULAN = datas.data[i].Bulan,
                                        JNS_KEND = datas.data[i].JnsKend,
                                        JUMLAH = datas.data[i].Jumlah,
                                        TARIF = datas.data[i].Tarif,
                                        PENDAPATAN = datas.data[i].Pendapatan,
                                        PPN = datas.data[i].Ppn,
                                        KETERANGAN = "REKAP PAS HARIAN",
                                        STS_RKP = 1,
                                        CREATED_DATE = DateTime.Now,
                                        CREATED_BY = datas.data[i].CreatedBy,
                                        LAST_UPDATED_DATE = DateTime.Now,
                                        LAST_UPDATED_BY = datas.data[i].LastUpdatedBy,
                                        PROGRAM_NAME = "BGST_REKAP_PAS_HR",
                                        NO_AWAL = "",
                                        NO_AKHIR = ""
                                    },
                                    "BGST_RKPALL_PAS_HR",
                                    "KD_CABANG, NO_RKP, TGL_TRANS, SHIFT, GATE, KD_OPERATOR, TAHUN, BULAN, JNS_KEND, JUMLAH, TARIF, PENDAPATAN, PPN, KETERANGAN, STS_RKP, CREATED_DATE, CREATED_BY, LAST_UPDATED_DATE, LAST_UPDATED_BY, PROGRAM_NAME, NO_AWAL, NO_AKHIR");
                                }
                            }
                            result = 1;
                            trx.Commit();
                        }
                        catch
                        {
                            result = 0;
                            trx.Rollback();
                            throw;
                        }
                    }


                }
            }
            return result;
        }
        #endregion
        #endregion
    }
}
