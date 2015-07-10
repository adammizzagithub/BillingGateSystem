using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BGSApps.Net.Model.Core
{
    public class BgsmAksesControl
    {
        public virtual short Bgsm_Control_Aksesid { get; set; }
        public virtual short Bgsm_Control_Menuid { get; set; }
        public virtual DateTime Created_Date { get; set; }
        public virtual string Created_By { get; set; }
    }
}
