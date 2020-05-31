using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Models
{
    public class DataConnection
    {
        private string _connectionString;
        //private readonly SqlConnection db;
        public DataConnection(string connectionString)
        {
            _connectionString = connectionString;
            //db = new SqlConnection(_connectionString);
        }

        public List<T> ToList<T>(string expression, Func<SqlDataReader, List<T>> mapper, SqlParameter[] parameters = null)
        {
            SqlConnection db = new SqlConnection(_connectionString);
            try
            {
                db.Open();
                SqlCommand command = new SqlCommand(expression, db);
                if (parameters != null)
                {
                    for (int i = 0; i < parameters.Length; i++)
                    {

                        command.Parameters.Add(parameters[i]);
                    }
                }
                SqlDataReader reader = command.ExecuteReader();
                return mapper(reader);
            }
            finally
            {
                db.Dispose();
                GC.SuppressFinalize(db);
            }
        }
        public T ToObject<T>(string expression, Func<SqlDataReader, T> mapper, SqlParameter[] parameters = null)
        {
            SqlConnection db = new SqlConnection(_connectionString);
            try
            {
                db.Open();
                SqlCommand command = new SqlCommand(expression, db);
                if (parameters != null)
                {
                    for (int i = 0; i < parameters.Length; i++)
                    {

                        command.Parameters.Add(parameters[i]);
                    }
                }
                SqlDataReader reader = command.ExecuteReader();
                return mapper(reader);
            }
            finally
            {
                db.Dispose();
                GC.SuppressFinalize(db);
            }
        }
        public int ExecuteNonQuery(string expression, SqlParameter[] parameters = null)
        {
            SqlConnection db = new SqlConnection(_connectionString);
            try
            {
                db.Open();
                SqlCommand command = new SqlCommand(expression, db);
                if (parameters!=null)
                for (int i = 0; i < parameters.Length; i++)
                {

                    command.Parameters.Add(parameters[i]);
                }
                var value = command.ExecuteNonQuery();
                return value;
            }
            finally
            {
                db.Dispose();
                GC.SuppressFinalize(db);
            }
        }

        //public void Dispose() 
        //{
        //    db.Close();
        //}
    }
}