using Distribuidora.Services;

namespace Distribuidora.Factories
{
    public class VentaServiceFactory
    {
        public static VentaService Crear()
        {
            return new VentaService();
        }
    }
}