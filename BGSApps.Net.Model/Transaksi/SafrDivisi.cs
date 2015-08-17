using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BGSApps.Net.Model.Transaksi
{
    public class SafrDivisi
    {
        public virtual short KD_CABANG { get; set; }
        public virtual string JENIS_TABLE { get; set; }
        public virtual string ID_TABLE { get; set; }
        public virtual string KODE_DIVISI { get; set; }
        public virtual string KODE_TERMINAL { get; set; }
        public virtual string NAMA_DIVISI { get; set; }
        public virtual DateTime LAST_UPDATED_DATE { get; set; }
        public virtual string LAST_UPDATED_BY { get; set; }
        public virtual string PROGRAM_NAME { get; set; }

    }
}
