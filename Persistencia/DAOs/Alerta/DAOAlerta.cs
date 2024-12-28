using Persistencia.DTOs;
using Persistencia.Helpers.DataBase;
using System.Collections.Generic;
using System.Data;

namespace Persistencia.DAOs.Alerta
{
    public class DAOAlerta : IDAOAlerta
    {
        private readonly IDataBaseHelper dataBaseHelper;

        public DAOAlerta(IDataBaseHelper dataBaseHelper)
        {
            this.dataBaseHelper = dataBaseHelper;
        }

        public void EmitirAlertaDeReposicion(string idProducto)
        {
            dataBaseHelper.AgregarParametroEntrada(idProducto, "@producto", SqlDbType.Int);
            _ = dataBaseHelper.ExecStoredProcedure("dbo.EmitirAlertaDeReposicion");
        }

        public void QuitarAlertaDeReposicion(string idProducto)
        {
            dataBaseHelper.AgregarParametroEntrada(idProducto, "@producto", SqlDbType.Int);
            _ = dataBaseHelper.ExecStoredProcedure("dbo.QuitarAlertaDeReposicion");
        }

        public int ObtenerCantidadDeAlertas()
        {
            var query = "select * from alerta";
            var result = dataBaseHelper.ExecQuery(query);

            return result.Rows.Count;
        }

        public List<DTOs.Alerta> ObtenerAlertas()
        {
            string query = "select * from dbo.Alerta a join dbo.Tipo_Alerta ta on a.aler_tipo = ta.tale_codigo";
            var result = dataBaseHelper.ExecQuery(query);

            var alertas = MapearAlertas(result.Rows);

            return alertas;
        }

        private List<DTOs.Alerta> MapearAlertas(DataRowCollection rows)
        {
            var alertas = new List<DTOs.Alerta>();

            foreach (DataRow row in rows)
            {
                var alerta = new DTOs.Alerta
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
