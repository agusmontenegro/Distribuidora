using Distribuidora.DTOs;
using Distribuidora.Helpers;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Distribuidora.Services
{
    public static class AlertaService
    {
        public static void EmitirAlertaDeReposicion(string codigo_producto)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            SqlParameter codigoProductoParameter = new SqlParameter("@producto", SqlDbType.Int);
            codigoProductoParameter.Value = int.Parse(codigo_producto);

            parameters.Add(codigoProductoParameter);

            DataBaseHelper.ExecStoredProcedure("dbo.EmitirAlertaDeReposicion", parameters);
        }

        public static void QuitarAlertaDeReposicion(string codigo_producto)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            SqlParameter codigoProductoParameter = new SqlParameter("@producto", SqlDbType.Int);
            codigoProductoParameter.Value = int.Parse(codigo_producto);

            parameters.Add(codigoProductoParameter);

            DataBaseHelper.ExecStoredProcedure("dbo.QuitarAlertaDeReposicion", parameters);
        }

        public static int ObtenerCantidadDeAlertas()
        {
            var query = "select * from alerta";
            var result = DataBaseHelper.ExecQuery(query);

            return result.Rows.Count;
        }

        public static List<Alerta> ObtenerAlertas()
        {
            string query = "select * from dbo.Alerta a join dbo.Tipo_Alerta ta on a.aler_tipo = ta.tale_codigo";
            string ConnectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
            SqlConnection Connection = new SqlConnection(ConnectionString);
            using (Connection)
            {
                Connection.Open();
                using (var cmd = new SqlCommand(query, Connection))
                {
                    var reader = cmd.ExecuteReader();
                    var alertas = new List<Alerta>();

                    while (reader.Read())
                    {                        
                        var alerta = new Alerta
                        {
                            Codigo = reader["aler_codigo"].ToString(),
                            Detalle = reader["aler_detalle"].ToString(),
                            Fecha = reader["aler_fecha"].ToString(),
                            Objeto = reader["aler_objeto"].ToString(),
                            TipoAlerta = new TipoAlerta
                            {
                                Codigo = reader["tale_codigo"].ToString(),
                                Detalle = reader["tale_detalle"].ToString()
                            }                            
                        };

                        alertas.Add(alerta);
                    }

                    return alertas;
                }
            }
        }
    }
}