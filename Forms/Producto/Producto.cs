using Distribuidora.Commons;
using Distribuidora.DTOs;
using Distribuidora.Factories;
using Distribuidora.Services;
using System;
using System.Windows.Forms;

namespace Distribuidora.Producto
{
    public partial class Producto : Form
    {
        int celda = 0;
        string codigoProductoEditar;

        private readonly RubroService rubroService;
        private readonly ProductoService productoService;
        private readonly ComboService comboService;
        private readonly FormsCommon formsCommon;
        private readonly ValidacionService validacionService;

        public Producto(string codigoProductoEditar = "")
        {
            InitializeComponent();
            this.codigoProductoEditar = codigoProductoEditar;
            rubroService = RubroServiceFactory.Crear();
            productoService = ProductoServiceFactory.Crear();
            comboService = ComboServiceFactory.Crear();
            formsCommon = FormsCommonFactory.Crear();
            validacionService = ValidacionServiceFactory.Crear();
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
            cboRubros.Items.AddRange(rubroService.ObtenerRubros().ToArray());
            cboRubros.DisplayMember = "Detalle";
            cboRubros.ValueMember = "Codigo";
        }

        private void CargarDatosAlFormulario(string codigoProducto)
        {
            var producto = productoService.ObtenerProducto(codigoProducto);

            txtDetalleProducto.Text = producto.Detalle;
            txtPrecioUnitario.Text = producto.PrecioUnitario.ToString();
            cboRubros.Text = producto.Rubro.Detalle;
            txtStockMinimo.Text = producto.Stock.CantidadMinima;

            var combo = comboService.ObtenerCombo(codigoProducto);

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
            formsCommon.OnlyNumerics(sender, e);

            if (e.KeyChar == (char)Keys.Return)
            {
                var codigo_producto = txtCodigoProductoComposicion.Text;
                string msj = string.Empty;

                if (productoService.CodigoProductoValido(codigo_producto, ref msj))
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
            var producto = productoService.ObtenerProducto(codigo_producto);

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
                    txtCantidadComposicion.Text);

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
                        productoService.ActualizarProducto(
                            codigoProductoEditar, txtDetalleProducto.Text, txtPrecioUnitario.Text, ((Rubro)cboRubros.SelectedItem).Codigo, txtStockMinimo.Text);

                        if (grdComponentes.Rows.Count > 0)
                        {
                            comboService.EliminarComponentes(codigoProductoEditar);
                            for (int i = 0;i < grdComponentes.Rows.Count;++i)
                            {
                                comboService.GuardarComponente(
                                    int.Parse(codigoProductoEditar), grdComponentes.Rows[i].Cells[0].Value.ToString(), grdComponentes.Rows[i].Cells[2].Value.ToString());
                            }
                        }
                    }
                    else // insert
                    {
                        var codigoProducto = productoService.GuardarProducto(
                            txtDetalleProducto.Text, txtPrecioUnitario.Text, ((Rubro)cboRubros.SelectedItem).Codigo, txtStockMinimo.Text);

                        if (grdComponentes.Rows.Count > 0)
                        {
                            for (int i = 0;i < grdComponentes.Rows.Count;++i)
                            {
                                comboService.GuardarComponente(
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
            validacionService.AgregarValidacion(
                !string.IsNullOrEmpty(txtDetalleProducto.Text),
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