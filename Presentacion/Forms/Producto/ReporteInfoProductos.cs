using Logica.Reportes;
using Logica.Services.Producto;
using System;
using System.Windows.Forms;

namespace Presentacion.Forms.Producto
{
    public partial class ReporteInfoProductos : Form
    {
        private readonly IProductoService productoService;

        public ReporteInfoProductos(
            IProductoService productoService)
        {
            InitializeComponent();
            this.productoService = productoService;
        }

        private void GenerarReporte()
        {
            var reporte = new InfoProductos();
            var productos = productoService.ObtenerProductos();
            reporte.txtFechaParametro.Value = DateTime.Now.ToString();
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
