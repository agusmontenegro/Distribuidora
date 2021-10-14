using Distribuidora.DTOs.Reportes;
using Distribuidora.Helpers;
using System.Collections.Generic;
using System.Data;

namespace Distribuidora.Services
{
    public class StockService
    {
        private readonly DataBaseHelper dataBaseHelper;

        public StockService()
        {
            dataBaseHelper = new DataBaseHelper();
        }

        public int GuardarReposicion()
        {
            dataBaseHelper.AgregarParametroSalida("@codigo", SqlDbType.Int);
            var salidas = dataBaseHelper.ExecStoredProcedure("dbo.GuardarReposicion");

            return int.Parse(salidas[0]);
        }

        public void ReponerStock(int reposicionCodigo, string codigoProducto, string cantidadAReponer)
        {
            dataBaseHelper.AgregarParametroEntrada(reposicionCodigo.ToString(), "@reposicion", SqlDbType.Int);
            dataBaseHelper.AgregarParametroEntrada(codigoProducto, "@codigoProducto", SqlDbType.Int);
            dataBaseHelper.AgregarParametroEntrada(cantidadAReponer, "@cantidadAReponer", SqlDbType.Int);

            _ = dataBaseHelper.ExecStoredProcedure("dbo.ReponerStock");
        }

        public Reposicion ObtenerReposicion(string codigoReposicion)
        {
            string query = "select * from dbo.Reposicion_View where Codigo = " + codigoReposicion;
            var result = dataBaseHelper.ExecQuery(query);

            var reposicion = MapearReposicion(result.Rows);

            return reposicion;
        }

        private Reposicion MapearReposicion(DataRowCollection rows)
        {
            var reposicion = new Reposicion();
            reposicion.Fecha = rows[0]["Fecha"].ToString();
            reposicion.Items = new List<ItemReposicion>();

            foreach (DataRow row in rows)
            {
                var item = new ItemReposicion();

                item.Producto = row["Detalle"].ToString();
                item.CantidadVieja = (int)row["CantidadAnterior"];
                item.CantidadTotal = (int)row["CantidadActual"];

                reposicion.Items.Add(item);
            }

            return reposicion;
        }
    }
}