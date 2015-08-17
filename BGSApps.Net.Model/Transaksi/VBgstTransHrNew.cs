using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BGSApps.Net.Controller.Transaksi
{
    public class VBgstTransHrNew
    {
        public virtual string Gate { get; set; }
        public virtual string Jns_Kend { get; set; }
        public virtual string Jns_Kendaraan { get; set; }
        public virtual decimal Tarif { get; set; }
        public virtual decimal Qty { get; set; }
        public virtual decimal Pndp_Gross { get; set; }
    }
}
