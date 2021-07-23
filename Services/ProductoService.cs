using Distribuidora.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Distribuidora.Services
{
    public static class ProductoService
    {
        public static bool ExisteProducto(string codigoProducto)
        {
            var query = "select prod_codigo from dbo.Producto_View where prod_codigo = " + codigoProducto;
            var result = DataBaseHelper.ExecQuery(query);

            return result.Rows.Count == 1;
        }

        public static DTOs.Producto ObtenerProducto(string codigoProducto)
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

        public static bool CodigoProductoValido(string codigoProducto, ref string msj)
        {
            ValidationService v = new ValidationService();

            v.Validations.Add(new ValidationService.Validation
            {
                condition = ExisteProducto(codigoProducto),
                msj = "No existe un producto activo con el código ingresado."
            });

            return v.validate(ref msj);
        }

        public static void EliminarProducto(string codigoProducto)
        {
            var query = "update dbo.Producto set prod_activo = 0 where prod_codigo = " + codigoProducto;
            DataBaseHelper.ExecScript(query);
        }

        public static int GuardarProducto(string detalleProducto, string precioUnitario, string codigoRubro, string stockMinimo)
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

            DataBaseHelper.ExecStoredProcedure("dbo.InsertarProducto", parameters);

            return Convert.ToInt32(parameters[4].Value.ToString());
        }

        public static void ActualizarProducto(string codigoProductoEditar, string detalleProducto, string precioUnitario, string codigoRubro, string stockMinimo)
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

            DataBaseHelper.ExecStoredProcedure("dbo.ActualizarProducto", parameters);
        }

        public static bool HayStock(string codigoProducto, string cantidad)
        {
            var query = "SELECT dbo.HayStockDisponible(" + codigoProducto + "," + cantidad + ");";
            var result = DataBaseHelper.ExecFunction(query);

            return Convert.ToBoolean(result.ToString());
        }

        public static bool HayQueReponer(string codigoProducto)
        {
            var query = "SELECT dbo.LlegoAPuntoDeReposicion(" + codigoProducto + ");";
            var result = DataBaseHelper.ExecFunction(query);

            return Convert.ToBoolean(result.ToString());
        }        
    }
}