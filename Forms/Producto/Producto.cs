using Distribuidora.Commons;
using Distribuidora.DTOs;
using Distribuidora.Services;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Distribuidora.Forms.Producto
{
    public partial class Producto : Form
    {
        int celda = 0;
        string idProduct;
        string idComponente;

        private readonly RubroService rubroService;
        private readonly ProductoService productoService;
        private readonly ComboService comboService;
        private readonly FormsCommon formsCommon;
        private readonly ValidacionService validacionService;
        private readonly AlertaService alertaService;
        private readonly StockService stockService;
        private readonly Menu menu;

        public Producto(Menu menu, string idProduct = null)
        {
            InitializeComponent();
            this.idProduct = idProduct;
            this.menu = menu;
            rubroService = new RubroService();
            productoService = new ProductoService();
            comboService = new ComboService();
            formsCommon = new FormsCommon();
            validacionService = new ValidacionService();
            alertaService = new AlertaService();
            stockService = new StockService();
        }

        private void AltaProducto_Load(object sender, EventArgs e)
        {
            txtDetalleProductoComposicion.Enabled = false;
            txtCantidadComposicion.Enabled = false;
            btnAgregarComponente.Enabled = false;
            CargarCombos();

            if (!string.IsNullOrEmpty(idProduct))
            {
                CargarDatosAlFormulario(idProduct);
            }
        }

        private void CargarCombos()
        {
            cboRubros.Items.AddRange(rubroService.ObtenerRubros().ToArray());
            cboRubros.DisplayMember = "Detalle";
            cboRubros.ValueMember = "Codigo";
        }

        private void CargarDatosAlFormulario(string idProduct)
        {
            var producto = productoService.ObtenerProductoPorId(idProduct);

            txtCodigoProducto.Text = producto.Codigo;
            txtDetalleProducto.Text = producto.Detalle;
            txtPrecioUnitario.Text = producto.PrecioUnitario.ToString();
            cboRubros.Text = producto.Rubro.Detalle;
            txtStockMinimo.Text = producto.Stock.CantidadMinima;

            var combo = comboService.ObtenerCombo(idProduct);

            foreach (var componente in combo.Componentes)
            {
                int rowId = grdComponentes.Rows.Add();

                grdComponentes.Rows[rowId].Cells[0].Value = componente.Producto.Codigo;
                grdComponentes.Rows[rowId].Cells[1].Value = componente.Producto.Detalle;
                grdComponentes.Rows[rowId].Cells[2].Value = componente.Cantidad;
                grdComponentes.Rows[rowId].Cells[3].Value = componente.Producto.Id;
            }
        }

        private void txtPrecioUnitario_KeyPress(object sender, KeyPressEventArgs e)
        {
            formsCommon.OnlyNumerics(sender, e);
        }

        private void txtStockMinimo_KeyPress(object sender, KeyPressEventArgs e)
        {
            formsCommon.OnlyNumerics(sender, e);
        }

        private void txtCantidadComposicion_KeyPress(object sender, KeyPressEventArgs e)
        {
            formsCommon.OnlyNumerics(sender, e);

            if (e.KeyChar == (char)Keys.Return)
            {
                AsignarAGrid();
            }
        }

        private void txtCodigoProductoComposicion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                var codigoProducto = txtCodigoProductoComposicion.Text;
                string msj = string.Empty;

                if (CodigoProductoValido(codigoProducto, ref msj))
                {
                    CompletarItem(codigoProducto);
                    txtCantidadComposicion.Focus();
                    btnAgregarComponente.Enabled = true;
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

        private void CompletarItem(string codigoProducto)
        {
            var producto = productoService.ObtenerProductosPorCodigo(codigoProducto)[0];

            idComponente = producto.Id;
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
                formsCommon.AsignarAGrid(
                    grdComponentes,
                    txtCodigoProductoComposicion.Text,
                    txtDetalleProductoComposicion.Text,
                    txtCantidadComposicion.Text,
                    idComponente);

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
            if (string.IsNullOrEmpty(txtCantidadComposicion.Text))
            {
                validacionService.AgregarValidacion(
                    false,
                    "Debe ingresar la cantidad del componente");
            }
            else
            {
                validacionService.AgregarValidacion(
                    int.Parse(txtCantidadComposicion.Text) > 0,
                    "La cantidad del componente no puede ser 0");

                validacionService.AgregarValidacion(
                    !ExisteItemEnGrid(txtCodigoProductoComposicion.Text.ToString()),
                    "El producto " + txtCodigoProductoComposicion.Text.ToString() + " ya fue agregado");
            }

            return validacionService.Validar(ref msj);
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
            idComponente = string.Empty;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string msj = string.Empty;

            if (DatosValidos(ref msj))
            {
                try
                {
                    if (string.IsNullOrEmpty(idProduct))
                    {
                        var productosSimilares = productoService.ObtenerProductosSimilares(txtDetalleProducto.Text.ToUpper().Trim());

                        if (productosSimilares.Count > 0)
                        {
                            var infoProductos = string.Empty;
                            productosSimilares.ForEach(p => infoProductos += p.Codigo + " - " + p.Detalle + "\n");

                            DialogResult dialogResult = MessageBox.Show(
                                "Ya hay productos similares guardados anteriormente:\n" + infoProductos +
                                "\n¿Desea guardar este producto de todos modos?",
                                "Existen productos similares",
                                MessageBoxButtons.YesNo);

                            if (dialogResult == DialogResult.Yes)
                            {
                                GuardarProducto();
                            }
                        }
                        else
                        {
                            GuardarProducto();
                        }
                    }
                    else
                    {
                        productoService.ActualizarProducto(
                            idProduct,
                            txtCodigoProducto.Text.ToUpper().Trim(),
                            txtDetalleProducto.Text.ToUpper().Trim(),
                            txtPrecioUnitario.Text,
                            ((Rubro)cboRubros.SelectedItem).Codigo,
                            txtStockMinimo.Text);

                        if (grdComponentes.Rows.Count > 0)
                        {
                            comboService.EliminarComponentes(idProduct);
                            for (int i = 0;i < grdComponentes.Rows.Count;++i)
                            {
                                comboService.GuardarComponente(
                                    int.Parse(idProduct),
                                    grdComponentes.Rows[i].Cells[3].Value.ToString(),
                                    grdComponentes.Rows[i].Cells[2].Value.ToString());
                            }
                        }

                        MessageBox.Show("El producto ha sido actualizado exitosamente");
                        VerificarAlertas(idProduct);
                        Close();
                    }
                }
                catch
                {
                    throw new Exception("Hubo un error al intentar guardar el producto");
                }
            }
            else
            {
                MessageBox.Show(msj);
            }
        }

        private void VerificarAlertas(string idProduct)
        {
            if (!comboService.EsCombo_Id(idProduct))
            {
                if (stockService.HayQueReponer(idProduct))
                    alertaService.EmitirAlertaDeReposicion(idProduct);
                else
                    alertaService.QuitarAlertaDeReposicion(idProduct);

                menu.CargarCantidadDeAlertas();
            }
        }

        private void GuardarProducto()
        {
            var idProducto = productoService.GuardarProducto(
                                    txtCodigoProducto.Text.ToUpper().Trim(),
                                    txtDetalleProducto.Text.ToUpper().Trim(),
                                    txtPrecioUnitario.Text,
                                    ((Rubro)cboRubros.SelectedItem).Codigo,
                                    txtStockMinimo.Text);

            if (grdComponentes.Rows.Count > 0)
            {
                for (int i = 0;i < grdComponentes.Rows.Count;++i)
                {
                    comboService.GuardarComponente(
                        idProducto,
                        grdComponentes.Rows[i].Cells[3].Value.ToString(),
                        grdComponentes.Rows[i].Cells[2].Value.ToString());
                }
            }
            MessageBox.Show("El producto ha sido guardado exitosamente");
            VerificarAlertas(idProducto.ToString());
            Close();
        }

        private bool DatosValidos(ref string msj)
        {
            if (!string.IsNullOrEmpty(txtCodigoProducto.Text.Trim()))
            {
                validacionService.AgregarValidacion(
                    txtCodigoProducto.Text.Trim().Count() < 5,
                    "El código del producto no debe superar los 5 caracteres");

                if (string.IsNullOrEmpty(idProduct))
                {
                    validacionService.AgregarValidacion(
                        !productoService.ExisteProductoSegunCodigo(txtCodigoProducto.Text.ToUpper().Trim()),
                        "Ya existe otro producto con el mismo código ingresado.");
                }
                else
                {
                    var products = productoService.ObtenerProductosPorCodigo(txtCodigoProducto.Text.ToUpper().Trim());

                    if (products != null)
                    {
                        validacionService.AgregarValidacion(
                            products.Count() == 1 && products[0].Id == idProduct,
                            "Ya existe otro producto con el mismo código ingresado.");
                    }
                }
            }
            else
            {
                validacionService.AgregarValidacion(
                    false,
                    "Debe ingresar el código del producto");
            }

            validacionService.AgregarValidacion(
                !string.IsNullOrEmpty(txtDetalleProducto.Text.Trim()),
                "Debe ingresar el detalle del producto");

            validacionService.AgregarValidacion(
                !string.IsNullOrEmpty(txtPrecioUnitario.Text),
                "Debe ingresar el precio unitario del producto");

            validacionService.AgregarValidacion(
                cboRubros.SelectedIndex != -1,
                "Debe seleccionar el rubro del producto");

            validacionService.AgregarValidacion(
                !string.IsNullOrEmpty(txtStockMinimo.Text),
                "Debe ingresar el punto de reposición de stock");

            return validacionService.Validar(ref msj);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarFormularioDeComponentes();

            btnAgregarComponente.Enabled = true;
            txtCodigoProductoComposicion.Focus();
        }
    }
}