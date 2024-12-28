namespace Persistencia.DAOs.Venta
{
    public interface IDAOVenta
    {
        int GuardarVenta(string precioTotal);
        void GuardarItem(int codigoVenta, int IdProducto, decimal precioUnitario, int cantidad);
        DTOs.Reportes.Venta ObtenerVenta(string codigoVenta);
    }
}