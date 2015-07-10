using BGSApps.Net.DapperFactory;
using BGSApps.Net.Model.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BGSApps.Net.Controller.Core
{
    public class AksesControlJson
    {
        public virtual List<BgsmAksesControl> multipleValue { get; set; }
    }
    public static class AccessSettingCtrl
    {
        private static string queryControlAkses()
        {
            string query = string.Empty;
            query = "select TBL.BGSM_MENU_ID," +
                                       "TBL.BGSM_MENU_NAMA," +
                                       "TBL.BGSM_MENU_PARENT," +
                                       "TBL.STATUS " +
                                "from (" +
                                "select BM.BGSM_MENU_ID,BM.BGSM_MENU_NAMA,BM.BGSM_MENU_PARENT,1 AS STATUS from BGSM_MENU BM " +
                                "where BM.BGSM_MENU_ID IN (" +
                                "select BGSM_AKSES_CONTROL.BGSM_CONTROL_MENUID from BGSM_AKSES_CONTROL where BGSM_AKSES_CONTROL.BGSM_CONTROL_AKSESID = :hakakses)  " +
                                " UNION " +
                                "select BM.BGSM_MENU_ID,BM.BGSM_MENU_NAMA,BM.BGSM_MENU_PARENT,0 AS STATUS from BGSM_MENU BM " +
                                "where BM.BGSM_MENU_ID NOT IN (" +
                                "select BGSM_AKSES_CONTROL.BGSM_CONTROL_MENUID from BGSM_AKSES_CONTROL where BGSM_AKSES_CONTROL.BGSM_CONTROL_AKSESID = :hakakses))TBL " +
                                "WHERE TBL.BGSM_MENU_PARENT = :parent";
            return query;
        }
        public static string GetRoles()
        {
            List<BgsmHakAkses> list = new List<BgsmHakAkses>();
            using (var database = new DapperLabFactory())
            {
                list = database.GetListNoParam<BgsmHakAkses>("select * from bgsm_hak_akses order by bgsm_akses_role asc").ToList();
            }
            return JsonConvert.SerializeObject(list, Formatting.Indented);
        }
        public static string GetRolesByHakAksesId(int HakAksesId)
        {
            List<HakAksesControlById> controlList = new List<HakAksesControlById>();
            using (var database = new DapperLabFactory())
            {
                controlList = database.GetListWithParam<HakAksesControlById>(queryControlAkses(), new { hakakses = HakAksesId, parent = -1 }).ToList();
                getRecursiveRolesByHakAksesId(controlList, HakAksesId);
            }
            return JsonConvert.SerializeObject(controlList);
        }
        public static void getRecursiveRolesByHakAksesId(List<HakAksesControlById> menus, int HakAksesId)
        {
            foreach (var item in menus)
            {
                List<HakAksesControlById> nestedChilds = new List<HakAksesControlById>();
                using (var database = new DapperLabFactory())
                {
                    nestedChilds = database.GetListWithParam<HakAksesControlById>(queryControlAkses(), new { hakakses = HakAksesId, parent = item.Bgsm_Menu_Id }).ToList();
                }
                item.Childs = nestedChilds;
                if (item.Childs.Count != 0)
                    getRecursiveRolesByHakAksesId(item.Childs, HakAksesId);
            }
        }
        #region CRUD
        public static int CreateNewRole(string obj)
        {
            int res = 0;
            string upass = string.Empty;
            BgsmHakAkses bgsmRole = JsonConvert.DeserializeObject<BgsmHakAkses>(obj);
            using (var database = new DapperLabFactory())
            {
                res = database.InsertRecord(new
                {
                    BGSM_AKSES_ROLE = bgsmRole.Bgsm_Akses_Role,
                    BGSM_AKSES_STATUS = bgsmRole.Bgsm_Akses_Status,
                    CREATION_DATE = DateTime.Now,
                    CREATION_BY = bgsmRole.Creation_By,
                }, "BGSM_HAK_AKSES"
                 , "BGSM_AKSES_ROLE, BGSM_AKSES_STATUS, CREATION_DATE, CREATION_BY");
            }
            return res;
        }
        public static int DeleteRole(int roleid)
        {
            int result = 0;
            using (var database = new DapperLabFactory())
            {
                int resdel = database.UpdateOrDeleteRecord("delete from bgsm_akses_control where bgsm_control_aksesid = :aksesid", new { aksesid = roleid });
                if (resdel == 1)
                    result = database.UpdateOrDeleteRecord("delete from bgsm_hak_akses where bgsm_akses_id = :aksesid", new { aksesid = roleid });
            }
            return result;
        }
        public static int CreateControlByRole(string obj)
        {
            int res = 0;
            AksesControlJson jsonobj = JsonConvert.DeserializeObject<AksesControlJson>(obj);
            using (var database = new DapperLabFactory())
            {
                int resdel = database.UpdateOrDeleteRecord("delete from BGSM_AKSES_CONTROL where BGSM_CONTROL_AKSESID=:aksesid", new { aksesid = jsonobj.multipleValue[0].Bgsm_Control_Aksesid });
                if (resdel == 1)
                {
                    for (int i = 0; i < jsonobj.multipleValue.Count; i++)
                    {
                        res = database.InsertRecord(new
                        {
                            BGSM_CONTROL_AKSESID = jsonobj.multipleValue[i].Bgsm_Control_Aksesid,
                            BGSM_CONTROL_MENUID = jsonobj.multipleValue[i].Bgsm_Control_Menuid,
                            CREATED_DATE = DateTime.Now,
                            CREATED_BY = jsonobj.multipleValue[i].Created_By
                        }, "BGSM_AKSES_CONTROL"
                     , "BGSM_CONTROL_AKSESID,BGSM_CONTROL_MENUID,CREATED_DATE,CREATED_BY");
                    }
                }
            }
            return res;
        }
        #endregion
    }
}
