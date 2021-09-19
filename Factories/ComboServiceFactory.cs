using Distribuidora.Services;

namespace Distribuidora.Factories
{
    public class ComboServiceFactory
    {
        public static ComboService Crear()
        {
            return new ComboService();
        }
    }
}