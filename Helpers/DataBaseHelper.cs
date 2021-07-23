using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Distribuidora.Helpers
{
    static class DataBaseHelper
    {
        public static object ExecStoredProcedure(string storedProcedure, List<SqlParameter> parameters)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
            SqlConnection Connection = new SqlConnection(ConnectionString);
            Connection.ConnectionString = ConnectionString;
            Connection.Open();
            SqlCommand sqlCommand = Connection.CreateCommand();

            sqlCommand.Connection = Connection;

            try
            {
                object result = null;

                sqlCommand.CommandText = storedProcedure;
                sqlCommand.CommandType = CommandType.StoredProcedure;

                foreach (SqlParameter sqlPrm in parameters)
                {
                    sqlCommand.Parameters.Add(sqlPrm);
                }

                result = sqlCommand.ExecuteScalar();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlCommand.Dispose();
                Connection.Close();
            }
        }

        public static object ExecFunction(string query)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
            SqlConnection Connection = new SqlConnection(ConnectionString);
            Connection.ConnectionString = ConnectionString;
            Connection.Open();
            SqlCommand sqlCommand = Connection.CreateCommand();
            sqlCommand.Connection = Connection;

            try
            {
                sqlCommand.CommandText = query;

                return sqlCommand.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable ExecQuery(string query)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
            SqlConnection Connection = new SqlConnection(ConnectionString);
            Connection.ConnectionString = ConnectionString;
            Connection.Open();
            SqlCommand sqlCommand = Connection.CreateCommand();
            SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);

            sqlCommand.Connection = Connection;

            try
            {
                DataSet dataSet = new DataSet();

                sqlCommand.CommandText = query;
                sqlCommand.CommandType = CommandType.Text;


                sqlAdapter.Fill(dataSet);

                return dataSet.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlCommand.Dispose();
                sqlAdapter.Dispose();
                Connection.Close();
            }
        }

        public static void ExecScript(string query)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
            SqlConnection Connection = new SqlConnection(ConnectionString);
            Connection.ConnectionString = ConnectionString;
            Connection.Open();
            SqlCommand sqlCommand = Connection.CreateCommand();
            sqlCommand.Connection = Connection;

            try
            {
                sqlCommand.CommandText = query;
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}