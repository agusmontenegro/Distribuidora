using Logica.Services;
using Logica.Reportes;
using System;
using System.Windows.Forms;

namespace Presentacion.Forms.Producto
{
    public partial class ReporteInfoProductos : Form
    {
        private readonly ProductoService ProductoService;

        public ReporteInfoProductos()
        {
            InitializeComponent();
            ProductoService = new ProductoService();
        }

        private void GenerarReporte()
        {
            var reporte = new InfoProductos();
            var productos = ProductoService.ObtenerProductos();
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
