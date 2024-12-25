using Logica.Services.Alerta;
using System.Windows.Forms;

namespace Presentacion.Forms
{
    public partial class Alerta : Form
    {
        private int celda = -1;
        private readonly IAlertaService alertaService;

        public Alerta(IAlertaService alertaService)
        {
            InitializeComponent();
            this.alertaService = alertaService;
        }

        private void Alerta_Load(object sender, System.EventArgs e)
        {
            var alertas = alertaService.ObtenerAlertas();
            grdAlertas.DataSource = alertas;
        }

        private void grdAlertas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            celda = e.RowIndex;
        }

        private void grdAlertas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            MessageBox.Show(grdAlertas.Rows[celda].Cells[1].Value.ToString());
        }
    }
}