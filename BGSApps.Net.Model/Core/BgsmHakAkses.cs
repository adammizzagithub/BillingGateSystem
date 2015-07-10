using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BGSApps.Net.Model.Core
{
    public class BgsmHakAkses
    {
        public virtual decimal Bgsm_Akses_Id { get; set; }
        public virtual string Bgsm_Akses_Role { get; set; }
        public virtual string Bgsm_Akses_Status { get; set; }
        public virtual DateTime Creation_Date { get; set; }
        public virtual string Creation_By { get; set; }
        public virtual DateTime Last_Update_Date { get; set; }
        public virtual string Last_Update_By { get; set; }
    }
}
