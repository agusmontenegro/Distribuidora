using Distribuidora.Services;

namespace Distribuidora.Factories
{
    public class StockServiceFactory
    {
        public static StockService Crear()
        {
            return new StockService();
        }
    }
}