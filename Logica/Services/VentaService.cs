using Persistencia.DAOs;
using Persistencia.DTOs.Reportes;

namespace Logica.Services
{
    public class VentaService
    {
        private readonly DAOVenta DAOVenta;

        public VentaService()
        {
            DAOVenta = new DAOVenta();
        }

        public int GuardarVenta(string precioTotal)
        {
            var codigoVenta = DAOVenta.GuardarVenta(precioTotal);
            return codigoVenta;
        }

        public void GuardarItem(int codigoVenta, int IdProducto, decimal precioUnitario, int cantidad)
        {
            DAOVenta.GuardarItem(codigoVenta, IdProducto, precioUnitario, cantidad);
        }

        public Venta ObtenerVenta(string codigoVenta)
        {
            var venta = DAOVenta.ObtenerVenta(codigoVenta);
            return venta;
        }
    }
}