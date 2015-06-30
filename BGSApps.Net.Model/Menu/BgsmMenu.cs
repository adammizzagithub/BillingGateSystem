using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BGSApps.Net.Model.Menu
{
    public class BgsmMenu
    {
        public virtual decimal Bgsm_Menu_Id { get; set; }
        public virtual string Bgsm_Menu_Nama { get; set; }
        public virtual string Bgsm_Menu_Vurl { get; set; }
        public virtual string Bgsm_Menu_Purl { get; set; }
        public virtual string Bgsm_Menu_Icon { get; set; }
        public virtual decimal Bgsm_Menu_Parent { get; set; }
        public virtual decimal Bgsm_Menu_Urut { get; set; }
        public virtual DateTime Creation_Date { get; set; }
        public virtual string Creation_By { get; set; }
        public virtual DateTime Last_Update_Date { get; set; }
        public virtual string Last_Update_By { get; set; }
        public virtual List<BgsmMenu> Childs { get; set; }
    }
}
