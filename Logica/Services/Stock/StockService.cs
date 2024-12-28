using Persistencia.DAOs.Stock;
using Persistencia.DTOs.Reportes;

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
            var reposicion = dAOStock.ObtenerReposicion(codigoReposicion);
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
    }
}