using Persistencia.DAOs;
using Persistencia.DTOs.Reportes;

namespace Logica.Services
{
    public class StockService
    {
        private readonly DAOStock DAOStock;

        public StockService()
        {
            DAOStock = new DAOStock();
        }

        public int GuardarReposicion()
        {
            var codigoReposicion = DAOStock.GuardarReposicion();
            return codigoReposicion;
        }

        public void ReponerStock(int reposicionCodigo, string idProducto, string cantidadAReponer)
        {
            DAOStock.ReponerStock(reposicionCodigo, idProducto, cantidadAReponer);
        }

        public Reposicion ObtenerReposicion(string codigoReposicion)
        {
            var reposicion = DAOStock.ObtenerReposicion(codigoReposicion);
            return reposicion;
        }

        public bool HayStock(string codigoProducto, string cantidad)
        {
            var hayStock = DAOStock.HayStock(codigoProducto, cantidad);
            return hayStock;
        }

        public bool HayQueReponer(string idProducto)
        {
            var hayQueReponer = DAOStock.HayQueReponer(idProducto);
            return hayQueReponer;
        }
    }
}