using Distribuidora.Services;
using System;
using System.Windows.Forms;

namespace Distribuidora.Forms.Stock
{
    public partial class ReporteStock : Form
    {
        private readonly string codigoReposicion;
        private readonly StockService stockService;

        public ReporteStock(string codigoReposicion)
        {
            InitializeComponent();
            this.codigoReposicion = codigoReposicion;
            stockService = new StockService();
        }

        private void GenerarReporte()
        {
            var reporte = new Reportes.StockReport();
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