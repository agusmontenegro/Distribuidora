using Distribuidora.Factories;
using Distribuidora.Services;
using System.Windows.Forms;

namespace Distribuidora.Forms
{
    public partial class Alerta : Form
    {
        private readonly AlertaService alertaService;
        private int celda = -1;

        public Alerta()
        {
            InitializeComponent();
            alertaService = AlertaServiceFactory.Crear();
        }

        private void Alerta_Load(object sender, System.EventArgs e)
        {
            var alertas = alertaService.ObtenerAlertas();

            foreach (var alerta in alertas)
            {
                int rowId = grdAlertas.Rows.Add();

                grdAlertas.Rows[rowId].Cells[0].Value = alerta.Codigo;
                grdAlertas.Rows[rowId].Cells[1].Value = alerta.Detalle;
                grdAlertas.Rows[rowId].Cells[2].Value = alerta.TipoAlerta.Detalle;
            }
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