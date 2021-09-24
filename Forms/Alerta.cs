using Distribuidora.Commons;
using Distribuidora.Services;
using System.Windows.Forms;

namespace Distribuidora.Forms
{
    public partial class Alerta : Form
    {
        private int celda = -1;
        private readonly AlertaService alertaService;
        private readonly FormsCommon formsCommon;

        public Alerta()
        {
            InitializeComponent();
            alertaService = new AlertaService();
            formsCommon = new FormsCommon();
        }

        private void Alerta_Load(object sender, System.EventArgs e)
        {
            var alertas = alertaService.ObtenerAlertas();

            foreach (var alerta in alertas)
            {
                formsCommon.AsignarAGrid(
                    grdAlertas,
                    alerta.Codigo,
                    alerta.Detalle,
                    alerta.TipoAlerta.Detalle);
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