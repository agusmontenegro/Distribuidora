using Persistencia.DAOs.Venta;
using Persistencia.DTOs.Reportes;
using System.Collections.Generic;
using System.Data;

namespace Logica.Services.Venta
{
    public class VentaService : IVentaService
    {
        private readonly IDAOVenta dAOVenta;

        public VentaService(IDAOVenta dAOVenta)
        {
            this.dAOVenta = dAOVenta;
        }

        public int GuardarVenta(string precioTotal)
        {
            var codigoVenta = dAOVenta.GuardarVenta(precioTotal);
            return codigoVenta;
        }

        public void GuardarItem(int codigoVenta, int IdProducto, decimal precioUnitario, int cantidad)
        {
            dAOVenta.GuardarItem(codigoVenta, IdProducto, precioUnitario, cantidad);
        }

        public Persistencia.DTOs.Reportes.Venta ObtenerVenta(string codigoVenta)
        {
            var result = dAOVenta.ObtenerVenta(codigoVenta);
            var venta = MapearVenta(result.Rows);
            return venta;
        }

        private Persistencia.DTOs.Reportes.Venta MapearVenta(DataRowCollection rows)
        {
            var venta = new Persistencia.DTOs.Reportes.Venta();
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