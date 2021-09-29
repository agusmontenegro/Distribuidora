using Distribuidora.Helpers;
using System;
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
            var query = "select prod_codigo from dbo.Producto_View where prod_codigo = " + codigoProducto;
            var result = dataBaseHelper.ExecQuery(query);

            return result.Rows.Count == 1;
        }

        public DTOs.Producto ObtenerProducto(string codigoProducto)
        {
            string query = "select * from dbo.Producto_View where prod_codigo = " + codigoProducto;
            var result = dataBaseHelper.ExecQuery(query);

            var producto = MapearProducto(result.Rows);

            return producto;
        }

        private DTOs.Producto MapearProducto(DataRowCollection rows)
        {
            var producto = new DTOs.Producto();

            foreach (DataRow row in rows)
            {
                producto.Codigo = row["prod_codigo"].ToString();
                producto.Detalle = row["prod_detalle"].ToString();
                producto.PrecioUnitario = (decimal)row["prod_precio"];
                producto.Rubro = new DTOs.Rubro
                {
                    Codigo = row["rubr_codigo"].ToString(),
                    Detalle = row["rubr_detalle"].ToString()
                };
                producto.Stock = new DTOs.Stock
                {
                    CantidadActual = row["stoc_cantidad_actual"].ToString(),
                    CantidadMinima = row["stoc_cantidad_minima"].ToString(),
                    UltimaReposicion = row["stoc_ultima_reposicion"].ToString()
                };
            }

            return producto;
        }

        public bool CodigoProductoValido(string codigoProducto, ref string msj)
        {
            if (string.IsNullOrEmpty(codigoProducto))
            {
                validacionService.AgregarValidacion(
                    false,
                    "Debe ingresar un código de producto.");
            }
            else
            {
                validacionService.AgregarValidacion(
                    ExisteProducto(codigoProducto),
                    "No existe un producto activo con el código ingresado.");
            }

            return validacionService.Validar(ref msj);
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