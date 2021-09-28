using Distribuidora.Helpers;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

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
            string ConnectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
            SqlConnection Connection = new SqlConnection(ConnectionString);
            using (Connection)
            {
                Connection.Open();
                using (var cmd = new SqlCommand(query, Connection))
                {
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        return new DTOs.Producto()
                        {
                            Codigo = reader["prod_codigo"].ToString(),
                            Detalle = reader["prod_detalle"].ToString(),
                            PrecioUnitario = (decimal)reader["prod_precio"],
                            Rubro = new DTOs.Rubro
                            {
                                Codigo = reader["rubr_codigo"].ToString(),
                                Detalle = reader["rubr_detalle"].ToString()
                            },
                            Stock = new DTOs.Stock
                            {
                                CantidadActual = reader["stoc_cantidad_actual"].ToString(),
                                CantidadMinima = reader["stoc_cantidad_minima"].ToString(),
                                UltimaReposicion = reader["stoc_ultima_reposicion"].ToString()
                            }
                        };
                    }
                }
            }

            return null;
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