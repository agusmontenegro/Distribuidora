using Distribuidora.DTOs;
using Distribuidora.Services;
using System;
using System.Windows.Forms;

namespace Distribuidora.Producto
{
    public partial class Producto : Form
    {
        int celda = 0;
        string codigoProductoEditar;

        public Producto(string codigoProductoEditar = "")
        {
            InitializeComponent();
            this.codigoProductoEditar = codigoProductoEditar;
        }

        private void AltaProducto_Load(object sender, EventArgs e)
        {
            txtDetalleProductoComposicion.Enabled = false;
            txtCantidadComposicion.Enabled = false;
            btnAgregarComponente.Enabled = false;
            CargarCombos();

            if (!string.IsNullOrEmpty(codigoProductoEditar))
            {
                CargarDatosAlFormulario(codigoProductoEditar);
            }
        }

        private void CargarCombos()
        {
            cboRubros.Items.AddRange(RubroService.ObtenerRubros().ToArray());
            cboRubros.DisplayMember = "Detalle";
            cboRubros.ValueMember = "Codigo";
        }

        private void CargarDatosAlFormulario(string codigoProducto)
        {
            var producto = ProductoService.ObtenerProducto(codigoProducto);

            txtDetalleProducto.Text = producto.Detalle;
            txtPrecioUnitario.Text = producto.PrecioUnitario.ToString();
            cboRubros.Text = producto.Rubro.Detalle;
            txtStockMinimo.Text = producto.Stock.CantidadMinima;

            var combo = ComboService.ObtenerCombo(codigoProducto);

            foreach (var componente in combo.Componentes)
            {
                int rowId = grdComponentes.Rows.Add();

                grdComponentes.Rows[rowId].Cells[0].Value = componente.Producto.Codigo;
                grdComponentes.Rows[rowId].Cells[1].Value = componente.Producto.Detalle;
                grdComponentes.Rows[rowId].Cells[2].Value = componente.Cantidad;
            }
        }

        private void txtPrecioUnitario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtStockMinimo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtCantidadComposicion_KeyPress(object sender, KeyPressEventArgs e)
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
                AsignarAGrid();
            }
        }

        private void txtCodigoProductoComposicion_KeyPress(object sender, KeyPressEventArgs e)
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
                var codigo_producto = txtCodigoProductoComposicion.Text;
                string msj = string.Empty;

                if (ProductoService.CodigoProductoValido(codigo_producto, ref msj))
                {
                    CompletarItem(codigo_producto);
                    txtCantidadComposicion.Focus();
                    btnAgregarComponente.Enabled = true;
                }
                else
                {
                    MessageBox.Show(msj);
                }
            }
        }

        private void CompletarItem(string codigo_producto)
        {
            var producto = ProductoService.ObtenerProducto(codigo_producto);

            txtCodigoProductoComposicion.Enabled = false;
            txtDetalleProductoComposicion.Enabled = false;
            txtCantidadComposicion.Enabled = true;
            txtDetalleProductoComposicion.Text = producto.Detalle;
        }

        private void btnAgregarComponente_Click(object sender, EventArgs e)
        {
            AsignarAGrid();
        }

        private void AsignarAGrid()
        {
            string msj = string.Empty;

            if (ItemValido(ref msj))
            {
                int rowId = grdComponentes.Rows.Add();

                grdComponentes.Rows[rowId].Cells[0].Value = txtCodigoProductoComposicion.Text;
                grdComponentes.Rows[rowId].Cells[1].Value = txtDetalleProductoComposicion.Text;
                grdComponentes.Rows[rowId].Cells[2].Value = txtCantidadComposicion.Text;

                LimpiarFormularioDeComponentes();

                btnAgregarComponente.Enabled = true;
                txtCodigoProductoComposicion.Focus();
            }
            else
            {
                MessageBox.Show(msj);
                txtCantidadComposicion.Focus();
            }
        }

        private bool ItemValido(ref string msj)
        {
            ValidationService v = new ValidationService();

            if (string.IsNullOrEmpty(txtCantidadComposicion.Text))
            {
                v.Validations.Add(new ValidationService.Validation
                {
                    condition = false,
                    msj = "Debe ingresar la cantidad del componente"
                });
            }
            else
            {
                v.Validations.Add(new ValidationService.Validation
                {
                    condition = int.Parse(txtCantidadComposicion.Text) > 0,
                    msj = "La cantidad del componente no puede ser 0"
                });

                v.Validations.Add(new ValidationService.Validation
                {
                    condition = !ExisteItemEnGrid(txtCodigoProductoComposicion.Text.ToString()),
                    msj = "El producto " + txtCodigoProductoComposicion.Text.ToString() + " ya fue agregado"
                });
            }

            return v.validate(ref msj);
        }

        private bool ExisteItemEnGrid(string codigoProducto)
        {
            foreach (DataGridViewRow row in grdComponentes.Rows)
            {
                if (row.Cells[0].Value.ToString() == codigoProducto)
                {
                    return true;
                }
            }

            return false;
        }

        private void LimpiarFormularioDeComponentes()
        {
            txtCantidadComposicion.Enabled = false;
            btnAgregarComponente.Enabled = false;
            txtDetalleProductoComposicion.Enabled = false;
            txtCodigoProductoComposicion.Enabled = true;
            txtCodigoProductoComposicion.Text = string.Empty;
            txtDetalleProductoComposicion.Text = string.Empty;
            txtCantidadComposicion.Text = string.Empty;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string msj = string.Empty;

            if (DatosValidos(ref msj))
            {
                try
                {
                    if (!string.IsNullOrEmpty(codigoProductoEditar)) // update
                    {
                        ProductoService.ActualizarProducto(
                            codigoProductoEditar, txtDetalleProducto.Text, txtPrecioUnitario.Text, ((Rubro)cboRubros.SelectedItem).Codigo, txtStockMinimo.Text);

                        if (grdComponentes.Rows.Count > 0)
                        {
                            ComboService.EliminarComponentes(codigoProductoEditar);
                            for (int i = 0;i < grdComponentes.Rows.Count;++i)
                            {
                                ComboService.GuardarComponente(
                                    int.Parse(codigoProductoEditar), grdComponentes.Rows[i].Cells[0].Value.ToString(), grdComponentes.Rows[i].Cells[2].Value.ToString());
                            }
                        }
                    }
                    else // insert
                    {
                        var codigoProducto = ProductoService.GuardarProducto(
                            txtDetalleProducto.Text, txtPrecioUnitario.Text, ((Rubro)cboRubros.SelectedItem).Codigo, txtStockMinimo.Text);

                        if (grdComponentes.Rows.Count > 0)
                        {
                            for (int i = 0;i < grdComponentes.Rows.Count;++i)
                            {
                                ComboService.GuardarComponente(
                                    codigoProducto, grdComponentes.Rows[i].Cells[0].Value.ToString(), grdComponentes.Rows[i].Cells[2].Value.ToString());
                            }
                        }
                    }

                    MessageBox.Show("El producto ha sido guardado exitosamente");
                    Close();
                }
                catch (Exception)
                {
                    throw new Exception("Hubo un error al intentar guardar el producto");
                }
            }
            else
            {
                MessageBox.Show(msj);
            }
        }

        private bool DatosValidos(ref string msj)
        {
            ValidationService v = new ValidationService();

            v.Validations.Add(new ValidationService.Validation
            {
                condition = !string.IsNullOrEmpty(txtDetalleProducto.Text),
                msj = "Debe ingresar el detalle del producto"
            });

            v.Validations.Add(new ValidationService.Validation
            {
                condition = !string.IsNullOrEmpty(txtPrecioUnitario.Text),
                msj = "Debe ingresar el precio unitario del producto"
            });

            v.Validations.Add(new ValidationService.Validation
            {
                condition = cboRubros.SelectedIndex != -1,
                msj = "Debe seleccionar el rubro del producto"
            });

            v.Validations.Add(new ValidationService.Validation
            {
                condition = !string.IsNullOrEmpty(txtStockMinimo.Text),
                msj = "Debe ingresar el punto de reposición de stock"
            });

            return v.validate(ref msj);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (celda != -1)
            {
                grdComponentes.Rows.RemoveAt(celda);
                celda = -1;
            }
        }

        private void grdComponentes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            celda = e.RowIndex;
        }
    }
}