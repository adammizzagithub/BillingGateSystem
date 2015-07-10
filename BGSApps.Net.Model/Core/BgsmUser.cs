using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BGSApps.Net.Model.Core
{
    public class BgsmUser
    {
        public virtual decimal Bgsm_User_Id { get; set; }
        public virtual string Bgsm_User_Username { get; set; }
        public virtual string Bgsm_User_Password { get; set; }
        public virtual decimal Bgsm_User_Kdcabang { get; set; }
        public virtual string Bgsm_User_Cabang { get; set; }
        public virtual string Bgsm_User_Nama { get; set; }
        public virtual string Bgsm_User_Isaktif { get; set; }
        public virtual DateTime Bgsm_User_Last_login { get; set; }
        public virtual decimal Bgsm_User_Roleid { get; set; }
        public virtual string Bgsm_User_Rolename { get; set; }
        public virtual DateTime Creation_Date { get; set; }
        public virtual string Creation_By { get; set; }
        public virtual DateTime Last_Update_Date { get; set; }
        public virtual string Last_Update_By { get; set; }
    }
}
