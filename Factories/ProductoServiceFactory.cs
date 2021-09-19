using Distribuidora.Services;

namespace Distribuidora.Factories
{
    public class ProductoServiceFactory
    {
        public static ProductoService Crear()
        {
            return new ProductoService();
        }
    }
}