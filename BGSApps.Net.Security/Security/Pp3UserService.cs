using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BGSApps.Net.Security.ServiceP3UserManagement;
using BGSApps.Net.DapperFactory;
using System.Web;
using System.Web.UI;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using BGSApps.Net.Model.Core;

namespace BGSApps.Net.Security.Security
{
    public static class Pp3UserService
    {
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
        public static bool isValidUserPp3WebService(string username, string password, Page page)
        {
            string desc = string.Empty;
            wsOuthSoap xService;
            UserAkunInfo akun;
            bool valid = false;
            try
            {
                xService = new wsOuthSoapClient();
                akun = new UserAkunInfo();
                akun = xService.valLoginAkun("66", username, password);

                if (akun.responType.Substring(0, 1) == "S")
                {
                    HttpContext.Current.Session["UserId"] = akun.IDUSER;
                    HttpContext.Current.Session["UserName"] = akun.USERNAME;
                    HttpContext.Current.Session["Nama"] = akun.NAMA;
                    HttpContext.Current.Session["KodeCabang"] = akun.KD_CABANG;
                    HttpContext.Current.Session["NamaCabang"] = akun.NAMA_CABANG;
                    HttpContext.Current.Session["KodeDit"] = akun.KD_DIT;
                    HttpContext.Current.Session["NamaDit"] = akun.NAMA_DIT;
                    HttpContext.Current.Session["Email"] = akun.EMAIL;
                    HttpContext.Current.Session["NamaJabatan"] = akun.NAMA_JABATAN;
                    HttpContext.Current.Session["HakAkses"] = akun.HAKAKSES;
                    valid = true;
                }
                else
                {
                    if (akun.responType == "E-003" || akun.responType == "E-018") valid = false;
                    else valid = false;
                }
            }
            catch (Exception ex)
            {
                desc = ex.Message;
            }
            return valid;
        }
        public static bool isValidCanLogin(string username, string password, Page page)
        {
            bool canLogin = false;
            using (MD5 md5Hash = MD5.Create())
            {
                password = GetMd5Hash(md5Hash, password);
            }
            using (var database = new DapperLabFactory())
            {
                int count = database.GetScalarWithParam<int>("select count(*) AS CANLOGIN from BGSM_USER where BGSM_USER_USERNAME = :username and BGSM_USER_PASSWORD = :pswd and  BGSM_USER_ISAKTIF = :isaktif", new { username = username, pswd = password, isaktif = "Y" });
                canLogin = count == 0 ? false : true;
                if (canLogin)
                {
                    BgsmUser user = database.GetListWithParam<BgsmUser>("select * from BGSM_USER where BGSM_USER_USERNAME = :username", new { username = username }).DefaultIfEmpty(new BgsmUser()).SingleOrDefault();
                    HttpContext.Current.Session["UserName"] = user.Bgsm_User_Username;
                    HttpContext.Current.Session["Nama"] = user.Bgsm_User_Nama;
                    HttpContext.Current.Session["KodeCabang"] = user.Bgsm_User_Kdcabang;
                    HttpContext.Current.Session["NamaCabang"] = user.Bgsm_User_Cabang;
                    HttpContext.Current.Session["HakAkses"] = user.Bgsm_User_Roleid;
                }
            }
            return canLogin;
        }
    }
}
