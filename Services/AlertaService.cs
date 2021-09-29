using Distribuidora.DTOs;
using Distribuidora.Helpers;
using System.Collections.Generic;
using System.Data;

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
            var result = dataBaseHelper.ExecQuery(query);

            var alertas = MapearAlertas(result.Rows);

            return alertas;
        }

        private List<Alerta> MapearAlertas(DataRowCollection rows)
        {
            var alertas = new List<Alerta>();

            foreach (DataRow row in rows)
            {
                var alerta = new Alerta
                {
                    Codigo = row["aler_codigo"].ToString(),
                    Detalle = row["aler_detalle"].ToString(),
                    Fecha = row["aler_fecha"].ToString(),
                    Objeto = row["aler_objeto"].ToString(),
                    TipoAlerta = new TipoAlerta
                    {
                        Codigo = row["tale_codigo"].ToString(),
                        Detalle = row["tale_detalle"].ToString()
                    }
                };

                alertas.Add(alerta);
            }

            return alertas;
        }
    }
}