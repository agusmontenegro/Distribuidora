using Distribuidora.Helpers;
using System;
using System.Collections.Generic;
using System.Data;

namespace Distribuidora.Services
{
    public class ProductoService
    {
        private readonly DataBaseHelper dataBaseHelper;
        private readonly ValidacionService validacionService;

        public ProductoService()
        {
            dataBaseHelper = new DataBaseHelper();
            validacionService = new ValidacionService();
        }

        public bool ExisteProducto(string codigoProducto)
        {
            var query = "select Codigo from dbo.Producto_View where Codigo = " + codigoProducto;
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

        public DTOs.Producto ObtenerProducto(string codigoProducto)
        {
            string query = "select * from dbo.Producto_View where Codigo = " + codigoProducto;
            var result = dataBaseHelper.ExecQuery(query);

            var productos = MapearProductos(result.Rows);

            return productos[0];
        }

        private List<DTOs.Producto> MapearProductos(DataRowCollection rows)
        {
            var productos = new List<DTOs.Producto>();

            foreach (DataRow row in rows)
            {
                var producto = new DTOs.Producto();

                producto.Codigo = row["Codigo"].ToString();
                producto.Detalle = row["Detalle"].ToString();
                producto.PrecioUnitario = (decimal)row["Precio"];
                producto.Rubro = new DTOs.Rubro
                {
                    Codigo = row["RubroCodigo"].ToString(),
                    Detalle = row["RubroDetalle"].ToString()
                };
                producto.Stock = new DTOs.Stock
                {
                    CantidadActual = row["StockActual"].ToString(),
                    CantidadMinima = row["PtoReposicion"].ToString(),
                    UltimaReposicion = row["UltimaReposicion"].ToString()
                };

                productos.Add(producto);
            }

            return productos;
        }

        public void EliminarProducto(string codigoProducto)
        {
            var query = "update dbo.Producto set prod_activo = 0 where prod_codigo = " + codigoProducto;
            dataBaseHelper.ExecScript(query);
        }

        public int GuardarProducto(string detalleProducto, string precioUnitario, string codigoRubro, string stockMinimo)
        {
            dataBaseHelper.AgregarParametroEntrada(detalleProducto, "@detalle", SqlDbType.NVarChar);
            dataBaseHelper.AgregarParametroEntrada(precioUnitario, "@precioUnitario", SqlDbType.Decimal);
            dataBaseHelper.AgregarParametroEntrada(codigoRubro, "@rubro", SqlDbType.Int);
            dataBaseHelper.AgregarParametroEntrada(stockMinimo, "@stockMinimo", SqlDbType.Int);
            dataBaseHelper.AgregarParametroSalida("@codigoProducto", SqlDbType.Int);

            var salidas = dataBaseHelper.ExecStoredProcedure("dbo.InsertarProducto");

            return int.Parse(salidas[0]);
        }

        public void ActualizarProducto(string codigoProductoEditar, string detalleProducto, string precioUnitario, string codigoRubro, string stockMinimo)
        {
            dataBaseHelper.AgregarParametroEntrada(codigoProductoEditar, "@codigo", SqlDbType.Int);
            dataBaseHelper.AgregarParametroEntrada(detalleProducto, "@detalle", SqlDbType.NVarChar);
            dataBaseHelper.AgregarParametroEntrada(precioUnitario, "@precioUnitario", SqlDbType.Decimal);
            dataBaseHelper.AgregarParametroEntrada(codigoRubro, "@rubro", SqlDbType.Int);
            dataBaseHelper.AgregarParametroEntrada(stockMinimo, "@stockMinimo", SqlDbType.Int);

            _ = dataBaseHelper.ExecStoredProcedure("dbo.ActualizarProducto");
        }

        public bool HayStock(string codigoProducto, string cantidad)
        {
            var query = "SELECT dbo.HayStockDisponible(" + codigoProducto + "," + cantidad + ");";
            var result = dataBaseHelper.ExecFunction(query);

            return Convert.ToBoolean(result.ToString());
        }

        public bool HayQueReponer(string codigoProducto)
        {
            var query = "SELECT dbo.LlegoAPuntoDeReposicion(" + codigoProducto + ");";
            var result = dataBaseHelper.ExecFunction(query);

            return Convert.ToBoolean(result.ToString());
        }
    }
}