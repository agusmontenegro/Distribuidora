using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Persistencia.Helpers.DataBase
{
    public class DataBaseHelper : IDataBaseHelper
    {
        public List<SqlParameter> Parametros { get; set; }
        public string ConnectionString { get; set; }

        public DataBaseHelper()
        {
            Parametros = new List<SqlParameter>();
            ConnectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
        }

        public void AgregarParametroEntrada(string parametro, string parametroSQL, SqlDbType sqlDbType)
        {
            var codigoProductoParameter = new SqlParameter(parametroSQL, sqlDbType);

            switch (sqlDbType)
            {
                case SqlDbType.Decimal:
                    codigoProductoParameter.Value = decimal.Parse(parametro);
                    break;
                case SqlDbType.Int:
                    codigoProductoParameter.Value = int.Parse(parametro);
                    break;
                case SqlDbType.NVarChar:
                    codigoProductoParameter.Value = parametro;
                    break;
            }

            Parametros.Add(codigoProductoParameter);
        }

        public void AgregarParametroSalida(string parametroSQL, SqlDbType sqlDbType)
        {
            var codigoProductoParameter = new SqlParameter(parametroSQL, sqlDbType);
            codigoProductoParameter.Direction = ParameterDirection.Output;

            Parametros.Add(codigoProductoParameter);
        }

        public List<string> ExecStoredProcedure(string storedProcedure)
        {
            var parametrosOut = new List<string>();
            var Connection = AbrirConexion();
            var sqlCommand = Connection.CreateCommand();

            try
            {
                sqlCommand.CommandText = storedProcedure;
                sqlCommand.CommandType = CommandType.StoredProcedure;

                foreach (var sqlPrm in Parametros)
                {
                    sqlCommand.Parameters.Add(sqlPrm);
                }

                var result = sqlCommand.ExecuteScalar();
                var parametrosSalida = Parametros.Where(p => p.Direction == ParameterDirection.Output).ToList();

                foreach (var parametro in parametrosSalida)
                {
                    parametrosOut.Add(parametro.Value.ToString());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlCommand.Dispose();
                Connection.Close();
                Parametros.Clear();
            }

            return parametrosOut;
        }

        public object ExecFunction(string query)
        {
            var Connection = AbrirConexion();
            var sqlCommand = Connection.CreateCommand();

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

        public DataTable ExecQuery(string query)
        {
            var Connection = AbrirConexion();
            var sqlCommand = Connection.CreateCommand();
            var sqlAdapter = new SqlDataAdapter(sqlCommand);

            try
            {
                var dataSet = new DataSet();

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

        public void ExecScript(string query)
        {
            var Connection = AbrirConexion();
            var sqlCommand = Connection.CreateCommand();

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

        private SqlConnection AbrirConexion()
        {
            var Connection = new SqlConnection(ConnectionString);
            Connection.ConnectionString = ConnectionString;
            Connection.Open();
            return Connection;
        }
    }
}