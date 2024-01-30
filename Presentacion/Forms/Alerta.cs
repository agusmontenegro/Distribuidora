using Logica.Services;
using System.Windows.Forms;

namespace Presentacion.Forms
{
    public partial class Alerta : Form
    {
        private int celda = -1;
        private readonly AlertaService AlertaService;

        public Alerta()
        {
            InitializeComponent();
            AlertaService = new AlertaService();
        }

        private void Alerta_Load(object sender, System.EventArgs e)
        {
            var alertas = AlertaService.ObtenerAlertas();
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