using Persistencia.DAOs.Producto;
using Persistencia.DTOs;
using System.Collections.Generic;
using System.Data;
using System.Linq;

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
            var result = dAOProducto.ObtenerProductos();
            var productos = MapearProductos(result.Rows);
            return productos;
        }

        public DataTable ObtenerProductosParaExcel()
        {
            var productos = dAOProducto.ObtenerProductosParaExcel();
            return productos;
        }

        public List<Persistencia.DTOs.Producto> ObtenerProductosSimilares(string detalleProducto)
        {
            var result = dAOProducto.ObtenerProductosSimilares(detalleProducto);
            var productos = MapearProductos(result.Rows);
            return productos;
        }

        public Persistencia.DTOs.Producto ObtenerProductoPorId(string idProducto)
        {
            var result = dAOProducto.ObtenerProductoPorId(idProducto);
            var productos = MapearProductos(result.Rows);
            return productos[0];
        }

        public List<Persistencia.DTOs.Producto> ObtenerProductosPorCodigo(string codigoProducto)
        {
            var result = dAOProducto.ObtenerProductosPorCodigo(codigoProducto);
            var productos = MapearProductos(result.Rows);
            return productos.Any() ? productos : null;
        }

        public List<Persistencia.DTOs.Producto> Buscar(Persistencia.DTOs.Producto producto)
        {
            var results = dAOProducto.Buscar(producto);
            var productos = MapearProductos(results.Rows);
            return productos;
        }

        public List<Estadistica> BuscarParaEstadistica(Estadistica estadistica)
        {
            var resultados = dAOProducto.BuscarParaEstadistica(estadistica);
            var estadisticas = MapearEstadisticas(resultados.Rows);
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

        private List<Persistencia.DTOs.Producto> MapearProductos(DataRowCollection rows)
        {
            var productos = new List<Persistencia.DTOs.Producto>();

            foreach (DataRow row in rows)
            {
                var producto = new Persistencia.DTOs.Producto
                {
                    Id = row["Id"].ToString(),
                    Codigo = row["Codigo"].ToString(),
                    Detalle = row["Detalle"].ToString(),
                    PrecioUnitario = (decimal)row["Precio"],
                    Rubro = new Persistencia.DTOs.Rubro
                    {
                        Codigo = row["RubroCodigo"].ToString(),
                        Detalle = row["RubroDetalle"].ToString()
                    },
                    Stock = new Persistencia.DTOs.Stock
                    {
                        CantidadActual = row["StockActual"].ToString(),
                        CantidadMinima = row["PtoReposicion"].ToString(),
                        UltimaReposicion = row["UltimaReposicion"].ToString()
                    },
                    UltimaModificacion = row["UltimaModificacion"].ToString(),
                };

                productos.Add(producto);
            }

            return productos;
        }

        private List<Estadistica> MapearEstadisticas(DataRowCollection rows)
        {
            var estadisticas = new List<Estadistica>();

            foreach (DataRow row in rows)
            {
                var estadistica = new Estadistica
                {
                    CodigoProducto = row["Codigo"].ToString(),
                    DetalleProducto = row["Detalle"].ToString(),
                    PrecioUnitarioProducto = (decimal)row["Precio"],
                    StockActualProducto = (int)row["StockActual"],
                    CantidadTotal = (int)row["CantidadTotal"]
                };

                estadisticas.Add(estadistica);
            }

            return estadisticas;
        }
    }
}