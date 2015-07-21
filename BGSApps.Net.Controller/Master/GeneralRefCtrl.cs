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

    public static class GeneralRefCtrl
    {
        #region DATATABLE
        public static string[] GetDataGeneralRef(string jsonQueryString)
        {
            DataTable dt;
            List<BgsmGeneralRef> list = new List<BgsmGeneralRef>();
            BgsmGeneralRef generalRef;
            QueryStringTable wheres = JsonConvert.DeserializeObject<QueryStringTable>(jsonQueryString);
            using (var database = new DapperLabFactory())
            {
                string WhereClause = Helper.HelperFactory.querySearchBuilder(wheres.datas);
                List<KeyValuePair<string, object>> WhereParameterValue = new List<KeyValuePair<string, object>>();
                foreach (var item in wheres.datas)
                    WhereParameterValue.Add(new KeyValuePair<string, object>(item.columnName, item.columnValue));
                dt = database.GetListViewSuperQuery("BGSM_GENERAL_REF", wheres.orderName + " " + wheres.orderMethod, WhereClause, WhereParameterValue, wheres.offset, wheres.limit);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        generalRef = new BgsmGeneralRef();
                        generalRef.KD_CABANG = dt.Rows[i]["KD_CABANG"].ToString();
                        generalRef.ID_REF_FILE = dt.Rows[i]["ID_REF_FILE"].ToString();
                        generalRef.KET_REFERENCE = dt.Rows[i]["KET_REFERENCE"].ToString();
                        generalRef.ATTRIB1 = dt.Rows[i]["ATTRIB1"].ToString();
                        generalRef.ATTRIB2 = dt.Rows[i]["ATTRIB2"].ToString();
                        generalRef.VAL1 = dt.Rows[i]["VAL1"].ToString();
                        generalRef.VAL2 = dt.Rows[i]["VAL2"].ToString();
                        generalRef.KD_AKTIF = dt.Rows[i]["KD_AKTIF"].ToString();
                        generalRef.Details = GetDetailFromRefMaster(generalRef.KD_CABANG, generalRef.ID_REF_FILE);
                        list.Add(generalRef);
                    }
                }
            }
            string[] jsonresult = new string[list.Count];
            for (int i = 0; i < list.Count; i++)
                jsonresult[i] = JsonConvert.SerializeObject(list[i], Formatting.Indented);

            return jsonresult;
        }
        public static decimal GetCountGeneralRef(string jsonQueryString)
        {
            decimal result = 0;
            QueryStringTable wheres = JsonConvert.DeserializeObject<QueryStringTable>(jsonQueryString);
            using (var database = new DapperLabFactory())
            {
                string WhereClause = Helper.HelperFactory.querySearchBuilder(wheres.datas);
                List<KeyValuePair<string, object>> WhereParameterValue = new List<KeyValuePair<string, object>>();
                foreach (var item in wheres.datas)
                    WhereParameterValue.Add(new KeyValuePair<string, object>(item.columnName, item.columnValue));
                result = database.GetCountViewSuperQuery("BGSM_GENERAL_REF", WhereClause, WhereParameterValue);
            }
            return result;
        }
        public static List<BgsmGeneralRefDetail> GetDetailFromRefMaster(string kdcabang, string IdRefFile)
        {
            List<BgsmGeneralRefDetail> list = new List<BgsmGeneralRefDetail>();
            using (var dapper = new DapperLabFactory())
            {
                list = dapper.GetListWithParam<BgsmGeneralRefDetail>("select * from BGSM_GENERAL_REF_DETAIL WHERE KD_CABANG = :kdcabang AND ID_REF_FILE = :reffile", new { kdcabang = kdcabang, reffile = IdRefFile }).ToList();
            }
            return list;
        }
        #endregion
        #region CRUD
        public static int InsertUpdateMstrRef(string jsonObj, string userid, bool isedit, string oldreffile)
        {
            int result = 0;
            BgsmGeneralRef genref = JsonConvert.DeserializeObject<BgsmGeneralRef>(jsonObj);
            using (var database = new DapperLabFactory())
            {
                if (!isedit)
                {
                    result = database.InsertRecord(new
                    {
                        KD_CABANG = genref.KD_CABANG,
                        JENIS_TABLE = "M",
                        ID_TABLE = "TABLE",
                        KODE_MODUL = "UPP",
                        ID_REF_FILE = genref.ID_REF_FILE,
                        KET_REFERENCE = genref.KET_REFERENCE,
                        ATTRIB1 = genref.ATTRIB1,
                        ATTRIB2 = genref.ATTRIB2,
                        VAL1 = genref.VAL1,
                        VAL2 = genref.VAL2,
                        KD_AKTIF = genref.KD_AKTIF,
                        LAST_UPDATED_DATE = DateTime.Now,
                        LAST_UPDATED_BY = userid,
                        PROGRAM_NAME = "BGAPP"
                    },
                    "BGSM_GENERAL_REF",
                    "KD_CABANG, JENIS_TABLE, ID_TABLE, KODE_MODUL, ID_REF_FILE, KET_REFERENCE, KD_AKTIF, ATTRIB1, ATTRIB2, VAL1, VAL2, LAST_UPDATED_DATE, LAST_UPDATED_BY, PROGRAM_NAME");
                }
                else
                {
                    result = database.UpdateOrDeleteRecord("update bgsm_general_ref set ID_REF_FILE=:idreffile," +
                                                           "KET_REFERENCE=:ketref, " +
                                                           "KD_AKTIF=:kdaktif, " +
                                                           "ATTRIB1=:attr1, " +
                                                           "ATTRIB2=:attr2, " +
                                                           "VAL1=:val1, " +
                                                           "VAL2=:val2, " +
                                                           "LAST_UPDATED_DATE=:lastdate, " +
                                                           "LAST_UPDATED_BY=:lastby WHERE ID_REF_FILE=:oldreffile", new { idreffile = genref.ID_REF_FILE, ketref = genref.KET_REFERENCE, kdaktif = genref.KD_AKTIF, attr1 = genref.ATTRIB1, attr2 = genref.ATTRIB2, val1 = genref.VAL1, val2 = genref.VAL2, lastdate = DateTime.Now, lastby = userid, oldreffile = oldreffile });
                }
            }
            return result;
        }
        public static int InsertUpdateDtlRef(string jsonObj, string userid, bool isedit, string oldrefdata)
        {
            int result = 0;
            BgsmGeneralRefDetail genref = JsonConvert.DeserializeObject<BgsmGeneralRefDetail>(jsonObj);
            using (var database = new DapperLabFactory())
            {
                if (!isedit)
                {
                    result = database.InsertRecord(new
                    {
                        KD_CABANG = genref.KD_CABANG,
                        JENIS_TABLE = "M",
                        ID_TABLE = "TABLE",
                        KODE_MODUL = "UPP",
                        ID_REF_FILE = genref.ID_REF_FILE,
                        ID_REF_DATA = genref.ID_REF_DATA,
                        KET_REF_DATA = genref.KET_REF_DATA,
                        ATTRIB1 = genref.ATTRIB1,
                        ATTRIB2 = genref.ATTRIB2,
                        VAL1 = genref.VAL1,
                        VAL2 = genref.VAL2,
                        KD_AKTIF = genref.KD_AKTIF,
                        LAST_UPDATED_DATE = DateTime.Now,
                        LAST_UPDATED_BY = userid,
                        PROGRAM_NAME = "BGAPP"
                    },
                    "BGSM_GENERAL_REF_DETAIL",
                    "KD_CABANG, JENIS_TABLE, ID_TABLE, KODE_MODUL, ID_REF_FILE, ID_REF_DATA, KET_REF_DATA, KD_AKTIF, ATTRIB1, ATTRIB2, VAL1, VAL2, LAST_UPDATED_DATE, LAST_UPDATED_BY, PROGRAM_NAME");
                }
                else
                {
                    result = database.UpdateOrDeleteRecord("update BGSM_GENERAL_REF_DETAIL set ID_REF_DATA=:idrefdata," +
                                                           "KET_REF_DATA=:ketrefdata, " +
                                                           "KD_AKTIF=:kdaktif, " +
                                                           "ATTRIB1=:attr1, " +
                                                           "ATTRIB2=:attr2, " +
                                                           "VAL1=:val1, " +
                                                           "VAL2=:val2, " +
                                                           "LAST_UPDATED_DATE=:lastdate, " +
                                                           "LAST_UPDATED_BY=:lastby WHERE ID_REF_DATA=:oldrefdata", new { idrefdata = genref.ID_REF_DATA, ketrefdata = genref.KET_REF_DATA, kdaktif = genref.KD_AKTIF, attr1 = genref.ATTRIB1, attr2 = genref.ATTRIB2, val1 = genref.VAL1, val2 = genref.VAL2, lastdate = DateTime.Now, lastby = userid, oldrefdata = oldrefdata });
                }
            }
            return result;
        }
        #endregion
    }
}
