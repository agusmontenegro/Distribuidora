using Persistencia.Helpers;

namespace Logica.Services.Excel
{
    public class ExcelService : IExcelService
    {
        private readonly ExcelHelper ExcelHelper;

        public ExcelService()
        {
            ExcelHelper = new ExcelHelper();
        }

        public string ImportarProductos()
        {
            return ExcelHelper.ImportarProductos();
        }

        public string ExportarProductos()
        {
            return ExcelHelper.ExportarProductos();
        }
    }
}
