using Logica.Reportes;
using Logica.Services.Venta;
using System;
using System.Windows.Forms;

namespace Presentacion.Forms.Venta
{
    public partial class ReporteVenta : Form
    {
        private string codigoVenta;
        private readonly IVentaService ventaService;

        public ReporteVenta(IVentaService ventaService)
        {
            InitializeComponent();
            this.ventaService = ventaService;
        }

        public void SetCodigoVenta(string codigoVenta)
        {
            this.codigoVenta = codigoVenta;
        }

        private void GenerarReporte()
        {
            var reporte = new VentaReport();
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