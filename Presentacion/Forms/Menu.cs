using Logica.Services;
using Presentacion.Forms.Producto;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Presentacion.Forms
{
    public partial class Menu : Form
    {
        private readonly AlertaService AlertaService;

        public Menu()
        {
            InitializeComponent();
            AlertaService = new AlertaService();
        }

        private void btnSale_Click(object sender, EventArgs e)
        {
            var venta = new Venta.Venta(this);
            venta.ShowDialog();
        }

        private void btnStock_Click(object sender, EventArgs e)
        {
            var stock = new Stock.Stock(this);
            stock.ShowDialog();
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            var buscarProducto = new BuscarProducto(this);
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
            var cantidadDeAlertas = AlertaService.ObtenerCantidadDeAlertas();

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
