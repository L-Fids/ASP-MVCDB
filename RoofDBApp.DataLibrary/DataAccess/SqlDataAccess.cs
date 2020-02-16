using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using RoofDBApp.DataLibrary.Models;

namespace RoofDBApp.DataLibrary.DataAccess
{
    public static class SqlDataAccess // might as well be static, not going to store data here
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

        
        //public static List<T> LoadDataMultiple<T>(string sql, T entity1, T entity2, T entity3, string splitOn)
        //{
        //    using (IDbConnection connection = new SqlConnection(GetConnectionString()))
        //    {
        //        return connection.Query<T, T, T>(sql, (entity1, entity2),).ToList();
        //    }
        //}
    }
}
