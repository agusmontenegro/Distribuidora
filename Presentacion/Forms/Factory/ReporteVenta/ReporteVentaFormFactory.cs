using Microsoft.Extensions.DependencyInjection;
using System;

namespace Presentacion.Forms.Factory.ReporteVenta
{
    public class ReporteVentaFormFactory : IReporteVentaFormFactory
    {
        private readonly IServiceProvider serviceProvider;

        public ReporteVentaFormFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public Venta.ReporteVenta CrearReporteVenta(string codigoVenta = null)
        {
            var reporteVentaForm = serviceProvider.GetRequiredService<Venta.ReporteVenta>();
            reporteVentaForm.SetCodigoVenta(codigoVenta);
            return reporteVentaForm;
        }
    }
}
