using Persistencia.DTOs;
using Persistencia.Helpers;
using System.Collections.Generic;
using System.Data;

namespace Persistencia.DAOs
{
    public class DAOAlerta
    {
        private readonly DataBaseHelper DataBaseHelper;

        public DAOAlerta()
        {
            DataBaseHelper = new DataBaseHelper();
        }

        public void EmitirAlertaDeReposicion(string idProducto)
        {
            DataBaseHelper.AgregarParametroEntrada(idProducto, "@producto", SqlDbType.Int);
            _ = DataBaseHelper.ExecStoredProcedure("dbo.EmitirAlertaDeReposicion");
        }

        public void QuitarAlertaDeReposicion(string idProducto)
        {
            DataBaseHelper.AgregarParametroEntrada(idProducto, "@producto", SqlDbType.Int);
            _ = DataBaseHelper.ExecStoredProcedure("dbo.QuitarAlertaDeReposicion");
        }

        public int ObtenerCantidadDeAlertas()
        {
            var query = "select * from alerta";
            var result = DataBaseHelper.ExecQuery(query);

            return result.Rows.Count;
        }

        public List<Alerta> ObtenerAlertas()
        {
            string query = "select * from dbo.Alerta a join dbo.Tipo_Alerta ta on a.aler_tipo = ta.tale_codigo";
            var result = DataBaseHelper.ExecQuery(query);

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
