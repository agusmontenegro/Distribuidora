using Distribuidora.Commons;
using Distribuidora.Factories;
using Distribuidora.Services;
using System;
using System.Windows.Forms;

namespace Distribuidora
{
    public partial class Stock : Form
    {
        private int celda = -1;

        private readonly Menu menu;
        private readonly FormsCommon formsCommon;
        private readonly ValidacionService validacionService;
        private readonly ProductoService productoService;
        private readonly ComboService comboService;
        private readonly StockService stockService;
        private readonly AlertaService alertaService;

        public Stock(Menu menu)
        {
            InitializeComponent();
            this.menu = menu;
            formsCommon = FormsCommonFactory.Crear();
            validacionService = ValidacionServiceFactory.Crear();
            productoService = ProductoServiceFactory.Crear();
            comboService = ComboServiceFactory.Crear();
            stockService = StockServiceFactory.Crear();
            alertaService = AlertaServiceFactory.Crear();
        }

        private void Stock_Load(object sender, EventArgs e)
        {
            txtDetalleProducto.Enabled = false;
            txtCantidadActual.Enabled = false;
            txtPtoReposicion.Enabled = false;
            txtFechaUltimaReposicion.Enabled = false;
            txtCantidadReponer.Enabled = false;
            btnRealizarReposicionStock.Enabled = false;
            btnConfirmar.Enabled = false;
            btnCancelar.Enabled = false;
        }

        private void txtCodigoProducto_KeyPress(object sender, KeyPressEventArgs e)
        {
            formsCommon.OnlyNumerics(sender, e);

            if (e.KeyChar == (char)Keys.Return)
            {
                string msj = string.Empty;

                if (ProductoValido(ref msj))
                {
                    CompletarDatos(txtCodigoProducto.Text);
                    txtCantidadReponer.Focus();
                }
                else
                {
                    MessageBox.Show(msj);
                }
            }
        }

        private bool ProductoValido(ref string msj)
        {
            validacionService.AgregarValidacion(
                productoService.CodigoProductoValido(txtCodigoProducto.Text, ref msj),
                msj);

            validacionService.AgregarValidacion(
                !comboService.EsCombo(txtCodigoProducto.Text),
                "No está permitido reponer stock de un combo, si de sus componentes");

            return validacionService.Validar(ref msj);
        }

        private void CompletarDatos(string codigo_producto)
        {
            var producto = productoService.ObtenerProducto(codigo_producto);

            txtCodigoProducto.Enabled = false;
            txtDetalleProducto.Enabled = false;
            txtCantidadActual.Enabled = false;
            txtPtoReposicion.Enabled = false;
            txtFechaUltimaReposicion.Enabled = false;
            txtCantidadReponer.Enabled = true;
            btnConfirmar.Enabled = true;
            btnCancelar.Enabled = true;
            txtDetalleProducto.Text = producto.Detalle;
            txtCantidadActual.Text = producto.Stock.CantidadActual;
            txtPtoReposicion.Text = producto.Stock.CantidadMinima;
            txtFechaUltimaReposicion.Text = producto.Stock.UltimaReposicion;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            txtCodigoProducto.Focus();
        }

        private void LimpiarFormulario()
        {
            txtCodigoProducto.Text = string.Empty;
            txtDetalleProducto.Text = string.Empty;
            txtCantidadActual.Text = string.Empty;
            txtPtoReposicion.Text = string.Empty;
            txtCantidadReponer.Text = string.Empty;
            txtFechaUltimaReposicion.Text = string.Empty;
            txtCodigoProducto.Enabled = true;
            txtDetalleProducto.Enabled = false;
            txtCantidadActual.Enabled = false;
            txtPtoReposicion.Enabled = false;
            txtCantidadReponer.Enabled = false;
            txtFechaUltimaReposicion.Enabled = false;
            btnCancelar.Enabled = false;
            btnConfirmar.Enabled = false;
        }

        private void txtCantidadReponer_KeyPress(object sender, KeyPressEventArgs e)
        {
            formsCommon.OnlyNumerics(sender, e);

            if (e.KeyChar == (char)Keys.Return)
            {
                AsignarAGrid();
            }
        }

        private void AsignarAGrid()
        {
            string msj = string.Empty;

            if (ItemValido(ref msj))
            {
                formsCommon.AsignarAGrid(
                    grdStock,
                    txtCodigoProducto.Text,
                    txtDetalleProducto.Text,
                    txtCantidadActual.Text,
                    txtCantidadReponer.Text);

                LimpiarFormulario();
                txtCodigoProducto.Focus();
                btnRealizarReposicionStock.Enabled = true;
            }
            else
            {
                MessageBox.Show(msj);
                txtCantidadReponer.Focus();
            }
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            AsignarAGrid();
        }

        private void btnEliminarItem_Click(object sender, EventArgs e)
        {
            if (celda != -1)
            {
                grdStock.Rows.RemoveAt(celda);
                celda = -1;
            }

            btnRealizarReposicionStock.Enabled = grdStock.Rows.Count > 0;
        }

        private void grdStock_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            celda = e.RowIndex;
        }

        private bool ItemValido(ref string msj)
        {

            if (string.IsNullOrEmpty(txtCantidadReponer.Text))
            {
                validacionService.AgregarValidacion(
                    false,
                    "Debe ingresar la cantidad a reponer");
            }
            else
            {
                validacionService.AgregarValidacion(
                    int.Parse(txtCantidadReponer.Text) > 0,
                    "La cantidad a reponer no puede ser 0");

                validacionService.AgregarValidacion(
                    !ExisteItemEnGrid(txtCodigoProducto.Text.ToString()),
                    "El producto " + txtCodigoProducto.Text.ToString() + " ya fue agregado");
            }

            return validacionService.Validar(ref msj);
        }

        private bool ExisteItemEnGrid(string codigoProducto)
        {
            foreach (DataGridViewRow row in grdStock.Rows)
            {
                if (row.Cells[0].Value.ToString() == codigoProducto)
                {
                    return true;
                }
            }

            return false;
        }

        private void btnRealizarReposicionStock_Click(object sender, EventArgs e)
        {
            ReponerStock();
        }

        private void ReponerStock()
        {
            try
            {
                for (int i = 0;i < grdStock.Rows.Count;++i)
                {
                    var codigo_producto = grdStock.Rows[i].Cells[0].Value.ToString();
                    var cantidad_a_reponer = grdStock.Rows[i].Cells[3].Value.ToString();

                    stockService.ReponerStock(codigo_producto, cantidad_a_reponer);

                    if (!productoService.HayQueReponer(codigo_producto))
                    {
                        alertaService.QuitarAlertaDeReposicion(codigo_producto);
                        menu.CargarCantidadDeAlertas();
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("Hubo un error al intentar reponer stock");
            }

            MessageBox.Show("La reposición de stock se ha realizado con éxito.");
            LimpiarFormulario();
            txtCodigoProducto.Focus();
            grdStock.Rows.Clear();
            btnConfirmar.Enabled = false;

            DialogResult dialogResult = MessageBox.Show(
                "¿Desea imprimir el comprobante de reposición de stock?",
                "Imprimir información de stock",
                MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    // Imprimir
                }
                catch (Exception)
                {
                    throw new Exception("Hubo un error al querer imprimir la reposición de stock");
                }
            }
        }
    }
}