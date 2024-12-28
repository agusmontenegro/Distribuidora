using Persistencia.DAOs.Venta;

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
            var venta = dAOVenta.ObtenerVenta(codigoVenta);
            return venta;
        }
    }
}