using Persistencia.DTOs;
using Persistencia.Helpers.DataBase;
using System.Collections.Generic;
using System.Data;
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

        public DataTable ObtenerProductosParaExcel()
        {
            string query = "select Codigo, Detalle, Precio from dbo.Producto_View";
            var result = dataBaseHelper.ExecQuery(query);
            return result;
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

        public void ActualizarProductoLazy(string id, string codigo, string detalle, string precioUnitario)
        {
            dataBaseHelper.AgregarParametroEntrada(id, "@id", SqlDbType.Int);
            dataBaseHelper.AgregarParametroEntrada(codigo, "@codigo", SqlDbType.NVarChar);
            dataBaseHelper.AgregarParametroEntrada(detalle, "@detalle", SqlDbType.NVarChar);
            dataBaseHelper.AgregarParametroEntrada(precioUnitario, "@precioUnitario", SqlDbType.Decimal);

            _ = dataBaseHelper.ExecStoredProcedure("dbo.ActualizarProductoLazy");
        }

        public List<DTOs.Producto> Buscar(DTOs.Producto producto)
        {
            string query = "select * from dbo.Producto_View ";

            if (!string.IsNullOrEmpty(producto.Codigo))
                if (query.Contains("where"))
                    query += " and Codigo like '%" + producto.Codigo.Trim() + "%' ";
                else
                    query += " where Codigo like '%" + producto.Codigo.Trim() + "%' ";

            if (!string.IsNullOrEmpty(producto.Detalle))
                if (query.Contains("where"))
                    query += " and Detalle like '%" + producto.Detalle.Trim() + "%' ";
                else
                    query += " where Detalle like '%" + producto.Detalle.Trim() + "%' ";

            if (producto.Rubro != null)
                if (query.Contains("where"))
                    query += " and RubroCodigo = " + producto.Rubro.Codigo;
                else
                    query += " where RubroCodigo = " + producto.Rubro.Codigo;

            var results = dataBaseHelper.ExecQuery(query);
            var productos = MapearProductos(results.Rows);
            return productos;
        }

        public List<Estadistica> BuscarParaEstadistica(Estadistica estadistica)
        {
            string query = "select top 100 p.prod_codigo Codigo," +
                "                          p.prod_detalle Detalle," +
                "            			   p.prod_precio Precio," +
                "            			   s.stoc_cantidad_actual StockActual," +
                "                          sum(i.item_cantidad) CantidadTotal" +
                "           from Producto p" +
                "           join Stock s on p.prod_id = s.stoc_producto" +
                "           join Item_Venta i on i.item_producto = p.prod_id" +
                "           join Venta v on i.item_venta = v.vent_codigo ";

            if (!estadistica.EstaActivoProducto)
                query += " where p.prod_activo = 1";

            if (!string.IsNullOrEmpty(estadistica.Año))
                if (query.Contains("where"))
                    query += " and year(v.vent_fecha) = " + estadistica.Año;
                else
                    query += " where year(v.vent_fecha) = " + estadistica.Año;

            if (!string.IsNullOrEmpty(estadistica.Mes))
                if (query.Contains("where"))
                    query += " and MONTH(v.vent_fecha) = " + (estadistica.Mes + 1).ToString();
                else
                    query += " where MONTH(v.vent_fecha) = " + (estadistica.Mes + 1).ToString();

            if (!string.IsNullOrEmpty(estadistica.RubroProducto))
                if (query.Contains("where"))
                    query += " and p.prod_rubro = " + estadistica.RubroProducto;
                else
                    query += " where p.prod_rubro = " + estadistica.RubroProducto;

            query += " group by p.prod_codigo, p.prod_Detalle, p.prod_precio, s.stoc_cantidad_actual";

            if (!string.IsNullOrEmpty(estadistica.Año))
                query += " ,year(v.vent_fecha) ";

            if (!string.IsNullOrEmpty(estadistica.Mes))
                query += " ,MONTH(v.vent_fecha) ";

            if (!string.IsNullOrEmpty(estadistica.RubroProducto))
                query += " ,p.prod_rubro";

            query += " order by sum(i.item_cantidad) desc";

            var resultados = dataBaseHelper.ExecQuery(query);
            var estadisticas = MapearEstadisticas(resultados.Rows);

            return estadisticas;
        }
    }
}