using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using Oracle.DataAccess.Client;
using System.Configuration;

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
            if (wherecondition.Equals("")) list = connection.Query<T>(sql);
            else list = connection.Query<T>(sql, parameters);
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
