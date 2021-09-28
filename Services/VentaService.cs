using Distribuidora.Helpers;
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
    }
}