using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BGSApps.Net.Security.ServiceP3UserManagement;
using System.Web;
using System.Web.UI;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace BGSApps.Net.Security.Security
{
    public static class Pp3UserService
    {
        public static bool isValidUserPp3WebService(string username,string password,Page page)
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
    }
}
