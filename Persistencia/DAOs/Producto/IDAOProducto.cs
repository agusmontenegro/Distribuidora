using Persistencia.DTOs;
using System.Collections.Generic;
using System.Data;

namespace Persistencia.DAOs.Producto
{
    public interface IDAOProducto
    {
        bool ExisteProductoSegunCodigo(string codigoProducto);
        List<DTOs.Producto> ObtenerProductos();
        DataTable ObtenerProductosParaExcel();
        List<DTOs.Producto> ObtenerProductosSimilares(string detalleProducto);
        DTOs.Producto ObtenerProductoPorId(string idProducto);
        List<DTOs.Producto> ObtenerProductosPorCodigo(string codigoProducto);
        void EliminarProducto(string codigoProducto);
        int GuardarProducto(string codigo, string detalle, string precioUnitario, string codigoRubro, string stockMinimo);
        void ActualizarProducto(string id, string codigo, string detalle, string precioUnitario, string codigoRubro, string stockMinimo);
        void ActualizarProductoLazy(string id, string codigo, string detalle, string precioUnitario);
        List<DTOs.Producto> Buscar(DTOs.Producto producto);
        List<Estadistica> BuscarParaEstadistica(Estadistica estadistica);
    }
}