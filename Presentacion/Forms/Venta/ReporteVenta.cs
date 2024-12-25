using Logica.Reportes;
using Logica.Services.Venta;
using System;
using System.Windows.Forms;

namespace Presentacion.Forms.Venta
{
    public partial class ReporteVenta : Form
    {
        private readonly string CodigoVenta;
        private readonly IVentaService ventaService;

        public ReporteVenta(string CodigoVenta,
            IVentaService ventaService)
        {
            InitializeComponent();
            this.CodigoVenta = CodigoVenta;
            this.ventaService = ventaService;
        }

        private void GenerarReporte()
        {
            var reporte = new VentaReport();
            var venta = ventaService.ObtenerVenta(CodigoVenta);
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