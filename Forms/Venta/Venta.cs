using Distribuidora.Commons;
using Distribuidora.Services;
using System;
using System.Windows.Forms;

namespace Distribuidora.Forms.Venta
{
    public partial class Venta : Form
    {
        private DTOs.Stock Stock;
        private int celda = -1;
        private string idProducto;

        private readonly Menu menu;
        private readonly FormsCommon formsCommon;
        private readonly ProductoService productoService;
        private readonly ComboService comboService;
        private readonly ValidacionService validacionService;
        private readonly AlertaService alertaService;
        private readonly VentaService ventaService;
        private readonly StockService stockService;

        public Venta(Menu menu)
        {
            InitializeComponent();
            this.menu = menu;
            formsCommon = new FormsCommon();
            productoService = new ProductoService();
            comboService = new ComboService();
            validacionService = new ValidacionService();
            alertaService = new AlertaService();
            ventaService = new VentaService();
            stockService = new StockService();
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
            if (e.KeyChar == (char)Keys.Return)
            {
                var codigoProducto = txtCodigoProducto.Text;
                string msj = string.Empty;

                if (CodigoProductoValido(codigoProducto, ref msj))
                {
                    CompletarItem(codigoProducto);
                    ObtenerStock();
                    txtCantidad.Focus();
                }
                else
                {
                    MessageBox.Show(msj);
                }
            }
        }

        private bool CodigoProductoValido(string codigoProducto, ref string msj)
        {
            if (string.IsNullOrEmpty(codigoProducto))
            {
                validacionService.AgregarValidacion(
                    false,
                    "Debe ingresar un código de producto.");
            }
            else
            {
                validacionService.AgregarValidacion(
                    productoService.ExisteProductoSegunCodigo(codigoProducto),
                    "No existe un producto activo con el código ingresado.");
            }

            return validacionService.Validar(ref msj);
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
            idProducto = string.Empty;
        }

        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCantidad.Text))
            {
                var cantidad = int.Parse(txtCantidad.Text);
                var precioUnitario = decimal.Parse(txtPrecioUnitario.Text);
                var subtotal = cantidad * precioUnitario;

                txtSubtotal.Text = subtotal.ToString();

                if (!comboService.EsCombo_Id(idProducto))
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
            formsCommon.OnlyNumerics(sender, e);

            if (e.KeyChar == (char)Keys.Return)
            {
                AsignarAGrid();
            }
        }

        private void CompletarItem(string codigoProducto)
        {
            var producto = productoService.ObtenerProductosPorCodigo(codigoProducto)[0];

            idProducto = producto.Id;
            txtCodigoProducto.Enabled = false;
            txtDetalleProducto.Enabled = false;
            txtCantidad.Enabled = true;
            btnCancelarItem.Enabled = true;
            btnConfirmarItem.Enabled = true;
            txtDetalleProducto.Text = producto.Detalle;
            txtPrecioUnitario.Text = producto.PrecioUnitario.ToString();
        }

        private void ObtenerStock()
        {
            // Se podría consultar igual el stock actual segun el stock actual de los componentes cuántos combos se pueden armar
            if (!comboService.EsCombo_Id(idProducto))
            {
                var producto = productoService.ObtenerProductoPorId(idProducto);

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
                formsCommon.AsignarAGrid(
                    grdVentas,
                    txtCodigoProducto.Text,
                    txtDetalleProducto.Text,
                    txtCantidad.Text,
                    txtSubtotal.Text,
                    txtPrecioUnitario.Text,
                    idProducto);

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
            if (string.IsNullOrEmpty(txtCantidad.Text))
            {
                validacionService.AgregarValidacion(
                    false, "Debe ingresar la cantidad del producto a vender");
            }
            else
            {
                validacionService.AgregarValidacion(
                    int.Parse(txtCantidad.Text) > 0, "La cantidad del producto a vender no puede ser 0");

                if (comboService.EsCombo_Id(idProducto))
                {
                    if (!stockService.HayStock(idProducto, txtCantidad.Text))
                    {
                        var stockInfo = comboService.InformarStockFaltante(idProducto, txtCantidad.Text);
                        validacionService.AgregarValidacion(false, "Stock no disponible para la cantidad ingresada. \n" + stockInfo);
                    }
                }
                else
                {
                    validacionService.AgregarValidacion(
                        int.Parse(txtCantidad.Text) <= int.Parse(txtStockActual.Text),
                        "Stock no disponible para la cantidad ingresada");
                }
            }

            validacionService.AgregarValidacion(
                        !ExisteItemEnGrid(txtCodigoProducto.Text.ToString()),
                        "El producto " + txtCodigoProducto.Text.ToString() + " ya fue agregado");

            return validacionService.Validar(ref msj);
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
            var codigoVenta = ventaService.GuardarVenta(txtPrecioTotal.Text);
            try
            {
                for (int i = 0;i < grdVentas.Rows.Count;++i)
                {
                    var producto = grdVentas.Rows[i].Cells[5].Value.ToString();
                    var cantidad = grdVentas.Rows[i].Cells[2].Value.ToString();
                    var precio = grdVentas.Rows[i].Cells[4].Value.ToString();

                    ventaService.GuardarItem(codigoVenta, int.Parse(producto), decimal.Parse(precio), int.Parse(cantidad));

                    if (stockService.HayQueReponer(producto))
                    {
                        alertaService.EmitirAlertaDeReposicion(producto);
                        menu.CargarCantidadDeAlertas();
                    }
                }
            }
            catch
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
                    var reporte = new ReporteVenta(codigoVenta.ToString());
                    reporte.ShowDialog();
                }
                catch
                {
                    throw new Exception("Hubo un error al querer imprimir la venta");
                }
            }
        }
    }
}