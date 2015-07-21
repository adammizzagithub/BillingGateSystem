using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BGSApps.Net.Model.Master
{
    public class BgsmMstTarif
    {
        public virtual string KD_TARIF { get; set; }
        public virtual short KD_CABANG { get; set; }
        public virtual string KD_PLY { get; set; }
        public virtual string NO_BA { get; set; }
        public virtual string TGL_TMT { get; set; }
        public virtual string KET_TARIF { get; set; }
        public virtual DateTime LAST_UPDATE_DATE { get; set; }
        public virtual string LAST_UPDATE_BY { get; set; }
        public virtual string PROGRAME_NAME { get; set; }
        public virtual string NM_PLY { get; set; }
        public virtual List<BgsmMstTarifD> Details { get; set; }
    }
}
