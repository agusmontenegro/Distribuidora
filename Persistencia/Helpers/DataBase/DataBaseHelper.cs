using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Persistencia.Helpers.DataBase
{
    public class DataBaseHelper : IDataBaseHelper
    {
        public List<SqlParameter> Parametros { get; set; }
        public string ConnectionString { get; set; }

        public DataBaseHelper(string ConnectionString)
        {
            Parametros = new List<SqlParameter>();
            this.ConnectionString = ConnectionString;
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

        public object ExecFunction(string query, List<SqlParameter> parameters)
        {
            using (var connection = AbrirConexion())
            {
                using (var sqlCommand = connection.CreateCommand())
                {
                    sqlCommand.CommandText = query;
                    if (parameters != null)
                        sqlCommand.Parameters.AddRange(parameters.ToArray());
                    return sqlCommand.ExecuteScalar();
                }
            }
        }

        public DataTable ExecQuery(string query, List<SqlParameter> parameters)
        {
            using (var connection = AbrirConexion())
            using (var sqlCommand = connection.CreateCommand())
            using (var sqlAdapter = new SqlDataAdapter(sqlCommand))
            {
                try
                {
                    var dataSet = new DataSet();

                    sqlCommand.CommandText = query;
                    sqlCommand.CommandType = CommandType.Text;

                    if (parameters != null)
                        sqlCommand.Parameters.AddRange(parameters.ToArray());

                    sqlAdapter.Fill(dataSet);
                    return dataSet.Tables[0];
                }
                catch (SqlException ex)
                {
                    throw new Exception("Hubo un problema al ejecutar la consulta.", ex);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("Error desconocido", ex);
                }
            }
        }

        public void ExecNonQuery(string query, List<SqlParameter> parameters)
        {
            using (var connection = AbrirConexion())
            using (var sqlCommand = connection.CreateCommand())
            {
                try
                {
                    sqlCommand.CommandText = query;

                    if (parameters != null)
                        sqlCommand.Parameters.AddRange(parameters.ToArray());

                    sqlCommand.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    throw new Exception("Hubo un problema al ejecutar la consulta.", ex);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("Error desconocido", ex);
                }
            }
        }

        private SqlConnection AbrirConexion()
        {
            var connection = new SqlConnection(ConnectionString);
            connection.Open();
            return connection;
        }
    }
}