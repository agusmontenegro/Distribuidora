using Distribuidora.Commons;
using Distribuidora.Services;
using System;
using System.Windows.Forms;

namespace Distribuidora
{
    public partial class Venta : Form
    {
        private DTOs.Stock Stock;
        private int celda = -1;
        private Menu menu;

        public Venta(Menu menu)
        {
            InitializeComponent();
            this.menu = menu;
        }

        private void Venta_Load(object sender, EventArgs e)
        {
            txtDetalleProducto.Enabled = false;
            txtCantidad.Enabled = false;
            txtStockActual.Enabled = false;
            txtNuevoStock.Enabled = false;
            txtPrecioUnitario.Enabled = false;
            txtSubtotal.Enabled = false;
            txtPrecioTotal.Enabled = false;
            btnConfirmarItem.Enabled = false;
            btnCancelarItem.Enabled = false;
            btnGuardarVenta.Enabled = false;
        }

        private void txtCodigoProducto_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormsCommon.OnlyNumerics(sender, e);

            if (e.KeyChar == (char)Keys.Return)
            {
                var codigo_producto = txtCodigoProducto.Text;
                string msj = string.Empty;

                if (ProductoService.CodigoProductoValido(codigo_producto, ref msj))
                {
                    CompletarItem(codigo_producto);
                    ObtenerStock(codigo_producto);
                    txtCantidad.Focus();
                }
                else
                {
                    MessageBox.Show(msj);
                }
            }
        }

        private void btnCancelarItem_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            txtCodigoProducto.Focus();
        }

        private void LimpiarFormulario()
        {
            txtCantidad.Enabled = false;
            btnCancelarItem.Enabled = false;
            btnConfirmarItem.Enabled = false;
            txtCodigoProducto.Enabled = true;
            txtCodigoProducto.Text = string.Empty;
            txtDetalleProducto.Text = string.Empty;
            txtPrecioUnitario.Text = string.Empty;
            txtNuevoStock.Text = string.Empty;
            txtStockActual.Text = string.Empty;
            txtSubtotal.Text = string.Empty;
            txtCantidad.Text = string.Empty;
            Stock = null;
        }

        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCantidad.Text))
            {
                var cantidad = int.Parse(txtCantidad.Text);
                var precioUnitario = decimal.Parse(txtPrecioUnitario.Text);
                var subtotal = cantidad * precioUnitario;

                txtSubtotal.Text = subtotal.ToString();

                if (!ComboService.EsCombo(txtCodigoProducto.Text))
                {
                    var nuevoStockActual = int.Parse(Stock.CantidadActual) - cantidad;
                    txtNuevoStock.Text = nuevoStockActual.ToString();
                }
                else
                    txtNuevoStock.Text = "--------";
            }
            else
            {
                txtSubtotal.Text = string.Empty;
                txtNuevoStock.Text = string.Empty;
            }
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormsCommon.OnlyNumerics(sender, e);

            if (e.KeyChar == (char)Keys.Return)
            {
                AsignarAGrid();
            }
        }

        private void CompletarItem(string codigo_producto)
        {
            var producto = ProductoService.ObtenerProducto(codigo_producto);

            txtCodigoProducto.Enabled = false;
            txtDetalleProducto.Enabled = false;
            txtCantidad.Enabled = true;
            btnCancelarItem.Enabled = true;
            btnConfirmarItem.Enabled = true;
            txtDetalleProducto.Text = producto.Detalle;
            txtPrecioUnitario.Text = producto.PrecioUnitario.ToString();
        }

        private void ObtenerStock(string codigo_producto)
        {
            // Se podría consultar igual el stock actual segun el stock actual de los componentes cuántos combos se pueden armar
            if (!ComboService.EsCombo(codigo_producto))
            {
                var producto = ProductoService.ObtenerProducto(codigo_producto);

                txtStockActual.Text = producto.Stock.CantidadActual.ToString();
                Stock = producto.Stock;
            }
            else
                txtStockActual.Text = "------";
        }

        private void btnConfirmarItem_Click(object sender, EventArgs e)
        {
            AsignarAGrid();
        }

        private void AsignarAGrid()
        {
            string msj = string.Empty;

            if (ItemValido(ref msj))
            {
                FormsCommon.AsignarAGrid(
                    grdVentas, 
                    txtCodigoProducto.Text, 
                    txtDetalleProducto.Text, 
                    txtCantidad.Text, 
                    txtSubtotal.Text, 
                    txtPrecioUnitario.Text);

                LimpiarFormulario();
                CalcularTotal();

                btnGuardarVenta.Enabled = true;
                txtCodigoProducto.Focus();
            }
            else
            {
                MessageBox.Show(msj);
                txtCantidad.Focus();
            }
        }

        private bool ItemValido(ref string msj)
        {
            ValidationService v = new ValidationService();

            if (string.IsNullOrEmpty(txtCantidad.Text))
            {
                v.Validations.Add(new ValidationService.Validation
                {
                    condition = false,
                    msj = "Debe ingresar la cantidad del producto a vender"
                });
            }
            else
            {
                v.Validations.Add(new ValidationService.Validation
                {
                    condition = int.Parse(txtCantidad.Text) > 0,
                    msj = "La cantidad del producto a vender no puede ser 0"
                });

                if (ComboService.EsCombo(txtCodigoProducto.Text))
                {
                    v.Validations.Add(new ValidationService.Validation
                    {
                        condition = ProductoService.HayStock(txtCodigoProducto.Text, txtCantidad.Text),
                        msj = "Stock no disponible en algunos de los componentes"
                    });
                }
                else
                {
                    v.Validations.Add(new ValidationService.Validation
                    {
                        condition = int.Parse(txtCantidad.Text) <= int.Parse(txtStockActual.Text),
                        msj = "Stock no disponible para la cantidad ingresada"
                    });
                }
            }

            v.Validations.Add(new ValidationService.Validation
            {
                condition = !ExisteItemEnGrid(txtCodigoProducto.Text.ToString()),
                msj = "El producto " + txtCodigoProducto.Text.ToString() + " ya fue agregado"
            });

            return v.validate(ref msj);
        }

        private bool ExisteItemEnGrid(string codigoProducto)
        {
            foreach (DataGridViewRow row in grdVentas.Rows)
            {
                if (row.Cells[0].Value.ToString() == codigoProducto)
                {
                    return true;
                }
            }

            return false;
        }

        private void CalcularTotal()
        {
            decimal sum = 0;

            for (int i = 0;i < grdVentas.Rows.Count;++i)
                sum += Convert.ToDecimal(grdVentas.Rows[i].Cells[3].Value);

            txtPrecioTotal.Text = sum.ToString();
        }

        private void btnEliminarItem_Click(object sender, EventArgs e)
        {
            if (celda != -1)
            {
                var subtotal = decimal.Parse(grdVentas.Rows[celda].Cells[3].Value.ToString());
                var totalActual = decimal.Parse(txtPrecioTotal.Text);
                var nuevoSubtotal = totalActual - subtotal;

                grdVentas.Rows.RemoveAt(celda);
                txtPrecioTotal.Text = nuevoSubtotal.ToString();
                celda = -1;
            }

            btnGuardarVenta.Enabled = grdVentas.Rows.Count > 0;
        }

        private void grdVentas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            celda = e.RowIndex;
        }

        private void btnGuardarVenta_Click(object sender, EventArgs e)
        {
            try
            {
                var codigoVenta = VentaService.GuardarVenta(txtPrecioTotal.Text);

                for (int i = 0;i < grdVentas.Rows.Count;++i)
                {
                    var producto = grdVentas.Rows[i].Cells[0].Value.ToString();
                    var cantidad = grdVentas.Rows[i].Cells[2].Value.ToString();
                    var precio = grdVentas.Rows[i].Cells[4].Value.ToString();

                    VentaService.GuardarItem(codigoVenta, int.Parse(producto), decimal.Parse(precio), int.Parse(cantidad));

                    if (ProductoService.HayQueReponer(producto))
                    {
                        AlertaService.EmitirAlertaDeReposicion(producto);
                        menu.CargarCantidadDeAlertas();
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("Hubo un error al intentar procesar la venta");
            }

            MessageBox.Show("La venta ha sido procesada con éxito");
            LimpiarFormulario();
            txtCodigoProducto.Focus();
            grdVentas.Rows.Clear();
            btnGuardarVenta.Enabled = false;
            txtPrecioTotal.Text = string.Empty;

            DialogResult dialogResult = MessageBox.Show(
                "¿Desea imprimir el comprobante de venta?",
                "Imprimir venta",
                MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    // Imprimir
                }
                catch (Exception)
                {
                    throw new Exception("Hubo un error al querer imprimir la venta");
                }
            }
        }
    }
}