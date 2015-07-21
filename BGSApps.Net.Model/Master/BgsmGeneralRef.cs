using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BGSApps.Net.Model.Master
{
    public class BgsmGeneralRef
    {
        public virtual string KD_CABANG { get; set; }
        public virtual string JENIS_TABLE { get; set; }
        public virtual string ID_TABLE { get; set; }
        public virtual string KODE_MODUL { get; set; }
        public virtual string ID_REF_FILE { get; set; }
        public virtual string KET_REFERENCE { get; set; }
        public virtual string KD_AKTIF { get; set; }
        public virtual string ATTRIB1 { get; set; }
        public virtual string ATTRIB2 { get; set; }
        public virtual string VAL1 { get; set; }
        public virtual string VAL2 { get; set; }
        public virtual DateTime LAST_UPDATED_DATE { get; set; }
        public virtual string LAST_UPDATED_BY { get; set; }
        public virtual string PROGRAM_NAME { get; set; }
        public virtual List<BgsmGeneralRefDetail> Details { get; set; }
    }
}
