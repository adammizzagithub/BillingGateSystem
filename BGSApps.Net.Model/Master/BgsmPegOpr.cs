using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BGSApps.Net.Model.Master
{
    public class BgsmPegOpr
    {
        public virtual short KD_CABANG { get; set; }
        public virtual string NIP_PEG { get; set; }
        public virtual string NM_PEG { get; set; }
        public virtual string KD_BAGIAN { get; set; }
        public virtual string KD_GATE { get; set; }
        public virtual string NM_GATE { get; set; }
        public virtual string REC_STAT { get; set; }
        public virtual DateTime LAST_UPDATE_DATE { get; set; }
        public virtual string LAST_UPDATE_BY { get; set; }
        public virtual string PROGRAME_NAME { get; set; }
        public virtual string PASSWORD { get; set; }
    }
}
