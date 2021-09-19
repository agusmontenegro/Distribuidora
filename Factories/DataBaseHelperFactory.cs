using Distribuidora.Helpers;

namespace Distribuidora.Factories
{
    public class DataBaseHelperFactory
    {
        public static DataBaseHelper Crear()
        {
            return new DataBaseHelper();
        }
    }
}