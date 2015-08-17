using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BGSApps.Net.Model.Transaksi
{
    public class BgstDaftarPasStiker
    {
        public virtual short KD_CABANG { get; set; }
        public virtual Int64 ID { get; set; }
        public virtual string NO_STIKER { get; set; }
        public virtual string DIVISI { get; set; }
        public virtual string NAMA_DIVISI { get; set; }
        public virtual string INSTANSI { get; set; }
        public virtual string NO_ID { get; set; }
        public virtual string NAMA { get; set; }
        public virtual string NOPOL_2 { get; set; }
        public virtual string NOPOL_4 { get; set; }
        public virtual string STATUS_REC { get; set; }
        public virtual string KETERANGAN { get; set; }
        public virtual DateTime TGL_ENTRY { get; set; }
        public virtual DateTime LAST_UPDATE_DATE { get; set; }
        public virtual string LAST_UPDATE_BY { get; set; }
        public virtual string PROGRAME_NAME { get; set; }
        public virtual string NO_RFID { get; set; }
        public virtual string TGL_AKHIR { get; set; }
        public virtual string JNS_KEND { get; set; }
        public virtual string JNS_KENDARAAN { get; set; }
        public virtual string NM_DIVISI { get; set; }
        public virtual string NM_KEND { get; set; }

    }
}
