using Distribuidora.Services;

namespace Distribuidora.Factories
{
    public class RubroServiceFactory
    {
        public static RubroService Crear()
        {
            return new RubroService();
        }
    }
}