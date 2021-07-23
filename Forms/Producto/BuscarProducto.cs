using Distribuidora.DTOs;
using Distribuidora.Helpers;
using Distribuidora.Services;
using System;
using System.Data;
using System.Windows.Forms;

namespace Distribuidora
{
    public partial class BuscarProducto : Form
    {
        public int celda = 0;

        public BuscarProducto()
        {
            InitializeComponent();
        }

        private void Producto_Load(object sender, EventArgs e)
        {
            btnEliminarProducto.Enabled = false;
            btnEditarProducto.Enabled = false;
            CargarCombos();
        }

        private void txtCodigoProducto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }

            if (e.KeyChar == (char)Keys.Return)
            {
                RealizarBusqueda();
            }
        }

        private void txtDetalleProducto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                RealizarBusqueda();
            }
        }

        private void CargarCombos()
        {
            cboRubros.Items.AddRange(RubroService.ObtenerRubros().ToArray());
            cboRubros.DisplayMember = "detalle";
            cboRubros.ValueMember = "codigo";
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarBusqueda();
        }

        private void LimpiarBusqueda()
        {
            cboRubros.SelectedIndex = -1;
            txtCodigoProducto.Text = string.Empty;
            txtDetalleProducto.Text = string.Empty;
            txtCodigoProducto.Focus();
            grdResult.Rows.Clear();
            btnEliminarProducto.Enabled = false;
            btnEditarProducto.Enabled = false;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            RealizarBusqueda();
        }

        private void RealizarBusqueda()
        {
            string msj = string.Empty;

            if (BusquedaValida(ref msj))
            {
                string query = "select prod_codigo, " +
                    "                  prod_detalle, " +
                    "                  prod_precio, " +
                    "                  rubr_detalle, " +
                    "                  stoc_cantidad_actual, " +
                    "                  stoc_ultima_reposicion" +
                    "           from dbo.Producto " +
                    "           join dbo.Rubro on rubr_codigo = prod_rubro" +
                    "           join dbo.Stock on stoc_producto = prod_codigo" +
                    "           where prod_activo = 1 ";

                if (!string.IsNullOrEmpty(txtCodigoProducto.Text))
                    query += "and prod_codigo like '%" + txtCodigoProducto.Text.Trim() + "%' ";

                if (!string.IsNullOrEmpty(txtDetalleProducto.Text))
                    query += "and prod_detalle like '%" + txtDetalleProducto.Text.Trim() + "%' ";

                if (cboRubros.SelectedIndex != -1)
                    query += "and prod_rubro = " + ((Rubro)cboRubros.SelectedItem).Codigo;

                CargarGrid(DataBaseHelper.ExecQuery(query));
                btnEliminarProducto.Enabled = true;
                btnEditarProducto.Enabled = true;

                if (grdResult.Rows.Count == 0)
                {
                    btnEliminarProducto.Enabled = false;
                    btnEditarProducto.Enabled = false;
                    MessageBox.Show("La búsqueda no arrojó ningún resultado");
                }
            }
            else
            {
                MessageBox.Show(msj);
                txtCodigoProducto.Focus();
            }
        }

        private bool BusquedaValida(ref string msj)
        {
            ValidationService v = new ValidationService();

            v.Validations.Add(new ValidationService.Validation
            {
                condition = !string.IsNullOrEmpty(txtCodigoProducto.Text) ||
                            !string.IsNullOrEmpty(txtDetalleProducto.Text) ||
                            cboRubros.SelectedIndex != -1,
                msj = "Ingrese algún criterio de búsqueda"
            });

            return v.validate(ref msj);
        }

        private void CargarGrid(DataTable result)
        {
            grdResult.Rows.Clear();

            for (int i = 0;i < result.Rows.Count;i++)
            {
                grdResult.Rows.Add(
                    result.Rows[i][0].ToString(),
                    result.Rows[i][1].ToString(),
                    result.Rows[i][2].ToString(),
                    result.Rows[i][3].ToString(),
                    result.Rows[i][4].ToString(),
                    result.Rows[i][5].ToString());
            }
        }

        private void btnEliminarProducto_Click(object sender, EventArgs e)
        {
            if (celda != -1)
            {
                var codigoProducto = grdResult.Rows[celda].Cells[0].Value.ToString();
                var detalleProducto = grdResult.Rows[celda].Cells[1].Value.ToString();

                DialogResult dialogResult = MessageBox.Show(
                    "Se eliminará el producto " + codigoProducto + " - " + detalleProducto + ".\n¿Continuar? ",
                    "Eliminar producto",
                    MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    try
                    {
                        ProductoService.EliminarProducto(codigoProducto);
                        grdResult.Rows.RemoveAt(celda);
                        celda = -1;
                        MessageBox.Show("Se ha eliminado el producto exitosamente");

                        if (grdResult.Rows.Count == 0)
                        {
                            btnEliminarProducto.Enabled = false;
                            btnEditarProducto.Enabled = false;
                        }
                    }
                    catch (Exception)
                    {
                        throw new Exception("Hubo un error al querer eliminar el producto");
                    }
                }
            }
            else
                MessageBox.Show("Seleccione un producto");
        }

        private void grdResult_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            celda = e.RowIndex;
        }

        private void btnNuevoProducto_Click(object sender, EventArgs e)
        {
            LimpiarBusqueda();
            var altaProducto = new Producto.Producto();
            altaProducto.ShowDialog();
        }

        private void btnEditarProducto_Click(object sender, EventArgs e)
        {
            if (celda != -1)
            {
                var codigoProducto = grdResult.Rows[celda].Cells[0].Value.ToString();
                var editarProducto = new Producto.Producto(codigoProducto);
                LimpiarBusqueda();
                editarProducto.ShowDialog();
            }
            else
                MessageBox.Show("Seleccione un producto");
        }
    }
}