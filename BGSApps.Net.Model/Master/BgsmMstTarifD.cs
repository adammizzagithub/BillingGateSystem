using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BGSApps.Net.Model.Master
{
    public class BgsmMstTarifD
    {
        public virtual string KD_TARIF { get; set; }
        public virtual decimal NO_SEQ { get; set; }
        public virtual short KD_CABANG { get; set; }
        public virtual string JNS_KEND { get; set; }
        public virtual decimal TARIF { get; set; }
        public virtual string KETERANGAN { get; set; }
        public virtual DateTime LAST_UPDATE_DATE { get; set; }
        public virtual string LAST_UPDATE_BY { get; set; }
        public virtual string PROGRAME_NAME { get; set; }
        public virtual string NM_KEND { get; set; }
    }
}
