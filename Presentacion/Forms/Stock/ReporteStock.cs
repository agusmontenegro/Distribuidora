using Logica.Reportes;
using Logica.Services.Stock;
using System;
using System.Windows.Forms;

namespace Presentacion.Forms.Stock
{
    public partial class ReporteStock : Form
    {
        private readonly string codigoReposicion;
        private readonly IStockService stockService;

        public ReporteStock(string codigoReposicion,
            IStockService stockService)
        {
            InitializeComponent();
            this.codigoReposicion = codigoReposicion;
            this.stockService = stockService;
        }

        private void GenerarReporte()
        {
            var reporte = new StockReport();
            var reposicion = stockService.ObtenerReposicion(codigoReposicion);
            reporte.txtFechaReposicionParametro.Value = reposicion.Fecha.ToString();
            reporte.tblStock.DataSource = reposicion.Items;
            rptStock.Report = reporte;
            rptStock.RefreshReport();
        }

        private void ReporteStock_Load(object sender, EventArgs e)
        {
            GenerarReporte();
        }
    }
}