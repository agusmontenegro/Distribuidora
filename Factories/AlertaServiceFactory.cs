using Distribuidora.Services;

namespace Distribuidora.Factories
{
    public class AlertaServiceFactory
    {
        public static AlertaService Crear()
        {
            return new AlertaService();
        }
    }
}