using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BGSApps.Net.Model.Core
{
    public class HakAksesControlById
    {
        public virtual short Bgsm_Menu_Id { get; set; }
        public virtual string Bgsm_Menu_Nama { get; set; }
        public virtual short Bgsm_Menu_Parent { get; set; }
        public virtual short Status { get; set; }
        public virtual List<HakAksesControlById> Childs { get; set; }

    }
}
