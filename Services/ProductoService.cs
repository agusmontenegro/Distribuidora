using Distribuidora.Helpers;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Distribuidora.Services
{
    public class ProductoService
    {
        private readonly DataBaseHelper dataBaseHelper;

        public ProductoService()
        {
            dataBaseHelper = new DataBaseHelper();
        }

        public bool ExisteProductoSegunCodigo(string codigoProducto)
        {
            var query = "select Codigo from dbo.Producto_View where Codigo = '" + codigoProducto + "'";
            var result = dataBaseHelper.ExecQuery(query);
            return result.Rows.Count == 1;
        }

        public List<DTOs.Producto> ObtenerProductos()
        {
            string query = "select * from dbo.Producto_View";
            var result = dataBaseHelper.ExecQuery(query);
            var productos = MapearProductos(result.Rows);
            return productos;
        }

        public List<DTOs.Producto> ObtenerProductosSimilares(string detalleProducto)
        {
            var likeQuery = string.Empty;
            var palabras = detalleProducto.Split(' ').Where(pal => pal.Count() > 3).ToList();
            palabras = palabras.Select(pal => pal.Insert(0, "'%")).ToList();
            palabras = palabras.Select(pal => pal.Insert(pal.Length, "%'")).ToList();
            for (int i = 0;i < palabras.Count;i++)
            {
                likeQuery += palabras[i];
                if (i + 1 < palabras.Count)
                {
                    likeQuery += " or Detalle like ";
                }
            }

            string query = "select * from dbo.Producto_View where Detalle like " + likeQuery;
            var result = dataBaseHelper.ExecQuery(query);
            var productos = MapearProductos(result.Rows);
            return productos;
        }

        public DTOs.Producto ObtenerProductoPorId(string idProducto)
        {
            string query = "select * from dbo.Producto_View where Id = " + idProducto;
            var result = dataBaseHelper.ExecQuery(query);
            var productos = MapearProductos(result.Rows);
            return productos[0];
        }

        public List<DTOs.Producto> ObtenerProductosPorCodigo(string codigoProducto)
        {
            string query = "select * from dbo.Producto_View where Codigo = '" + codigoProducto + "'";
            var result = dataBaseHelper.ExecQuery(query);
            var productos = MapearProductos(result.Rows);
            return productos.Any() ? productos : null;
        }

        private List<DTOs.Producto> MapearProductos(DataRowCollection rows)
        {
            var productos = new List<DTOs.Producto>();

            foreach (DataRow row in rows)
            {
                var producto = new DTOs.Producto
                {
                    Id = row["Id"].ToString(),
                    Codigo = row["Codigo"].ToString(),
                    Detalle = row["Detalle"].ToString(),
                    PrecioUnitario = (decimal)row["Precio"],
                    Rubro = new DTOs.Rubro
                    {
                        Codigo = row["RubroCodigo"].ToString(),
                        Detalle = row["RubroDetalle"].ToString()
                    },
                    Stock = new DTOs.Stock
                    {
                        CantidadActual = row["StockActual"].ToString(),
                        CantidadMinima = row["PtoReposicion"].ToString(),
                        UltimaReposicion = row["UltimaReposicion"].ToString()
                    }
                };

                productos.Add(producto);
            }

            return productos;
        }

        public void EliminarProducto(string codigoProducto)
        {
            var query = "update dbo.Producto set prod_activo = 0 where prod_id = " + codigoProducto;
            dataBaseHelper.ExecScript(query);
        }

        public int GuardarProducto(string codigo, string detalle, string precioUnitario, string codigoRubro, string stockMinimo)
        {
            dataBaseHelper.AgregarParametroEntrada(codigo, "@codigo", SqlDbType.NVarChar);
            dataBaseHelper.AgregarParametroEntrada(detalle, "@detalle", SqlDbType.NVarChar);
            dataBaseHelper.AgregarParametroEntrada(precioUnitario, "@precioUnitario", SqlDbType.Decimal);
            dataBaseHelper.AgregarParametroEntrada(codigoRubro, "@rubro", SqlDbType.Int);
            dataBaseHelper.AgregarParametroEntrada(stockMinimo, "@stockMinimo", SqlDbType.Int);
            dataBaseHelper.AgregarParametroSalida("@id", SqlDbType.Int);

            var salidas = dataBaseHelper.ExecStoredProcedure("dbo.InsertarProducto");
            return int.Parse(salidas[0]);
        }

        public void ActualizarProducto(string id, string codigo, string detalle, string precioUnitario, string codigoRubro, string stockMinimo)
        {
            dataBaseHelper.AgregarParametroEntrada(id, "@id", SqlDbType.Int);
            dataBaseHelper.AgregarParametroEntrada(codigo, "@codigo", SqlDbType.NVarChar);
            dataBaseHelper.AgregarParametroEntrada(detalle, "@detalle", SqlDbType.NVarChar);
            dataBaseHelper.AgregarParametroEntrada(precioUnitario, "@precioUnitario", SqlDbType.Decimal);
            dataBaseHelper.AgregarParametroEntrada(codigoRubro, "@rubro", SqlDbType.Int);
            dataBaseHelper.AgregarParametroEntrada(stockMinimo, "@stockMinimo", SqlDbType.Int);

            _ = dataBaseHelper.ExecStoredProcedure("dbo.ActualizarProducto");
        }
    }
}