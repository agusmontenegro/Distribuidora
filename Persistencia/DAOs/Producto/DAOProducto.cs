using Persistencia.DTOs;
using Persistencia.Helpers.DataBase;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Persistencia.DAOs.Producto
{
    public class DAOProducto : IDAOProducto
    {
        private readonly IDataBaseHelper dataBaseHelper;

        public DAOProducto(IDataBaseHelper dataBaseHelper)
        {
            this.dataBaseHelper = dataBaseHelper;
        }

        public bool ExisteProductoSegunCodigo(string codigoProducto)
        {
            var query = "select Codigo from dbo.Producto_View where Codigo = @Codigo";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Codigo", codigoProducto)
            };
            var result = dataBaseHelper.ExecQuery(query, parameters);
            return result.Rows.Count == 1;
        }

        public DataTable ObtenerProductos()
        {
            string query = "select * from dbo.Producto_View";
            var result = dataBaseHelper.ExecQuery(query, null);
            return result;
        }

        public DataTable ObtenerProductosParaExcel()
        {
            string query = "select Codigo, Detalle, Precio from dbo.Producto_View";
            var result = dataBaseHelper.ExecQuery(query, null);
            return result;
        }

        public DataTable ObtenerProductosSimilares(string detalleProducto)
        {
            var palabras = detalleProducto.Split(' ')
                                          .Where(pal => pal.Count() > 3)
                                          .Select(pal => "%" + pal + "%")
                                          .ToList();

            var query = "SELECT * FROM dbo.Producto_View WHERE ";
            var whereClauses = new List<string>();
            var parameters = new List<SqlParameter>();

            for (int i = 0;i < palabras.Count;i++)
            {
                var paramName = "@param" + i;
                whereClauses.Add($"Detalle LIKE {paramName}");
                parameters.Add(new SqlParameter(paramName, palabras[i]));
            }

            query += string.Join(" OR ", whereClauses);

            var result = dataBaseHelper.ExecQuery(query, parameters);
            return result;
        }


        public DataTable ObtenerProductoPorId(string idProducto)
        {
            string query = "select * from dbo.Producto_View where Id = @IdProducto";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@IdProducto", idProducto)
            };
            var result = dataBaseHelper.ExecQuery(query, parameters);
            return result;
        }

        public DataTable ObtenerProductosPorCodigo(string codigoProducto)
        {
            string query = "select * from dbo.Producto_View where Codigo = '@CodigoProducto'";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@CodigoProducto", codigoProducto)
            };
            var result = dataBaseHelper.ExecQuery(query, parameters);
            return result;
        }        

        public void EliminarProducto(string codigoProducto)
        {
            var query = "UPDATE dbo.Producto SET prod_activo = 0 WHERE prod_id = @codigoProducto";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@codigoProducto", codigoProducto)
            };
            dataBaseHelper.ExecNonQuery(query, parameters);
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

        public void ActualizarProductoLazy(string id, string codigo, string detalle, string precioUnitario)
        {
            dataBaseHelper.AgregarParametroEntrada(id, "@id", SqlDbType.Int);
            dataBaseHelper.AgregarParametroEntrada(codigo, "@codigo", SqlDbType.NVarChar);
            dataBaseHelper.AgregarParametroEntrada(detalle, "@detalle", SqlDbType.NVarChar);
            dataBaseHelper.AgregarParametroEntrada(precioUnitario, "@precioUnitario", SqlDbType.Decimal);

            _ = dataBaseHelper.ExecStoredProcedure("dbo.ActualizarProductoLazy");
        }

        public DataTable Buscar(DTOs.Producto producto)
        {
            string query = "select * from dbo.Producto_View where 1=1";  // Base query for where conditions

            if (!string.IsNullOrEmpty(producto.Codigo))
                query += " and Codigo like @codigo";

            if (!string.IsNullOrEmpty(producto.Detalle))
                query += " and Detalle like @detalle";

            if (producto.Rubro != null)
                query += " and RubroCodigo = @rubroCodigo";

            var parameters = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(producto.Codigo))
                parameters.Add(new SqlParameter("@codigo", SqlDbType.NVarChar) { Value = "%" + producto.Codigo.Trim() + "%" });

            if (!string.IsNullOrEmpty(producto.Detalle))
                parameters.Add(new SqlParameter("@detalle", SqlDbType.NVarChar) { Value = "%" + producto.Detalle.Trim() + "%" });

            if (producto.Rubro != null)
                parameters.Add(new SqlParameter("@rubroCodigo", SqlDbType.Int) { Value = producto.Rubro.Codigo });

            var results = dataBaseHelper.ExecQuery(query, parameters);
            return results;
        }

        public DataTable BuscarParaEstadistica(Estadistica estadistica)
        {
            string query = "select top 100 p.prod_codigo Codigo, " +
                           "p.prod_detalle Detalle, " +
                           "p.prod_precio Precio, " +
                           "s.stoc_cantidad_actual StockActual, " +
                           "sum(i.item_cantidad) CantidadTotal " +
                           "from Producto p " +
                           "join Stock s on p.prod_id = s.stoc_producto " +
                           "join Item_Venta i on i.item_producto = p.prod_id " +
                           "join Venta v on i.item_venta = v.vent_codigo ";

            List<SqlParameter> parameters = new List<SqlParameter>();

            if (!estadistica.EstaActivoProducto)
                query += " where p.prod_activo = @activo";
            else
                query += " where 1=1";  // No filtro si está activo

            if (!string.IsNullOrEmpty(estadistica.Año))
            {
                query += " and year(v.vent_fecha) = @anio";
                parameters.Add(new SqlParameter("@anio", SqlDbType.Int) { Value = estadistica.Año });
            }

            if (!string.IsNullOrEmpty(estadistica.Mes))
            {
                query += " and MONTH(v.vent_fecha) = @mes";
                parameters.Add(new SqlParameter("@mes", SqlDbType.Int) { Value = int.Parse(estadistica.Mes) });
            }

            if (!string.IsNullOrEmpty(estadistica.RubroProducto))
            {
                query += " and p.prod_rubro = @rubro";
                parameters.Add(new SqlParameter("@rubro", SqlDbType.Int) { Value = estadistica.RubroProducto });
            }

            query += " group by p.prod_codigo, p.prod_Detalle, p.prod_precio, s.stoc_cantidad_actual";

            if (!string.IsNullOrEmpty(estadistica.Año))
                query += ", year(v.vent_fecha)";
            if (!string.IsNullOrEmpty(estadistica.Mes))
                query += ", MONTH(v.vent_fecha)";
            if (!string.IsNullOrEmpty(estadistica.RubroProducto))
                query += ", p.prod_rubro";

            query += " order by sum(i.item_cantidad) desc";

            var resultados = dataBaseHelper.ExecQuery(query, parameters);
            return resultados;
        }
    }
}