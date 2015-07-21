using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BGSApps.Net.Model.Master
{
    public class BgsmJnsKend
    {
        public virtual string KD_KEND { get; set; }
        public virtual short KD_CABANG { get; set; }
        public virtual string JNS_KENDARAAN { get; set; }
        public virtual string KENA_PPN { get; set; }
        public virtual DateTime LAST_UPDATE_DATE { get; set; }
        public virtual string LAST_UPDATE_BY { get; set; }
        public virtual string PROGRAME_NAME { get; set; }
        public virtual string INEX_PPN { get; set; }
        public virtual string KLAS_TRANS { get; set; }
        public virtual string REF_CODE { get; set; }
    }
}
