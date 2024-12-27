using Logica.Services.Alerta;
using Logica.Services.Combo;
using Logica.Services.Producto;
using Logica.Services.Stock;
using Logica.Services.Validacion;
using Logica.Services.Venta;
using Presentacion.Forms.Factory.ReporteVenta;
using Presentacion.Forms.Observer;
using System;
using System.Transactions;
using System.Windows.Forms;

namespace Presentacion.Forms.Venta
{
    public partial class Venta : Form
    {
        private Persistencia.DTOs.Stock Stock;
        private int Celda = -1;
        private string IdProducto;

        private readonly IProductoService productoService;
        private readonly IComboService comboService;
        private readonly IValidacionService validacionService;
        private readonly IAlertaService alertaService;
        private readonly IVentaService ventaService;
        private readonly IStockService stockService;

        private readonly IPublisherAlerta publisherAlerta;

        private readonly IReporteVentaFormFactory reporteVentaFormFactory;

        public Venta(ReporteVenta reporteVenta,
            IProductoService productoService,
            IComboService comboService,
            IValidacionService validacionService,
            IAlertaService alertaService,
            IVentaService ventaService,
            IStockService stockService,
            IPublisherAlerta publisherAlerta,
            IReporteVentaFormFactory reporteVentaFormFactory)
        {
            InitializeComponent();
            this.productoService = productoService;
            this.comboService = comboService;
            this.validacionService = validacionService;
            this.alertaService = alertaService;
            this.ventaService = ventaService;
            this.stockService = stockService;
            this.publisherAlerta = publisherAlerta;
            this.reporteVentaFormFactory = reporteVentaFormFactory;
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
                validacionService.AgregarValidacion(false, "Debe ingresar un código de producto.");
            }
            else
            {
                var existeProd = productoService.ExisteProductoSegunCodigo(codigoProducto);
                validacionService.AgregarValidacion(existeProd, "No existe un producto activo con el código ingresado.");
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
            IdProducto = string.Empty;
        }

        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCantidad.Text))
            {
                var cantidad = int.Parse(txtCantidad.Text);
                var precioUnitario = decimal.Parse(txtPrecioUnitario.Text);
                var subtotal = cantidad * precioUnitario;

                txtSubtotal.Text = subtotal.ToString();

                if (!comboService.EsCombo_Id(IdProducto))
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
            OnlyNumerics(sender, e);

            if (e.KeyChar == (char)Keys.Return)
            {
                AsignarAGrid();
            }
        }

        private void OnlyNumerics(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
                e.Handled = true;
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
                e.Handled = true;
        }

        private void CompletarItem(string codigoProducto)
        {
            var producto = productoService.ObtenerProductosPorCodigo(codigoProducto)[0];

            IdProducto = producto.Id;
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
            if (!comboService.EsCombo_Id(IdProducto))
            {
                var producto = productoService.ObtenerProductoPorId(IdProducto);

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
                var index = grdVentas.Rows.Add();
                grdVentas.Rows[index].Cells[0].Value = txtCodigoProducto.Text;
                grdVentas.Rows[index].Cells[1].Value = txtDetalleProducto.Text;
                grdVentas.Rows[index].Cells[2].Value = txtCantidad.Text;
                grdVentas.Rows[index].Cells[3].Value = txtSubtotal.Text;
                grdVentas.Rows[index].Cells[4].Value = txtPrecioUnitario.Text;
                grdVentas.Rows[index].Cells[5].Value = IdProducto;
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
                validacionService.AgregarValidacion(false, "Debe ingresar la cantidad del producto a vender");
            }
            else
            {
                validacionService.AgregarValidacion(int.Parse(txtCantidad.Text) > 0, "La cantidad del producto a vender no puede ser 0");
                if (comboService.EsCombo_Id(IdProducto))
                {
                    if (!stockService.HayStock(IdProducto, txtCantidad.Text))
                    {
                        var stockInfo = comboService.InformarStockFaltante(IdProducto, txtCantidad.Text);
                        validacionService.AgregarValidacion(false, "Stock no disponible para la cantidad ingresada. \n" + stockInfo);
                    }
                }
                else
                {
                    validacionService.AgregarValidacion(int.Parse(txtCantidad.Text) <= int.Parse(txtStockActual.Text), "Stock no disponible para la cantidad ingresada");
                }
            }

            var existeEnGrid = ExisteItemEnGrid(txtCodigoProducto.Text.ToString());
            validacionService.AgregarValidacion(!existeEnGrid, "El producto " + txtCodigoProducto.Text.ToString() + " ya fue agregado");

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
            if (Celda != -1)
            {
                var subtotal = decimal.Parse(grdVentas.Rows[Celda].Cells[3].Value.ToString());
                var totalActual = decimal.Parse(txtPrecioTotal.Text);
                var nuevoSubtotal = totalActual - subtotal;

                grdVentas.Rows.RemoveAt(Celda);
                txtPrecioTotal.Text = nuevoSubtotal.ToString();
                Celda = -1;
            }

            btnGuardarVenta.Enabled = grdVentas.Rows.Count > 0;
        }

        private void grdVentas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Celda = e.RowIndex;
        }

        private void btnGuardarVenta_Click(object sender, EventArgs e)
        {
            int codigoVenta = 0;
            using (var scope = new TransactionScope())
            {
                try
                {
                    codigoVenta = ventaService.GuardarVenta(txtPrecioTotal.Text);
                    for (int i = 0;i < grdVentas.Rows.Count;++i)
                    {
                        var idProducto = grdVentas.Rows[i].Cells[5].Value.ToString();
                        var cantidad = grdVentas.Rows[i].Cells[2].Value.ToString();
                        var precio = grdVentas.Rows[i].Cells[4].Value.ToString();

                        ventaService.GuardarItem(codigoVenta, int.Parse(idProducto), decimal.Parse(precio), int.Parse(cantidad));
                        alertaService.ActualizarAlertaDeReposicion(idProducto);
                        Notificar(idProducto);
                    }
                    scope.Complete();
                    MessageBox.Show("La venta ha sido procesada con éxito");
                    LimpiarFormulario();
                    txtCodigoProducto.Focus();
                    grdVentas.Rows.Clear();
                    btnGuardarVenta.Enabled = false;
                    txtPrecioTotal.Text = string.Empty;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hubo un error al intentar procesar la venta " + ex.Message);
                }
            }


            var dialogResult = MessageBox.Show("¿Desea imprimir el comprobante de venta?", "Imprimir venta", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    var reporteVentaForm = reporteVentaFormFactory.CrearReporteVenta(codigoVenta.ToString());
                    reporteVentaForm.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hubo un error al querer imprimir la venta " + ex.Message);
                }
            }
        }

        private void Notificar(string idProducto)
        {
            publisherAlerta.Notificar(idProducto);
        }
    }
}