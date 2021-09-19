using Distribuidora.Services;

namespace Distribuidora.Factories
{
    public class ValidacionServiceFactory
    {
        public static ValidacionService Crear()
        {
            return new ValidacionService();
        }
    }
}