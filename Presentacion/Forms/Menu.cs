using Logica.Services.Alerta;
using Presentacion.Forms.Observer;
using Presentacion.Forms.Producto;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Presentacion.Forms
{
    public partial class Menu : Form, ISuscriptorAlerta
    {
        private readonly IAlertaService alertaService;
        private readonly IPublisherAlerta publisherAlerta;
        private readonly BuscarProducto buscarProducto;
        private readonly Venta.Venta venta;
        private readonly Stock.Stock stock;
        private readonly Estadistica estadistica;
        private readonly Alerta alerta;

        public Menu(IAlertaService alertaService,
            IPublisherAlerta publisherAlerta,
            BuscarProducto buscarProducto,
            Venta.Venta venta,
            Stock.Stock stock,
            Estadistica estadistica,
            Alerta alerta)
        {
            InitializeComponent();
            this.alertaService = alertaService;
            this.publisherAlerta = publisherAlerta;
            this.buscarProducto = buscarProducto;
            this.venta = venta;
            this.stock = stock;
            this.estadistica = estadistica;
            this.alerta = alerta;
        }

        private void btnSale_Click(object sender, EventArgs e)
        {
            venta.ShowDialog();
        }

        private void btnStock_Click(object sender, EventArgs e)
        {
            stock.ShowDialog();
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            buscarProducto.ShowDialog();
        }

        private void btnEstadistica_Click(object sender, EventArgs e)
        {
            estadistica.ShowDialog();
        }

        private void btnAlertas_Click(object sender, EventArgs e)
        {
            alerta.ShowDialog();
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            publisherAlerta.Subscribe(this);
            ActualizarAlertas();
        }

        public void ActualizarAlertas()
        {
            var cantidadDeAlertas = alertaService.ObtenerCantidadDeAlertas();
            btnAlertas.Text = $"ALERTAS ({cantidadDeAlertas})";
            btnAlertas.BackColor = cantidadDeAlertas > 0 ? Color.Red : Color.Green;
        }

        private void Menu_FormClosing(object sender, FormClosingEventArgs e)
        {
            publisherAlerta.Unsubscribe(this);
        }
    }
}
