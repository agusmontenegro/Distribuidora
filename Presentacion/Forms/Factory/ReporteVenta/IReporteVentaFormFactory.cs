namespace Presentacion.Forms.Factory.ReporteVenta
{
    public interface IReporteVentaFormFactory
    {
        Venta.ReporteVenta CrearReporteVenta(string codigoVenta = null);
    }
}
