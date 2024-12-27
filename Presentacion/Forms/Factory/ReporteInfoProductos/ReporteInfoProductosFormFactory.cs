using Microsoft.Extensions.DependencyInjection;
using System;

namespace Presentacion.Forms.Factory.ReporteInfoProductos
{
    public class ReporteInfoProductosFormFactory : IReporteInfoProductosFormFactory
    {
        private readonly IServiceProvider serviceProvider;

        public ReporteInfoProductosFormFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public Forms.Producto.ReporteInfoProductos CrearReporteProducto()
        {
            var reporteForm = serviceProvider.GetRequiredService<Forms.Producto.ReporteInfoProductos>();
            return reporteForm;
        }
    }
}