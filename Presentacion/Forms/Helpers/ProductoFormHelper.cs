using Presentacion.Forms.Producto;
using System.Windows.Forms;

namespace Distribuidora.Forms.Helpers
{
    public class ProductoFormHelper
    {
        public void CargarDatosAlFormulario(Producto form, Persistencia.DTOs.Producto producto)
        {
            form.txtCodigoProducto.Text = producto.Codigo;
            form.txtDetalleProducto.Text = producto.Detalle;
            form.txtPrecioUnitario.Text = producto.PrecioUnitario.ToString();
            form.cboRubros.Text = producto.Rubro.Detalle;
            form.txtStockMinimo.Text = producto.Stock.CantidadMinima;
        }

        public void CargarDatosALaGrilla(Producto form, Persistencia.DTOs.Combo combo)
        {
            foreach (var componente in combo.Componentes)
            {
                int rowId = form.grdComponentes.Rows.Add();

                form.grdComponentes.Rows[rowId].Cells[0].Value = componente.Producto.Codigo;
                form.grdComponentes.Rows[rowId].Cells[1].Value = componente.Producto.Detalle;
                form.grdComponentes.Rows[rowId].Cells[2].Value = componente.Cantidad;
                form.grdComponentes.Rows[rowId].Cells[3].Value = componente.Producto.Id;
            }
        }

        public bool ExisteItemEnGrid(Producto form, string codigoProducto)
        {
            foreach (DataGridViewRow row in form.grdComponentes.Rows)
            {
                if (row.Cells[0].Value.ToString() == codigoProducto)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
