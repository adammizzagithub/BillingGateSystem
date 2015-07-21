using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using BGSApps.Net.Model.Additional;

namespace BGSApps.Net.Controller.Helper
{
    public static class HelperFactory
    {
        public static string ResolveUrl(string url)
        {
            Page page = HttpContext.Current.Handler as Page;

            if (page == null)
            {
                return url;
            }

            return (page).ResolveUrl(url);
        }
        public static string querySearchBuilder(List<JsonParamsTable> parameters)
        {
            string clause = "";
            validateEmptyValue(parameters);
            for (int i = 0; i < parameters.Count; i++)
            {
                clause += i == 0 ? "WHERE " : "";
                switch (parameters[i].columnType)
                {
                    case "string":
                        clause += "UPPER(" + parameters[i].columnName + ") LIKE UPPER(:" + parameters[i].columnName + ")";
                        break;
                    case "number":
                        clause += parameters[i].columnName + " = :" + parameters[i].columnName;
                        break;
                }
                clause += i == parameters.Count - 1 ? "" : " AND ";
            }

            return clause;
        }
        private static void validateEmptyValue(List<JsonParamsTable> param)
        {
            for (int i = 0; i < param.Count; i++)
            {
                if (param[i].columnValue.Equals("") || param[i].columnValue.Equals(null) || param[i].columnValue.Equals("~"))
                {
                    param.RemoveAt(i);
                    i = -1;
                }
            }
        }
    }
}
