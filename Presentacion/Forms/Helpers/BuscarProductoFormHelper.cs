using Persistencia.DTOs;
using Presentacion.Forms.Producto;

namespace Presentacion.Forms.Helpers
{
    public class BuscarProductoFormHelper
    {
        public Persistencia.DTOs.Producto CompletarObjeto(BuscarProducto Form)
        {
            var producto = new Persistencia.DTOs.Producto();

            if (!string.IsNullOrEmpty(Form.txtCodigoProducto.Text))
                producto.Codigo = Form.txtCodigoProducto.Text.Trim();

            if (!string.IsNullOrEmpty(Form.txtDetalleProducto.Text))
                producto.Detalle = Form.txtDetalleProducto.Text.Trim();

            if (Form.cboRubros.SelectedIndex != -1)
            {
                producto.Rubro = new Rubro();
                producto.Rubro.Codigo = ((Rubro)Form.cboRubros.SelectedItem).Codigo;
            }

            return producto;
        }
    }
}