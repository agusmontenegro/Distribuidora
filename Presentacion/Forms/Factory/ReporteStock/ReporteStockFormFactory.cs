using Microsoft.Extensions.DependencyInjection;
using System;

namespace Presentacion.Forms.Factory.ReporteStock
{
    public class ReporteStockFormFactory : IReporteStockFormFactory
    {
        private readonly IServiceProvider serviceProvider;

        public ReporteStockFormFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public Stock.ReporteStock CrearReporteStock(string codigoReposicion)
        {
            var reporteStockForm = serviceProvider.GetRequiredService<Stock.ReporteStock>();
            reporteStockForm.SetCodigoReposicion(codigoReposicion);
            return reporteStockForm;
        }
    }
}
