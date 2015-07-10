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
    public static class RegisterUserCtrl
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
        public static int registerUser(string obj)
        {
            int res = 0;
            string upass = string.Empty;
            BgsmUser newUser = JsonConvert.DeserializeObject<BgsmUser>(obj);
            using (MD5 md5Hash = MD5.Create())
            {
                upass = GetMd5Hash(md5Hash, newUser.Bgsm_User_Password);
            }
            using (var database = new DapperLabFactory())
            {
                res = database.InsertRecord(new
                {
                    BGSM_USER_USERNAME = newUser.Bgsm_User_Username,
                    BGSM_USER_PASSWORD = upass,
                    BGSM_USER_NAMA = newUser.Bgsm_User_Nama,
                    CREATION_DATE = DateTime.Now,
                    CREATION_BY = "BGS SYS",
                    BGSM_USER_ISAKTIF = "N"
                }, "BGSM_USER",
                    "BGSM_USER_USERNAME,BGSM_USER_PASSWORD,BGSM_USER_NAMA,CREATION_DATE,CREATION_BY,BGSM_USER_ISAKTIF");
            }
            return res;
        }
    }
}
