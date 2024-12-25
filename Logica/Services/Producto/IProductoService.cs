using Persistencia.DTOs;
using System.Collections.Generic;
using System.Data;

namespace Logica.Services.Producto
{
    public interface IProductoService
    {
        bool ExisteProductoSegunCodigo(string codigoProducto);
        List<Persistencia.DTOs.Producto> ObtenerProductos();
        DataTable ObtenerProductosParaExcel();
        List<Persistencia.DTOs.Producto> ObtenerProductosSimilares(string detalleProducto);
        Persistencia.DTOs.Producto ObtenerProductoPorId(string idProducto);
        List<Persistencia.DTOs.Producto> ObtenerProductosPorCodigo(string codigoProducto);
        List<Persistencia.DTOs.Producto> Buscar(Persistencia.DTOs.Producto producto);
        List<Estadistica> BuscarParaEstadistica(Estadistica estadistica);
        void EliminarProducto(string codigoProducto);
        int GuardarProducto(string codigo, string detalle, string precioUnitario, string codigoRubro, string stockMinimo);
        void ActualizarProducto(string id, string codigo, string detalle, string precioUnitario, string codigoRubro, string stockMinimo);
        void ActualizarProductoLazy(string id, string codigo, string detalle, string precioUnitario);
    }
}