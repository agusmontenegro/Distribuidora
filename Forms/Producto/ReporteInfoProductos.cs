using Distribuidora.Services;
using System;
using System.Windows.Forms;

namespace Distribuidora.Forms.Producto
{
    public partial class ReporteInfoProductos : Form
    {
        private readonly ProductoService productoService;

        public ReporteInfoProductos()
        {
            InitializeComponent();
            productoService = new ProductoService();
        }

        private void GenerarReporte()
        {
            var reporte = new Reportes.InfoProductos();
            var productos = productoService.ObtenerProductos();
            reporte.txtFechaParametro.Value = DateTime.Today.ToString("dd/MM/yyyy");
            reporte.tblProductos.DataSource = productos;
            rptInfoProductos.Report = reporte;
            rptInfoProductos.RefreshReport();
        }

        private void ReporteInfoProductos_Load(object sender, EventArgs e)
        {
            GenerarReporte();
        }
    }
}
