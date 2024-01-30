using Logica.Services;
using Logica.Reportes;
using System;
using System.Windows.Forms;

namespace Presentacion.Forms.Venta
{
    public partial class ReporteVenta : Form
    {
        private readonly string CodigoVenta;
        private readonly VentaService VentaService;

        public ReporteVenta(string CodigoVenta)
        {
            InitializeComponent();
            this.CodigoVenta = CodigoVenta;
            VentaService = new VentaService();
        }

        private void GenerarReporte()
        {
            var reporte = new VentaReport();
            var venta = VentaService.ObtenerVenta(CodigoVenta);
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