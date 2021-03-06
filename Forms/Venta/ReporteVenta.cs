using Distribuidora.Services;
using System;
using System.Windows.Forms;

namespace Distribuidora.Forms.Venta
{
    public partial class ReporteVenta : Form
    {
        private readonly string codigoVenta;
        private readonly VentaService ventaService;

        public ReporteVenta(string codigoVenta)
        {
            InitializeComponent();
            this.codigoVenta = codigoVenta;
            ventaService = new VentaService();
        }

        private void GenerarReporte()
        {
            var reporte = new Reportes.VentaReport();
            var venta = ventaService.ObtenerVenta(codigoVenta);
            reporte.txtFechaParametro.Value = venta.Fecha;
            reporte.txtTotalParametro.Value = venta.Total.ToString();
            reporte.tblVenta.DataSource = venta.Items;
            rptVenta.Report = reporte;
            rptVenta.RefreshReport();
        }

        private void ReporteVenta_Load(object sender, EventArgs e)
        {
            GenerarReporte();
        }
    }
}