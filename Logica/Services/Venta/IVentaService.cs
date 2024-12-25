namespace Logica.Services.Venta
{
    public interface IVentaService
    {
        int GuardarVenta(string precioTotal);
        void GuardarItem(int codigoVenta, int IdProducto, decimal precioUnitario, int cantidad);
        Persistencia.DTOs.Reportes.Venta ObtenerVenta(string codigoVenta);
    }
}