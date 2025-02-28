using Persistencia.DTOs;
using System.Data;

namespace Persistencia.DAOs.Producto
{
    public interface IDAOProducto
    {
        bool ExisteProductoSegunCodigo(string codigoProducto);
        DataTable ObtenerProductos();
        DataTable ObtenerProductosParaExcel();
        DataTable ObtenerProductosSimilares(string detalleProducto);
        DataTable ObtenerProductoPorId(string idProducto);
        DataTable ObtenerProductosPorCodigo(string codigoProducto);
        void EliminarProducto(string codigoProducto);
        int GuardarProducto(string codigo, string detalle, string precioUnitario, string codigoRubro, string stockMinimo);
        void ActualizarProducto(string id, string codigo, string detalle, string precioUnitario, string codigoRubro, string stockMinimo);
        void ActualizarProductoLazy(string id, string codigo, string detalle, string precioUnitario);
        DataTable Buscar(DTOs.Producto producto);
        DataTable BuscarParaEstadistica(Estadistica estadistica);
    }
}