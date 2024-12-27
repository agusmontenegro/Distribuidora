using Logica.Reportes;
using Logica.Services.Stock;
using System;
using System.Windows.Forms;

namespace Presentacion.Forms.Stock
{
    public partial class ReporteStock : Form
    {
        private string codigoReposicion;
        private readonly IStockService stockService;

        public ReporteStock(IStockService stockService)
        {
            InitializeComponent();
            this.stockService = stockService;
        }

        public void SetCodigoReposicion(string codigoReposicion)
        {
            this.codigoReposicion = codigoReposicion;
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