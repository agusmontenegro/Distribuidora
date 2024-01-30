using Persistencia.DTOs.Reportes;
using Persistencia.Helpers;
using System.Collections.Generic;
using System.Data;

namespace Persistencia.DAOs
{
    public class DAOVenta
    {
        private readonly DataBaseHelper DataBaseHelper;

        public DAOVenta()
        {
            DataBaseHelper = new DataBaseHelper();
        }

        public int GuardarVenta(string precioTotal)
        {
            DataBaseHelper.AgregarParametroEntrada(precioTotal, "@precioTotal", SqlDbType.Decimal);
            DataBaseHelper.AgregarParametroSalida("@codigo", SqlDbType.Int);

            var salidas = DataBaseHelper.ExecStoredProcedure("dbo.InsertarVenta");

            return int.Parse(salidas[0]);
        }

        public void GuardarItem(int codigoVenta, int producto, decimal precioUnitario, int cantidad)
        {
            DataBaseHelper.AgregarParametroEntrada(codigoVenta.ToString(), "@codigoVenta", SqlDbType.Int);
            DataBaseHelper.AgregarParametroEntrada(producto.ToString(), "@producto", SqlDbType.Int);
            DataBaseHelper.AgregarParametroEntrada(precioUnitario.ToString(), "@precioUnitario", SqlDbType.Decimal);
            DataBaseHelper.AgregarParametroEntrada(cantidad.ToString(), "@cantidad", SqlDbType.Int);

            _ = DataBaseHelper.ExecStoredProcedure("dbo.InsertarItem");
        }

        public Venta ObtenerVenta(string codigoVenta)
        {
            string query = "select * from dbo.Venta_View where Codigo = " + codigoVenta;
            var result = DataBaseHelper.ExecQuery(query);

            var venta = MapearVenta(result.Rows);

            return venta;
        }

        private Venta MapearVenta(DataRowCollection rows)
        {
            var venta = new Venta();
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
