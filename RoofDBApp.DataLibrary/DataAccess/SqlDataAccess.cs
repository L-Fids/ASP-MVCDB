using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace RoofDBApp.DataLibrary.DataAccess
{
    public static class SqlDataAccess
    {
        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["RoofDBConnection"].ConnectionString;
        }

        public static List<T> LoadData<T>(string sql)
        {
            using (IDbConnection connection = new SqlConnection(GetConnectionString()))
            {
                return connection.Query<T>(sql).ToList();
            }
        }

        public static List<T> LoadSingleData<T>(string sql, DynamicParameters parameter)
        {
            using (IDbConnection connection = new SqlConnection(GetConnectionString()))
            {
                return connection.Query<T>(sql, parameter).ToList();
            }
        }

        public static T LoadSingle<T>(string sql, DynamicParameters parameter)
        {
            using (IDbConnection connection = new SqlConnection(GetConnectionString()))
            {
                return connection.Query<T>(sql, parameter).Single();
            }
        }

        // WORK IN PROGRESS NOT USEABLE
        public static T SaveDataReturnID<T>(string sql, T data)
        {
            using (IDbConnection connection = new SqlConnection(GetConnectionString()))
            {
                return connection.Query<T>(sql, data).Single();
            }
        }

        public static int SaveData<T>(string sql, T data)
        {
            using (IDbConnection connection = new SqlConnection(GetConnectionString()))
            {
                return connection.Execute(sql, data);
            }
        }
        
        public static int DeleteData(string sql, DynamicParameters parameters)
        {
            using (IDbConnection connection = new SqlConnection(GetConnectionString()))
            {
                return connection.Execute(sql, parameters);
            }
        }

        public static V LoadDataSingleMultiMap<T,U,V>(string sql, Func<T, U, V> mapResults, Object declareScalar, string splitCategory)
        {
            using (IDbConnection connection = new SqlConnection(GetConnectionString()))
            {
                return connection.Query<T, U, V>(
                    sql,
                    mapResults,
                    declareScalar,
                    splitOn: splitCategory).First();
            }
        }

        public static List<V> LoadDataMultiMap<T, U, V>(string sql, Func<T, U, V> mapResults, string splitCategory)
        {
            using (IDbConnection connection = new SqlConnection(GetConnectionString()))
            {
                return connection.Query<T, U, V>(
                    sql,
                    mapResults,
                    splitOn: splitCategory).ToList();
            }
        }
    }
}
