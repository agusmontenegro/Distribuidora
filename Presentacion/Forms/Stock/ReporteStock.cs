using Logica.Reportes;
using Logica.Services;
using System;
using System.Windows.Forms;

namespace Presentacion.Forms.Stock
{
    public partial class ReporteStock : Form
    {
        private readonly string codigoReposicion;
        private readonly StockService StockService;

        public ReporteStock(string codigoReposicion)
        {
            InitializeComponent();
            this.codigoReposicion = codigoReposicion;
            StockService = new StockService();
        }

        private void GenerarReporte()
        {
            var reporte = new StockReport();
            var reposicion = StockService.ObtenerReposicion(codigoReposicion);
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