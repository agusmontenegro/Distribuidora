using Distribuidora.DTOs.Reportes;
using Distribuidora.Helpers;
using System.Collections.Generic;
using System.Data;

namespace Distribuidora.Services
{
    public class VentaService
    {
        private readonly DataBaseHelper dataBaseHelper;

        public VentaService()
        {
            dataBaseHelper = new DataBaseHelper();
        }

        public int GuardarVenta(string precioTotal)
        {
            dataBaseHelper.AgregarParametroEntrada(precioTotal, "@precioTotal", SqlDbType.Decimal);
            dataBaseHelper.AgregarParametroSalida("@codigo", SqlDbType.Int);

            var salidas = dataBaseHelper.ExecStoredProcedure("dbo.InsertarVenta");

            return int.Parse(salidas[0]);
        }

        public void GuardarItem(int codigoVenta, int producto, decimal precioUnitario, int cantidad)
        {
            dataBaseHelper.AgregarParametroEntrada(codigoVenta.ToString(), "@codigoVenta", SqlDbType.Int);
            dataBaseHelper.AgregarParametroEntrada(producto.ToString(), "@producto", SqlDbType.Int);
            dataBaseHelper.AgregarParametroEntrada(precioUnitario.ToString(), "@precioUnitario", SqlDbType.Decimal);
            dataBaseHelper.AgregarParametroEntrada(cantidad.ToString(), "@cantidad", SqlDbType.Int);

            _ = dataBaseHelper.ExecStoredProcedure("dbo.InsertarItem");
        }

        public Venta ObtenerVenta(string codigoVenta)
        {
            string query = "select * from dbo.Venta_View where Codigo = " + codigoVenta;
            var result = dataBaseHelper.ExecQuery(query);

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