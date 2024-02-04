using Distribuidora.Forms.Helpers;
using Logica.Services;
using Persistencia.DTOs;
using Presentacion.Commons;
using System;
using System.Linq;
using System.Transactions;
using System.Windows.Forms;

namespace Presentacion.Forms.Producto
{
    public partial class Producto : Form
    {
        private int Celda = 0;
        private string IdProduct;
        private string IdComponente;

        private readonly RubroService RubroService;
        private readonly ProductoService ProductoService;
        private readonly ComboService ComboService;
        private readonly ValidacionService ValidacionService;
        private readonly AlertaService AlertaService;
        private readonly StockService StockService;

        private readonly FormsCommon FormsCommon;
        private readonly ProductoFormHelper ProductoFormHelper;

        private readonly Menu Menu;

        public Producto(Menu Menu, string IdProduct = null)
        {
            InitializeComponent();

            this.IdProduct = IdProduct;
            this.Menu = Menu;

            RubroService = new RubroService();
            ProductoService = new ProductoService();
            ComboService = new ComboService();
            ValidacionService = new ValidacionService();
            AlertaService = new AlertaService();
            StockService = new StockService();

            FormsCommon = new FormsCommon();
            ProductoFormHelper = new ProductoFormHelper();
        }

        private void AltaProducto_Load(object sender, EventArgs e)
        {
            txtDetalleProductoComposicion.Enabled = false;
            txtCantidadComposicion.Enabled = false;
            btnAgregarComponente.Enabled = false;
            CargarCombos();

            if (!string.IsNullOrEmpty(IdProduct))
            {
                CargarDatosAlFormulario(IdProduct);
            }
        }

        private void CargarCombos()
        {
            var rubros = RubroService.ObtenerRubros();
            cboRubros.Items.AddRange(rubros.ToArray());
            cboRubros.DisplayMember = "Detalle";
            cboRubros.ValueMember = "Codigo";
        }

        private void CargarDatosAlFormulario(string idProduct)
        {
            var producto = ProductoService.ObtenerProductoPorId(idProduct);
            var combo = ComboService.ObtenerCombo(idProduct);
            ProductoFormHelper.CargarDatosAlFormulario(this, producto);
            ProductoFormHelper.CargarDatosALaGrilla(this, combo);
        }

        private void txtPrecioUnitario_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormsCommon.OnlyNumerics(sender, e);
        }

        private void txtStockMinimo_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormsCommon.OnlyNumerics(sender, e);
        }

        private void txtCantidadComposicion_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormsCommon.OnlyNumerics(sender, e);

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
                ValidacionService.AgregarValidacion(false, "Debe ingresar un código de producto.");
            }
            else
            {
                var existeProd = ProductoService.ExisteProductoSegunCodigo(codigoProducto);
                ValidacionService.AgregarValidacion(existeProd, "No existe un producto activo con el código ingresado.");
            }

            return ValidacionService.Validar(ref msj);
        }

        private void CompletarItem(string codigoProducto)
        {
            var producto = ProductoService.ObtenerProductosPorCodigo(codigoProducto)[0];

            IdComponente = producto.Id;
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
                grdComponentes.Rows.Add(txtCodigoProductoComposicion.Text, txtDetalleProductoComposicion.Text, txtCantidadComposicion.Text, IdComponente);
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
                ValidacionService.AgregarValidacion(false, "Debe ingresar la cantidad del componente");
            }
            else
            {
                var existeItemEnGrid = ProductoFormHelper.ExisteItemEnGrid(this, txtCodigoProductoComposicion.Text.ToString());
                ValidacionService.AgregarValidacion(int.Parse(txtCantidadComposicion.Text) > 0, "La cantidad del componente no puede ser 0");
                ValidacionService.AgregarValidacion(!existeItemEnGrid, "El producto " + txtCodigoProductoComposicion.Text.ToString() + " ya fue agregado");
            }

            return ValidacionService.Validar(ref msj);
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
            IdComponente = string.Empty;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string msj = string.Empty;

            if (DatosValidos(ref msj))
            {
                using (var scope = new TransactionScope())
                {
                    try
                    {
                        if (string.IsNullOrEmpty(IdProduct))
                        {
                            var productosSimilares = ProductoService.ObtenerProductosSimilares(txtDetalleProducto.Text.ToUpper().Trim());

                            if (productosSimilares.Count > 0)
                            {
                                var infoProductos = string.Empty;
                                productosSimilares.ForEach(p => infoProductos += p.Codigo + " - " + p.Detalle + "\n");

                                var dialogResult = MessageBox.Show(
                                    "Ya hay productos similares guardados anteriormente:\n" + infoProductos +
                                    "\n¿Desea guardar este producto de todos modos?",
                                    "Existen productos similares",
                                    MessageBoxButtons.YesNo);

                                if (dialogResult == DialogResult.Yes)
                                    GuardarProducto();
                            }
                            else
                                GuardarProducto();
                        }
                        else
                        {
                            var codigoProd = txtCodigoProducto.Text.ToUpper().Trim();
                            var detalleProd = txtDetalleProducto.Text.ToUpper().Trim();
                            var precio = txtPrecioUnitario.Text;
                            var codigoRubro = ((Rubro)cboRubros.SelectedItem).Codigo;
                            var stockMinimo = txtStockMinimo.Text;

                            ProductoService.ActualizarProducto(IdProduct, codigoProd, detalleProd, precio, codigoRubro, stockMinimo);

                            if (grdComponentes.Rows.Count > 0)
                            {
                                ComboService.EliminarComponentes(IdProduct);
                                for (int i = 0;i < grdComponentes.Rows.Count;++i)
                                {
                                    var idComponente = grdComponentes.Rows[i].Cells[3].Value.ToString();
                                    var cantidad = grdComponentes.Rows[i].Cells[2].Value.ToString();
                                    ComboService.GuardarComponente(int.Parse(IdProduct), idComponente, cantidad);
                                }
                            }

                            VerificarAlertas(IdProduct);
                        }
                        scope.Complete();
                        MessageBox.Show("El producto ha sido actualizado exitosamente");
                        Close();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Hubo un error al intentar guardar el producto " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show(msj);
            }
        }

        private void VerificarAlertas(string idProduct)
        {
            if (!ComboService.EsCombo_Id(idProduct))
            {
                if (StockService.HayQueReponer(idProduct))
                    AlertaService.EmitirAlertaDeReposicion(idProduct);
                else
                    AlertaService.QuitarAlertaDeReposicion(idProduct);

                Menu.CargarCantidadDeAlertas();
            }
        }

        private void GuardarProducto()
        {
            var codigoProd = txtCodigoProducto.Text.ToUpper().Trim();
            var detalleProd = txtDetalleProducto.Text.ToUpper().Trim();
            var precio = txtPrecioUnitario.Text;
            var codigoRubro = ((Rubro)cboRubros.SelectedItem).Codigo;
            var stockMinimo = txtStockMinimo.Text;

            var idProducto = ProductoService.GuardarProducto(codigoProd, detalleProd, precio, codigoRubro, stockMinimo);

            if (grdComponentes.Rows.Count > 0)
            {
                for (int i = 0;i < grdComponentes.Rows.Count;++i)
                {
                    var idComponente = grdComponentes.Rows[i].Cells[3].Value.ToString();
                    var cantidad = grdComponentes.Rows[i].Cells[2].Value.ToString();
                    ComboService.GuardarComponente(idProducto, idComponente, cantidad);
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
                ValidacionService.AgregarValidacion(txtCodigoProducto.Text.Trim().Count() < 5, "El código del producto no debe superar los 5 caracteres");

                if (string.IsNullOrEmpty(IdProduct))
                {
                    var existeProducto = ProductoService.ExisteProductoSegunCodigo(txtCodigoProducto.Text.ToUpper().Trim());
                    ValidacionService.AgregarValidacion(!existeProducto, "Ya existe otro producto con el mismo código ingresado.");
                }
                else
                {
                    var products = ProductoService.ObtenerProductosPorCodigo(txtCodigoProducto.Text.ToUpper().Trim());

                    if (products != null)
                        ValidacionService.AgregarValidacion(products.Count() == 1 && products[0].Id == IdProduct, "Ya existe otro producto con el mismo código ingresado.");
                }
            }
            else
                ValidacionService.AgregarValidacion(false, "Debe ingresar el código del producto");

            ValidacionService.AgregarValidacion(!string.IsNullOrEmpty(txtDetalleProducto.Text.Trim()), "Debe ingresar el detalle del producto");
            ValidacionService.AgregarValidacion(!string.IsNullOrEmpty(txtPrecioUnitario.Text), "Debe ingresar el precio unitario del producto");
            ValidacionService.AgregarValidacion(cboRubros.SelectedIndex != -1, "Debe seleccionar el rubro del producto");
            ValidacionService.AgregarValidacion(!string.IsNullOrEmpty(txtStockMinimo.Text), "Debe ingresar el punto de reposición de stock");

            return ValidacionService.Validar(ref msj);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (Celda != -1)
            {
                grdComponentes.Rows.RemoveAt(Celda);
                Celda = -1;
            }
        }

        private void grdComponentes_CellClick(object sender, DataGridViewCellEventArgs e) { Celda = e.RowIndex; }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarFormularioDeComponentes();

            btnAgregarComponente.Enabled = false;
            txtCodigoProductoComposicion.Focus();
        }
    }
}