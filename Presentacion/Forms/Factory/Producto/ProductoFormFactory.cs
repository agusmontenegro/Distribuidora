using Microsoft.Extensions.DependencyInjection;
using System;

namespace Presentacion.Forms.Factory.Producto
{
    public class ProductoFormFactory : IProductoFormFactory
    {
        private readonly IServiceProvider serviceProvider;

        public ProductoFormFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        Forms.Producto.Producto IProductoFormFactory.CrearFormularioProducto(string idProduct)
        {
            var productoForm = serviceProvider.GetRequiredService<Forms.Producto.Producto>();
            productoForm.SetIdProducto(idProduct);
            return productoForm;
        }
    }
}