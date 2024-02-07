using Persistencia.DAOs;
using Persistencia.DTOs;
using System.Collections.Generic;
using System.Data;

namespace Logica.Services
{
    public class ProductoService
    {
        private readonly DAOProducto DAOProducto;

        public ProductoService()
        {
            DAOProducto = new DAOProducto();
        }

        public bool ExisteProductoSegunCodigo(string codigoProducto)
        {
            var existsProduct = DAOProducto.ExisteProductoSegunCodigo(codigoProducto);
            return existsProduct;
        }

        public List<Producto> ObtenerProductos()
        {
            var productos = DAOProducto.ObtenerProductos();
            return productos;
        }

        public DataTable ObtenerProductosParaExcel()
        {
            var productos = DAOProducto.ObtenerProductosParaExcel();
            return productos;
        }

        public List<Producto> ObtenerProductosSimilares(string detalleProducto)
        {
            var productos = DAOProducto.ObtenerProductosSimilares(detalleProducto);
            return productos;
        }

        public Producto ObtenerProductoPorId(string idProducto)
        {
            var producto = DAOProducto.ObtenerProductoPorId(idProducto);
            return producto;
        }

        public List<Producto> ObtenerProductosPorCodigo(string codigoProducto)
        {
            var productos = DAOProducto.ObtenerProductosPorCodigo(codigoProducto);
            return productos;
        }

        public List<Producto> Buscar(Producto producto)
        {
            var productos = DAOProducto.Buscar(producto);
            return productos;
        }

        public List<Estadistica> BuscarParaEstadistica(Estadistica estadistica)
        {
            var estadisticas = DAOProducto.BuscarParaEstadistica(estadistica);
            return estadisticas;
        }

        public void EliminarProducto(string codigoProducto)
        {
            DAOProducto.EliminarProducto(codigoProducto);
        }

        public int GuardarProducto(string codigo, string detalle, string precioUnitario, string codigoRubro, string stockMinimo)
        {
            var producto = DAOProducto.GuardarProducto(codigo, detalle, precioUnitario, codigoRubro, stockMinimo);
            return producto;
        }

        public void ActualizarProducto(string id, string codigo, string detalle, string precioUnitario, string codigoRubro, string stockMinimo)
        {
            DAOProducto.ActualizarProducto(id, codigo, detalle, precioUnitario, codigoRubro, stockMinimo);
        }

        public void ActualizarProductoLazy(string id, string codigo, string detalle, string precioUnitario)
        {
            DAOProducto.ActualizarProductoLazy(id, codigo, detalle, precioUnitario);
        }
    }
}