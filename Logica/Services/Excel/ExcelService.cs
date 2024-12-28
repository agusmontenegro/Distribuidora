using Persistencia.Helpers.Excel;

namespace Logica.Services.Excel
{
    public class ExcelService : IExcelService
    {
        private readonly IExcelHelper excelHelper;

        public ExcelService(IExcelHelper excelHelper)
        {
            this.excelHelper = excelHelper;
        }

        public string ImportarProductos()
        {
            return excelHelper.ImportarProductos();
        }

        public string ExportarProductos()
        {
            return excelHelper.ExportarProductos();
        }
    }
}
