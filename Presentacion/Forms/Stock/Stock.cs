using Logica.Services.Alerta;
using Logica.Services.Combo;
using Logica.Services.Producto;
using Logica.Services.Stock;
using Logica.Services.Validacion;
using Presentacion.Forms.Factory.ReporteStock;
using Presentacion.Forms.Observer;
using System;
using System.Transactions;
using System.Windows.Forms;

namespace Presentacion.Forms.Stock
{
    public partial class Stock : Form
    {
        private int Celda = -1;
        private string IdProducto;

        private readonly IValidacionService validacionService;
        private readonly IProductoService productoService;
        private readonly IComboService comboService;
        private readonly IStockService stockService;
        private readonly IAlertaService alertaService;

        private readonly IPublisherAlerta publisherAlerta;

        private readonly IReporteStockFormFactory reporteStockFormFactory;

        public Stock(IValidacionService validacionService,
            IProductoService productoService,
            IComboService comboService,
            IStockService stockService,
            IAlertaService alertaService,
            IPublisherAlerta publisherAlerta,
            IReporteStockFormFactory reporteStockFormFactory)
        {
            InitializeComponent();
            this.validacionService = validacionService;
            this.productoService = productoService;
            this.comboService = comboService;
            this.stockService = stockService;
            this.alertaService = alertaService;
            this.publisherAlerta = publisherAlerta;
            this.reporteStockFormFactory = reporteStockFormFactory;
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
            if (e.KeyChar == (char)Keys.Return)
            {
                string msj = string.Empty;

                if (ProductoValido(ref msj))
                {
                    CompletarDatos(txtCodigoProducto.Text.ToUpper().Trim());
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
            if (string.IsNullOrEmpty(txtCodigoProducto.Text))
            {
                validacionService.AgregarValidacion(false, "Debe ingresar un código de producto.");
            }
            else
            {
                var existeProd = productoService.ExisteProductoSegunCodigo(txtCodigoProducto.Text.ToUpper().Trim());
                var esCombo = comboService.EsCombo_Codigo(txtCodigoProducto.Text.ToUpper().Trim());
                validacionService.AgregarValidacion(existeProd, "No existe un producto activo con el código ingresado.");
                validacionService.AgregarValidacion(!esCombo, "No está permitido reponer stock de un combo. Reponga stock de cada uno de sus componentes");
            }

            return validacionService.Validar(ref msj);
        }

        private void CompletarDatos(string codigoProducto)
        {
            var producto = productoService.ObtenerProductosPorCodigo(codigoProducto)[0];

            IdProducto = producto.Id;
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
            IdProducto = string.Empty;
        }

        private void txtCantidadReponer_KeyPress(object sender, KeyPressEventArgs e)
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

        private void AsignarAGrid()
        {
            string msj = string.Empty;

            if (ItemValido(ref msj))
            {
                grdStock.Rows.Add(txtCodigoProducto.Text, txtDetalleProducto.Text, txtCantidadActual.Text, txtCantidadReponer.Text, IdProducto);
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
            if (Celda != -1)
            {
                grdStock.Rows.RemoveAt(Celda);
                Celda = -1;
            }

            btnRealizarReposicionStock.Enabled = grdStock.Rows.Count > 0;
        }

        private void grdStock_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Celda = e.RowIndex;
        }

        private bool ItemValido(ref string msj)
        {

            if (string.IsNullOrEmpty(txtCantidadReponer.Text))
            {
                validacionService.AgregarValidacion(false, "Debe ingresar la cantidad a reponer");
            }
            else
            {
                var existeEnGrid = ExisteItemEnGrid(txtCodigoProducto.Text.ToString());
                validacionService.AgregarValidacion(int.Parse(txtCantidadReponer.Text) > 0, "La cantidad a reponer no puede ser 0");
                validacionService.AgregarValidacion(!existeEnGrid, "El producto " + txtCodigoProducto.Text.ToString() + " ya fue agregado");
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
            int reposicionCodigo = 0;
            using (var scope = new TransactionScope())
            {
                try
                {
                    reposicionCodigo = stockService.GuardarReposicion();
                    for (int i = 0;i < grdStock.Rows.Count;++i)
                    {
                        var idProducto = grdStock.Rows[i].Cells[4].Value.ToString();
                        var cantidadAReponer = grdStock.Rows[i].Cells[3].Value.ToString();

                        stockService.ReponerStock(reposicionCodigo, idProducto, cantidadAReponer);
                        alertaService.ActualizarAlertaDeReposicion(idProducto);
                        Notificar(idProducto);
                    }

                    scope.Complete();
                    MessageBox.Show("La reposición de stock se ha realizado con éxito.");
                    LimpiarFormulario();
                    txtCodigoProducto.Focus();
                    grdStock.Rows.Clear();
                    btnConfirmar.Enabled = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hubo un error al intentar reponer stock " + ex.Message);
                }
            }

            var dialogResult = MessageBox.Show("¿Desea imprimir el comprobante de reposición de stock?", "Imprimir información de stock", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    var reporteStockForm = reporteStockFormFactory.CrearReporteStock(reposicionCodigo.ToString());
                    reporteStockForm.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hubo un error al querer imprimir la reposición de stock " + ex.Message);
                }
            }
        }

        private void Notificar(string idProducto)
        {
            publisherAlerta.Notificar(idProducto);
        }
    }
}