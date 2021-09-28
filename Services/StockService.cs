using Distribuidora.Helpers;
using System.Data;

namespace Distribuidora.Services
{
    public class StockService
    {
        private readonly DataBaseHelper dataBaseHelper;

        public StockService()
        {
            dataBaseHelper = new DataBaseHelper();
        }

        public void ReponerStock(string codigoProducto, string cantidadAReponer)
        {
            dataBaseHelper.AgregarParametroEntrada(codigoProducto, "@codigoProducto", SqlDbType.Int);
            dataBaseHelper.AgregarParametroEntrada(cantidadAReponer, "@cantidadAReponer", SqlDbType.Int);

            _ = dataBaseHelper.ExecStoredProcedure("dbo.ReponerStock");
        }
    }
}