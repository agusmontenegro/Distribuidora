using Distribuidora.Factories;
using Distribuidora.Helpers;
using System;
using System.Collections.Generic;
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
            dataBaseHelper = DataBaseHelperFactory.Crear();
            validacionService = ValidacionServiceFactory.Crear();
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
            List<SqlParameter> parameters = new List<SqlParameter>();

            SqlParameter detalleProductoParameter = new SqlParameter("@detalle", SqlDbType.NVarChar);
            detalleProductoParameter.Value = detalleProducto;

            SqlParameter precioUnitarioParameter = new SqlParameter("@precioUnitario", SqlDbType.Decimal);
            precioUnitarioParameter.Value = decimal.Parse(precioUnitario);

            SqlParameter codigoRubroParameter = new SqlParameter("@rubro", SqlDbType.Int);
            codigoRubroParameter.Value = int.Parse(codigoRubro);

            SqlParameter stockMinimoParameter = new SqlParameter("@stockMinimo", SqlDbType.Int);
            stockMinimoParameter.Value = int.Parse(stockMinimo);

            SqlParameter codigoProductoOuput = new SqlParameter("@codigoProducto", SqlDbType.Int);
            codigoProductoOuput.Direction = ParameterDirection.Output;

            parameters.Add(detalleProductoParameter);
            parameters.Add(precioUnitarioParameter);
            parameters.Add(codigoRubroParameter);
            parameters.Add(stockMinimoParameter);
            parameters.Add(codigoProductoOuput);

            dataBaseHelper.ExecStoredProcedure("dbo.InsertarProducto", parameters);

            return Convert.ToInt32(parameters[4].Value.ToString());
        }

        public void ActualizarProducto(string codigoProductoEditar, string detalleProducto, string precioUnitario, string codigoRubro, string stockMinimo)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            SqlParameter codigoProductoEditarParameter = new SqlParameter("@codigo", SqlDbType.Int);
            codigoProductoEditarParameter.Value = int.Parse(codigoProductoEditar);

            SqlParameter detalleProductoParameter = new SqlParameter("@detalle", SqlDbType.NVarChar);
            detalleProductoParameter.Value = detalleProducto;

            SqlParameter precioUnitarioParameter = new SqlParameter("@precioUnitario", SqlDbType.Decimal);
            precioUnitarioParameter.Value = decimal.Parse(precioUnitario);

            SqlParameter codigoRubroParameter = new SqlParameter("@rubro", SqlDbType.Int);
            codigoRubroParameter.Value = int.Parse(codigoRubro);

            SqlParameter stockMinimoParameter = new SqlParameter("@stockMinimo", SqlDbType.Int);
            stockMinimoParameter.Value = int.Parse(stockMinimo);

            parameters.Add(codigoProductoEditarParameter);
            parameters.Add(detalleProductoParameter);
            parameters.Add(precioUnitarioParameter);
            parameters.Add(codigoRubroParameter);
            parameters.Add(stockMinimoParameter);

            dataBaseHelper.ExecStoredProcedure("dbo.ActualizarProducto", parameters);
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