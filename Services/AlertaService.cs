using Distribuidora.DTOs;
using Distribuidora.Helpers;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Distribuidora.Services
{
    public class AlertaService
    {
        private readonly DataBaseHelper dataBaseHelper;

        public AlertaService()
        {
            dataBaseHelper = new DataBaseHelper();
        }

        public void EmitirAlertaDeReposicion(string codigoProducto)
        {
            dataBaseHelper.AgregarParametroEntrada(codigoProducto, "@producto", SqlDbType.Int);
            _ = dataBaseHelper.ExecStoredProcedure("dbo.EmitirAlertaDeReposicion");
        }

        public void QuitarAlertaDeReposicion(string codigoProducto)
        {
            dataBaseHelper.AgregarParametroEntrada(codigoProducto, "@producto", SqlDbType.Int);
            _ = dataBaseHelper.ExecStoredProcedure("dbo.QuitarAlertaDeReposicion");
        }

        public int ObtenerCantidadDeAlertas()
        {
            var query = "select * from alerta";
            var result = dataBaseHelper.ExecQuery(query);

            return result.Rows.Count;
        }

        public List<Alerta> ObtenerAlertas()
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