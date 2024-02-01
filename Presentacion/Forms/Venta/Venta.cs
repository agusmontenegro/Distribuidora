using Logica.Services;
using Presentacion.Commons;
using System;
using System.Windows.Forms;

namespace Presentacion.Forms.Venta
{
    public partial class Venta : Form
    {
        private Persistencia.DTOs.Stock Stock;
        private int Celda = -1;
        private string IdProducto;

        private readonly Menu Menu;
        private readonly FormsCommon FormsCommon;
        private readonly ProductoService ProductoService;
        private readonly ComboService ComboService;
        private readonly ValidacionService ValidacionService;
        private readonly AlertaService AlertaService;
        private readonly VentaService VentaService;
        private readonly StockService StockService;

        public Venta(Menu Menu)
        {
            InitializeComponent();
            this.Menu = Menu;
            FormsCommon = new FormsCommon();
            ProductoService = new ProductoService();
            ComboService = new ComboService();
            ValidacionService = new ValidacionService();
            AlertaService = new AlertaService();
            VentaService = new VentaService();
            StockService = new StockService();
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
                ValidacionService.AgregarValidacion(false, "Debe ingresar un código de producto.");
            }
            else
            {
                var existeProd = ProductoService.ExisteProductoSegunCodigo(codigoProducto);
                ValidacionService.AgregarValidacion(existeProd, "No existe un producto activo con el código ingresado.");
            }

            return ValidacionService.Validar(ref msj);
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

                if (!ComboService.EsCombo_Id(IdProducto))
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

        private void CompletarItem(string codigoProducto)
        {
            var producto = ProductoService.ObtenerProductosPorCodigo(codigoProducto)[0];

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
            if (!ComboService.EsCombo_Id(IdProducto))
            {
                var producto = ProductoService.ObtenerProductoPorId(IdProducto);

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
                ValidacionService.AgregarValidacion(false, "Debe ingresar la cantidad del producto a vender");
            }
            else
            {
                ValidacionService.AgregarValidacion(int.Parse(txtCantidad.Text) > 0, "La cantidad del producto a vender no puede ser 0");
                if (ComboService.EsCombo_Id(IdProducto))
                {
                    if (!StockService.HayStock(IdProducto, txtCantidad.Text))
                    {
                        var stockInfo = ComboService.InformarStockFaltante(IdProducto, txtCantidad.Text);
                        ValidacionService.AgregarValidacion(false, "Stock no disponible para la cantidad ingresada. \n" + stockInfo);
                    }
                }
                else
                {
                    ValidacionService.AgregarValidacion(int.Parse(txtCantidad.Text) <= int.Parse(txtStockActual.Text), "Stock no disponible para la cantidad ingresada");
                }
            }

            var existeEnGrid = ExisteItemEnGrid(txtCodigoProducto.Text.ToString());
            ValidacionService.AgregarValidacion(!existeEnGrid, "El producto " + txtCodigoProducto.Text.ToString() + " ya fue agregado");

            return ValidacionService.Validar(ref msj);
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
            var codigoVenta = VentaService.GuardarVenta(txtPrecioTotal.Text);
            try
            {
                for (int i = 0;i < grdVentas.Rows.Count;++i)
                {
                    var producto = grdVentas.Rows[i].Cells[5].Value.ToString();
                    var cantidad = grdVentas.Rows[i].Cells[2].Value.ToString();
                    var precio = grdVentas.Rows[i].Cells[4].Value.ToString();

                    VentaService.GuardarItem(codigoVenta, int.Parse(producto), decimal.Parse(precio), int.Parse(cantidad));

                    if (StockService.HayQueReponer(producto))
                    {
                        AlertaService.EmitirAlertaDeReposicion(producto);
                        Menu.CargarCantidadDeAlertas();
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

            DialogResult dialogResult = MessageBox.Show("¿Desea imprimir el comprobante de venta?", "Imprimir venta", MessageBoxButtons.YesNo);

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