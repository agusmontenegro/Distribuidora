using System.Data;

namespace Persistencia.DAOs.Venta
{
    public interface IDAOVenta
    {
        int GuardarVenta(string precioTotal);
        void GuardarItem(int codigoVenta, int IdProducto, decimal precioUnitario, int cantidad);
        DataTable ObtenerVenta(string codigoVenta);
    }
}