using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BGSApps.Net.Model.Menu
{
    public class JsonParamMenu
    {
        public virtual int id { set; get; }
        public virtual List<JsonParamMenu> children { set; get; }

    }
}
