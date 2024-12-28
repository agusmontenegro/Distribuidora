using Persistencia.DAOs.Producto;
using Persistencia.DTOs;
using System.Collections.Generic;
using System.Data;

namespace Logica.Services.Producto
{
    public class ProductoService : IProductoService
    {
        private readonly IDAOProducto dAOProducto;

        public ProductoService(IDAOProducto dAOProducto)
        {
            this.dAOProducto = dAOProducto;
        }

        public bool ExisteProductoSegunCodigo(string codigoProducto)
        {
            var existsProduct = dAOProducto.ExisteProductoSegunCodigo(codigoProducto);
            return existsProduct;
        }

        public List<Persistencia.DTOs.Producto> ObtenerProductos()
        {
            var productos = dAOProducto.ObtenerProductos();
            return productos;
        }

        public DataTable ObtenerProductosParaExcel()
        {
            var productos = dAOProducto.ObtenerProductosParaExcel();
            return productos;
        }

        public List<Persistencia.DTOs.Producto> ObtenerProductosSimilares(string detalleProducto)
        {
            var productos = dAOProducto.ObtenerProductosSimilares(detalleProducto);
            return productos;
        }

        public Persistencia.DTOs.Producto ObtenerProductoPorId(string idProducto)
        {
            var producto = dAOProducto.ObtenerProductoPorId(idProducto);
            return producto;
        }

        public List<Persistencia.DTOs.Producto> ObtenerProductosPorCodigo(string codigoProducto)
        {
            var productos = dAOProducto.ObtenerProductosPorCodigo(codigoProducto);
            return productos;
        }

        public List<Persistencia.DTOs.Producto> Buscar(Persistencia.DTOs.Producto producto)
        {
            var productos = dAOProducto.Buscar(producto);
            return productos;
        }

        public List<Estadistica> BuscarParaEstadistica(Estadistica estadistica)
        {
            var estadisticas = dAOProducto.BuscarParaEstadistica(estadistica);
            return estadisticas;
        }

        public void EliminarProducto(string codigoProducto)
        {
            dAOProducto.EliminarProducto(codigoProducto);
        }

        public int GuardarProducto(string codigo, string detalle, string precioUnitario, string codigoRubro, string stockMinimo)
        {
            var producto = dAOProducto.GuardarProducto(codigo, detalle, precioUnitario, codigoRubro, stockMinimo);
            return producto;
        }

        public void ActualizarProducto(string id, string codigo, string detalle, string precioUnitario, string codigoRubro, string stockMinimo)
        {
            dAOProducto.ActualizarProducto(id, codigo, detalle, precioUnitario, codigoRubro, stockMinimo);
        }

        public void ActualizarProductoLazy(string id, string codigo, string detalle, string precioUnitario)
        {
            dAOProducto.ActualizarProductoLazy(id, codigo, detalle, precioUnitario);
        }
    }
}