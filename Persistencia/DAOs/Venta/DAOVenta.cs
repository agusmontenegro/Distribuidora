using Persistencia.DTOs.Reportes;
using Persistencia.Helpers.DataBase;
using System.Collections.Generic;
using System.Data;

namespace Persistencia.DAOs.Venta
{
    public class DAOVenta : IDAOVenta
    {
        private readonly IDataBaseHelper dataBaseHelper;

        public DAOVenta(IDataBaseHelper dataBaseHelper)
        {
            this.dataBaseHelper = dataBaseHelper;
        }

        public int GuardarVenta(string precioTotal)
        {
            dataBaseHelper.AgregarParametroEntrada(precioTotal, "@precioTotal", SqlDbType.Decimal);
            dataBaseHelper.AgregarParametroSalida("@codigo", SqlDbType.Int);

            var salidas = dataBaseHelper.ExecStoredProcedure("dbo.InsertarVenta");

            return int.Parse(salidas[0]);
        }

        public void GuardarItem(int codigoVenta, int IdProducto, decimal precioUnitario, int cantidad)
        {
            dataBaseHelper.AgregarParametroEntrada(codigoVenta.ToString(), "@codigoVenta", SqlDbType.Int);
            dataBaseHelper.AgregarParametroEntrada(IdProducto.ToString(), "@producto", SqlDbType.Int);
            dataBaseHelper.AgregarParametroEntrada(precioUnitario.ToString(), "@precioUnitario", SqlDbType.Decimal);
            dataBaseHelper.AgregarParametroEntrada(cantidad.ToString(), "@cantidad", SqlDbType.Int);

            _ = dataBaseHelper.ExecStoredProcedure("dbo.InsertarItem");
        }

        public DTOs.Reportes.Venta ObtenerVenta(string codigoVenta)
        {
            string query = "select * from dbo.Venta_View where Codigo = " + codigoVenta;
            var result = dataBaseHelper.ExecQuery(query);

            var venta = MapearVenta(result.Rows);

            return venta;
        }

        private DTOs.Reportes.Venta MapearVenta(DataRowCollection rows)
        {
            var venta = new DTOs.Reportes.Venta();
            venta.Fecha = rows[0]["Fecha"].ToString();
            venta.Total = (decimal)rows[0]["Total"];
            venta.Items = new List<ItemVenta>();

            foreach (DataRow row in rows)
            {
                var item = new ItemVenta();

                item.Producto = row["Producto"].ToString();
                item.Detalle = row["Detalle"].ToString();
                item.PrecioUnitario = (decimal)row["Precio"];
                item.Cantidad = (int)row["Cantidad"];
                item.Subtotal = (decimal)row["Subtotal"];

                venta.Items.Add(item);
            }

            return venta;
        }
    }
}
