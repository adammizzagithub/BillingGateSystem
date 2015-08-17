using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BGSApps.Net.Model.Transaksi
{
    public class BgstRkpallPasHr
    {
        public virtual short KdCabang { get; set; }
        public virtual string NoRkp { get; set; }
        public virtual DateTime TglTrans { get; set; }
        public virtual string Shift { get; set; }
        public virtual string Gate { get; set; }
        public virtual string KdOperator { get; set; }
        public virtual string Tahun { get; set; }
        public virtual string Bulan { get; set; }
        public virtual string JnsKend { get; set; }
        public virtual decimal Jumlah { get; set; }
        public virtual decimal Tarif { get; set; }
        public virtual decimal Pendapatan { get; set; }
        public virtual decimal Ppn { get; set; }
        public virtual string Keterangan { get; set; }
        public virtual string StsRkp { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual DateTime LastUpdatedDate { get; set; }
        public virtual string LastUpdatedBy { get; set; }
        public virtual string ProgramName { get; set; }
        public virtual decimal NoAwal { get; set; }
        public virtual decimal NoAkhir { get; set; }
    }
}
