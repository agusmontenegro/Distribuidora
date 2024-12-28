using Persistencia.DTOs.Reportes;

namespace Persistencia.DAOs.Stock
{
    public interface IDAOStock
    {
        int GuardarReposicion();
        void ReponerStock(int reposicionCodigo, string idProducto, string cantidadAReponer);
        Reposicion ObtenerReposicion(string codigoReposicion);
        bool HayStock(string codigoProducto, string cantidad);
        bool HayQueReponer(string idProducto);
    }
}
