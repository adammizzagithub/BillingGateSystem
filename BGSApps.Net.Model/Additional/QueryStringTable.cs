using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BGSApps.Net.Model.Additional
{
    public class QueryStringTable
    {
        public virtual string orderName { get; set; }
        public virtual string orderMethod { get; set; }
        public virtual int offset { get; set; }
        public virtual int limit { get; set; }
        public virtual List<JsonParamsTable> datas { get; set; }
    }
}
