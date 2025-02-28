using Persistencia.DAOs.Stock;
using Persistencia.DTOs.Reportes;
using System.Collections.Generic;
using System.Data;

namespace Logica.Services.Stock
{
    public class StockService : IStockService
    {
        private readonly IDAOStock dAOStock;

        public StockService(IDAOStock dAOStock)
        {
            this.dAOStock = dAOStock;
        }

        public int GuardarReposicion()
        {
            var codigoReposicion = dAOStock.GuardarReposicion();
            return codigoReposicion;
        }

        public void ReponerStock(int reposicionCodigo, string idProducto, string cantidadAReponer)
        {
            dAOStock.ReponerStock(reposicionCodigo, idProducto, cantidadAReponer);
        }

        public Reposicion ObtenerReposicion(string codigoReposicion)
        {
            var result = dAOStock.ObtenerReposicion(codigoReposicion);
            var reposicion = MapearReposicion(result.Rows);
            return reposicion;
        }

        public bool HayStock(string codigoProducto, string cantidad)
        {
            var hayStock = dAOStock.HayStock(codigoProducto, cantidad);
            return hayStock;
        }

        public bool HayQueReponer(string idProducto)
        {
            var hayQueReponer = dAOStock.HayQueReponer(idProducto);
            return hayQueReponer;
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