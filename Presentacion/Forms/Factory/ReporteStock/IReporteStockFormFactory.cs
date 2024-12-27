using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentacion.Forms.Factory.ReporteStock
{
    public interface IReporteStockFormFactory
    {
        Stock.ReporteStock CrearReporteStock(string codigoReposicion = null);
    }
}
