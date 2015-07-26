using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using Oracle.DataAccess.Client;
using System.Configuration;
using System.Data;
using System.Globalization;

namespace BGSApps.Net.DapperFactory
{
    public class DapperLabFactory : IDisposable
    {
        OracleConnection connection;

        public DapperLabFactory()
        {
            string connectionString = "Data Source=(DESCRIPTION="
                + "(ADDRESS=(PROTOCOL=TCP)(HOST=" + ConfigurationManager.AppSettings["ServerName"] +
                ")(PORT=" + ConfigurationManager.AppSettings["PortID"] + "))" +
                "(CONNECT_DATA=(SERVICE_NAME=" + ConfigurationManager.AppSettings["ServiceName"] + ")));" +
                "User Id=" + ConfigurationManager.AppSettings["UserName"] + ";" +
                "Password=" + ConfigurationManager.AppSettings["UserPassword"] + ";Min Pool Size=5;Max Pool Size=1100;";
            connection = new OracleConnection(connectionString);
        }
        public DataTable GetListViewSuperQuery(string TableName, string orderby, string Wherecondition, List<KeyValuePair<string, object>> parameters, int offset, int limit)
        {
            DataTable dtable = new DataTable();
            OracleCommand cmd;
            var sql = "SELECT * FROM (SELECT T.*, ROW_NUMBER() OVER (ORDER BY @OrderBy) MYROW " +
                   "FROM @TableName T " + Wherecondition + ") " +
                   "WHERE MYROW BETWEEN :OFFSET AND :LIMIT";
            sql = sql.Replace("@TableName", TableName);
            sql = sql.Replace("@OrderBy", orderby);
            try
            {
                connection.Open();
                cmd = new OracleCommand();
                cmd.Connection = connection;
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;
                cmd.BindByName = true;

                foreach (var elem in parameters)
                    cmd.Parameters.Add(new OracleParameter(elem.Key, elem.Value));
                cmd.Parameters.Add(new OracleParameter("OFFSET", offset));
                cmd.Parameters.Add(new OracleParameter("LIMIT", limit));
                OracleDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    dtable.Load(rdr);
                }
                rdr.Close();
            }
            catch
            {
                connection.Close();
            }
            return dtable;
        }
        public decimal GetCountViewSuperQuery(string TableName, string Wherecondition, List<KeyValuePair<string, object>> parameters)
        {
            decimal count = 0;
            OracleCommand cmd;
            var sql = "SELECT COUNT(*) FROM @TableName " + Wherecondition + "";
            sql = sql.Replace("@TableName", TableName);
            try
            {
                connection.Open();
                cmd = new OracleCommand();
                cmd.Connection = connection;
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;
                cmd.BindByName = true;
                foreach (var elem in parameters)
                    cmd.Parameters.Add(new OracleParameter(elem.Key, elem.Value));
                count = Convert.ToDecimal(cmd.ExecuteScalar());
            }
            catch
            {
                connection.Close();
            }
            return count;
        }
        public T GetScalarNoParam<T>(string query)
        {
            T val = connection.ExecuteScalar<T>(query);
            return val;
        }
        public T GetScalarWithParam<T>(string query, object parameters)
        {
            T val = connection.ExecuteScalar<T>(query, parameters);
            return val;
        }
        public IEnumerable<T> GetListNoParam<T>(string query)
        {
            IEnumerable<T> list = connection.Query<T>(query);
            return list;
        }
        public IEnumerable<T> GetListWithParam<T>(string query, object parameters)
        {
            IEnumerable<T> list = connection.Query<T>(query, parameters);
            return list;
        }
        public IEnumerable<T> GetListViewPerPage<T>(string tableName, string orderby, string wherecondition, object parameters, int offset, int limit)
        {
            var sql = "SELECT * FROM (SELECT T.*, ROW_NUMBER() OVER (ORDER BY @OrderBy) MYROW " +
                    "FROM @TableName T " + wherecondition + ") " +
                    "WHERE MYROW BETWEEN " + offset + " AND " + limit + "";
            sql = sql.Replace("@TableName", tableName);
            sql = sql.Replace("@OrderBy", orderby);
            IEnumerable<T> list;
            if (wherecondition.Equals(""))
                list = connection.Query<T>(sql);
            else
                list = connection.Query<T>(sql, parameters);
            return list;
        }
        public int InsertRecord(object param, string table, string colom = "")
        {
            int success = 0;
            try
            {
                string valParam = string.Empty;
                string[] paramValue = colom.Split(',');
                for (int i = 0; i < paramValue.Length; i++)
                    paramValue[i] = ":" + paramValue[i].Trim();
                valParam = paramValue.Aggregate((current, next) => current + "," + next);
                string stmt = "INSERT INTO " + table + "(" + colom + ") values (" + valParam + ")";
                connection.Execute(stmt, param);
                success = 1;
            }
            catch
            {
                success = 0;
            }
            return success;
        }
        public int InsertMultipleRecord(string stmt, object[] param)
        {
            int success = 0;
            try
            {
                connection.Execute(stmt, param);
                success = 1;
            }
            catch
            {
                success = 0;
            }
            return success;
        }
        public int UpdateOrDeleteRecord(string query, object param)
        {
            int success = 0;
            try
            {
                connection.Execute(query, param);
                success = 1;
            }
            catch
            {
                success = 0;
            }
            return success;
        }
        public int RunProcedure(string procedureName, DynamicParameters parameter)
        {
            int success = 0;
            try
            {
                connection.Execute(procedureName, parameter, commandType: System.Data.CommandType.StoredProcedure);
                success = 1;
            }
            catch
            {
                success = 0;
            }
            return success;
        }
        #region ADDITIONAL GROUP BY
        public DataTable GroupByListView(string columnToGroup, string table, string Wherecondition, List<KeyValuePair<string, object>> parameters)
        {
            DataTable dtable = new DataTable();
            OracleCommand cmd;
            var sql = "SELECT * FROM (SELECT T.*, ROW_NUMBER() OVER (ORDER BY @COLUMNGROUP ASC) MYROW FROM (select " +
                           "@COLUMNGROUP," +
                           "COUNT(*) jumlah " +
                        "from " +
                        "@TABLE " +
                        "" + Wherecondition + "" +
                        "group by @COLUMNGROUP) T) WHERE MYROW BETWEEN :OFFSET AND :LIMIT";

            sql = sql.Replace("@TABLE", table);
            sql = sql.Replace("@COLUMNGROUP", columnToGroup);
            try
            {
                connection.Open();
                cmd = new OracleCommand();
                cmd.Connection = connection;
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;
                cmd.BindByName = true;

                foreach (var elem in parameters)
                    cmd.Parameters.Add(new OracleParameter(elem.Key, elem.Value));
                cmd.Parameters.Add(new OracleParameter("OFFSET", 1));
                cmd.Parameters.Add(new OracleParameter("LIMIT", 10));
                OracleDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    dtable.Load(rdr);
                }
                rdr.Close();
            }
            catch
            {
                connection.Close();
            }
            return dtable;
        }
        public decimal GroupByCount(string columnToGroup, string table, string Wherecondition, List<KeyValuePair<string, object>> parameters)
        {
            decimal count = 0;
            OracleCommand cmd;
            var sql = "SELECT COUNT(*) FROM (select " +
                           "@COLUMNGROUP," +
                           "COUNT(*) jumlah " +
                        "from " +
                        "@TableName " +
                        "" + Wherecondition + " " +
                        "group by @COLUMNGROUP)";
            sql = sql.Replace("@TableName", table);
            sql = sql.Replace("@COLUMNGROUP", columnToGroup);
            try
            {
                connection.Open();
                cmd = new OracleCommand();
                cmd.Connection = connection;
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;
                cmd.BindByName = true;
                foreach (var elem in parameters)
                    cmd.Parameters.Add(new OracleParameter(elem.Key, elem.Value));
                count = Convert.ToDecimal(cmd.ExecuteScalar());
            }
            catch
            {
                connection.Close();
            }
            return count;
        }
        #endregion
        public void Dispose()
        {
            try
            {
                connection.Close();
            }
            catch
            {

            }
            finally
            {
                connection.Close();
            }
        }
    }
}
