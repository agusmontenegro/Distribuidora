using Distribuidora.Forms.Producto;
using Distribuidora.Services;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Distribuidora.Forms
{
    public partial class Menu : Form
    {
        private readonly AlertaService alertaService;

        public Menu()
        {
            InitializeComponent();
            alertaService = new AlertaService();
        }

        private void btnSale_Click(object sender, EventArgs e)
        {
            var venta = new Venta.Venta(this);
            venta.ShowDialog();
        }

        private void btnStock_Click(object sender, EventArgs e)
        {
            var stock = new Stock(this);
            stock.ShowDialog();
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            var buscarProducto = new BuscarProducto();
            buscarProducto.ShowDialog();
        }

        private void btnEstadistica_Click(object sender, EventArgs e)
        {
            var estadistica = new Estadistica();
            estadistica.ShowDialog();
        }

        private void btnAlertas_Click(object sender, EventArgs e)
        {
            var alerta = new Alerta();
            alerta.ShowDialog();
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            CargarCantidadDeAlertas();
        }

        public void CargarCantidadDeAlertas()
        {
            var cantidadDeAlertas = alertaService.ObtenerCantidadDeAlertas();

            if (cantidadDeAlertas > 0)
            {
                btnAlertas.Text = "ALERTAS (" + cantidadDeAlertas + ")";
                btnAlertas.BackColor = Color.Red;
            }
            else
            {
                btnAlertas.Text = "ALERTAS (0)";
                btnAlertas.BackColor = Color.Green;
            }
        }
    }
}
