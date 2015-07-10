using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BGSApps.Net.Model.Core;
using Newtonsoft.Json;
using BGSApps.Net.DapperFactory;
using System.Security.Cryptography;

namespace BGSApps.Net.Controller.Core
{
    public static class UserSettingCtrl
    {
        public class ListRec
        {
            public int sEcho { get; set; }
            public int iTotalRecords { get; set; }
            public int iTotalDisplayRecords { get; set; }
            public IList<object> aaData { get; set; }
        }
        private static string GetMd5Hash(MD5 md5Hash, string input)
        {

            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
        #region CRUD
        public static int UpdateUser(string obj)
        {
            int res = 0;
            BgsmUser user = JsonConvert.DeserializeObject<BgsmUser>(obj);
            using (var database = new DapperLabFactory())
            {
                res = database.UpdateOrDeleteRecord("update bgsm_user set bgsm_user_kdcabang=:kdcab," +
                                                    "bgsm_user_cabang=:nmcab," +
                                                    "bgsm_user_roleid=:roleid," +
                                                    "bgsm_user_rolename=:rolename,last_update_date=:lastdate,last_update_by=:lastby where bgsm_user_id=:userid", new { kdcab = user.Bgsm_User_Kdcabang, nmcab = user.Bgsm_User_Cabang, roleid = user.Bgsm_User_Roleid, rolename = user.Bgsm_User_Rolename, userid = user.Bgsm_User_Id, lastdate = DateTime.Now, lastby = user.Last_Update_By });
            }
            return res;
        }
        public static int ActivateUser(int userid, string newval)
        {
            int res = 0;
            using (var database = new DapperLabFactory())
            {
                res = database.UpdateOrDeleteRecord("update bgsm_user set bgsm_user_isaktif = :newval where bgsm_user_id=:bgsm_user_id", new { newval = newval, bgsm_user_id = userid });
            }
            return res;
        }
        #endregion
        #region DATATABLE
        public static string getTableWithJSON(int sEcho, int iDisplayStart, int iDisplayLength, string sSearch)
        {
            int end = iDisplayStart + iDisplayLength;
            Random rnd = new Random();
            string Search = sSearch;
            int id = iDisplayStart + 1;
            ListRec listRec = new ListRec();
            listRec.sEcho = sEcho;
            listRec.aaData = new List<object>();
            if (Search == "")
            {
                int apCounts = getCount();
                List<BgsmUser> apLists = new List<BgsmUser>();
                apLists = getData(iDisplayStart + 1, iDisplayStart + iDisplayLength);
                listRec.iTotalRecords = apCounts;
                listRec.iTotalDisplayRecords = apCounts;
                for (int i = 0; i < apCounts; i++)
                {
                    string lastlog = apLists[i].Bgsm_User_Last_login.ToString("dd/MM/yyyy") == "01/01/0001" ? "" : apLists[i].Bgsm_User_Last_login.ToString("dd/MM/yyyy HH:mm");
                    string action = string.Empty;
                    if (apLists[i].Bgsm_User_Isaktif == "Y")
                        action = "<a href='javascript:;' class='btn btn-primary btn-xs btn-edituser'><i class='fa fa-edit'></i> Edit</a> <a href='javascript:;' class='btn btn-danger btn-xs btn-activate' data-isaktif='N'><i class='fa fa-times'></i> Nonaktifkan</a>";
                    else
                        action = "<a href='javascript:;' class='btn btn-primary btn-xs btn-edituser'><i class='fa fa-edit'></i> Edit</a> <a href='javascript:;' class='btn btn-default btn-xs btn-activate' data-isaktif='Y'><i class='fa fa-times'></i> Aktifkan</a>";

                    listRec.aaData.Add(new object[] { 
                        apLists[i].Bgsm_User_Id, 
                        apLists[i].Bgsm_User_Username, 
                        apLists[i].Bgsm_User_Kdcabang,
                        apLists[i].Bgsm_User_Cabang,
                        apLists[i].Bgsm_User_Nama,
                        lastlog,
                        apLists[i].Bgsm_User_Roleid,
                        apLists[i].Bgsm_User_Rolename,
                        action,
                        apLists[i].Bgsm_User_Isaktif
                    });
                }
            }
            else
            {
                int apCountsSearch = getCountSearch(Search);
                List<BgsmUser> apListsSearch = new List<BgsmUser>();
                apListsSearch = getDataSearch(iDisplayStart + 1, iDisplayStart + iDisplayLength, Search);
                listRec.iTotalRecords = apCountsSearch;
                listRec.iTotalDisplayRecords = apCountsSearch;
                for (int i = 0; i < apCountsSearch; i++)
                {
                    string lastlog = apListsSearch[i].Bgsm_User_Last_login.ToString("dd/MM/yyyy") == "01/01/0001" ? "" : apListsSearch[i].Bgsm_User_Last_login.ToString("dd/MM/yyyy HH:mm");
                    string action = string.Empty;
                    if (apListsSearch[i].Bgsm_User_Isaktif == "Y")
                        action = "<a href='javascript:;' class='btn btn-primary btn-xs btn-edituser'><i class='fa fa-edit'></i> Edit</a> <a href='javascript:;' class='btn btn-danger btn-xs btn-activate' data-isaktif='N'><i class='fa fa-times'></i> Nonaktifkan</a>";
                    else
                        action = "<a href='javascript:;' class='btn btn-primary btn-xs btn-edituser'><i class='fa fa-edit'></i> Edit</a> <a href='javascript:;' class='btn btn-default btn-xs btn-activate' data-isaktif='Y'><i class='fa fa-times'></i> Aktifkan</a>";
                    listRec.aaData.Add(new object[] { 
                    apListsSearch[i].Bgsm_User_Id, 
                        apListsSearch[i].Bgsm_User_Username, 
                        apListsSearch[i].Bgsm_User_Kdcabang,
                        apListsSearch[i].Bgsm_User_Cabang,
                        apListsSearch[i].Bgsm_User_Nama,
                        lastlog,
                        apListsSearch[i].Bgsm_User_Roleid,
                        apListsSearch[i].Bgsm_User_Rolename,
                        action,
                        apListsSearch[i].Bgsm_User_Isaktif
                    });
                }
            }
            return JsonConvert.SerializeObject(listRec, Formatting.Indented);
        }

        public static List<BgsmUser> getData(int offset, int limit)
        {
            List<BgsmUser> list = new List<BgsmUser>();
            using (var database = new DapperLabFactory())
            {
                list = database.GetListViewPerPage<BgsmUser>("BGSM_USER", "BGSM_USER_ID DESC", "", null, offset, limit).ToList();
            }
            return list;
        }
        public static int getCount()
        {
            int count = 0;
            using (var database = new DapperLabFactory())
            {
                count = database.GetScalarNoParam<int>("select count(*) from BGSM_USER");
            }
            return count;
        }
        public static List<BgsmUser> getDataSearch(int offset, int limit, string keyword)
        {
            List<BgsmUser> list = new List<BgsmUser>();
            using (var database = new DapperLabFactory())
            {
                list = database.GetListViewPerPage<BgsmUser>("BGSM_USER", "BGSM_USER_ID DESC", "WHERE UPPER(BGSM_USER_CABANG) LIKE UPPER(:cabang) OR UPPER(BGSM_USER_NAMA) LIKE UPPER(:nama)", new { cabang = keyword, nama = keyword }, offset, limit).ToList();
            }
            return list;
        }
        public static int getCountSearch(string keyword)
        {
            int count = 0;
            using (var database = new DapperLabFactory())
            {
                count = database.GetScalarWithParam<int>("select count(*) from BGSM_USER where UPPER(BGSM_USER_CABANG) LIKE UPPER(:cabang) OR UPPER(BGSM_USER_NAMA) LIKE UPPER(:nama)", new { cabang = keyword, nama = keyword });
            }
            return count;
        }
        #endregion
        #region SELECT LIST
        public static string GetCabangSelect()
        {
            List<VMasterCabang> list = new List<VMasterCabang>();
            using (var database = new DapperLabFactory())
            {
                list = database.GetListNoParam<VMasterCabang>("select * from V_MASTER_CABANG").ToList();
            }
            return JsonConvert.SerializeObject(list, Formatting.Indented);
        }
        public static string GetRolesSelect()
        {
            List<BgsmHakAkses> list = new List<BgsmHakAkses>();
            using (var database = new DapperLabFactory())
            {
                list = database.GetListNoParam<BgsmHakAkses>("select BGSM_AKSES_ID,BGSM_AKSES_ROLE from BGSM_HAK_AKSES").ToList();
            }
            return JsonConvert.SerializeObject(list, Formatting.Indented);
        }
        #endregion
    }
}
