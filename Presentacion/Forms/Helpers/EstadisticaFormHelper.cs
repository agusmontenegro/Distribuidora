using Persistencia.DTOs;

namespace Distribuidora.Forms.Helpers
{
    public class EstadisticaFormHelper
    {
        public Estadistica CompletarObjeto(Presentacion.Forms.Estadistica Form)
        {
            var estadistica = new Estadistica();

            if (!Form.chkIncludeNoActivo.Checked)
            {
                estadistica.EstaActivoProducto = true;
            }

            if (Form.cboAños.SelectedIndex != -1)
            {
                estadistica.Año = Form.cboAños.SelectedItem.ToString();
            }

            if (Form.cboMeses.SelectedIndex != -1)
            {
                estadistica.Mes = Form.cboMeses.SelectedItem.ToString();
            }

            if (Form.cboRubros.SelectedIndex != -1)
            {
                estadistica.RubroProducto = ((Rubro)Form.cboRubros.SelectedItem).Codigo;
            }

            return estadistica;
        }
    }
}