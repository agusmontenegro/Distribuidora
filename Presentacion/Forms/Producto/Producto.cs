﻿using Logica.Services.Combo;
using Logica.Services.Producto;
using Logica.Services.Rubro;
using Logica.Services.Validacion;
using Persistencia.DTOs;
using Presentacion.Forms.Observer;
using System;
using System.Linq;
using System.Transactions;
using System.Windows.Forms;

namespace Presentacion.Forms.Producto
{
    public partial class Producto : Form
    {
        private int Celda = 0;
        private string idProducto;
        private string IdComponente;

        private readonly IRubroService rubroService;
        private readonly IProductoService productoService;
        private readonly IComboService comboService;
        private readonly IValidacionService validacionService;

        //Producto podria implementar publisherAlerta segun diagrama de clases del Observer
        //pero rompe con el principio de responsabilidad unica: ademas de ser un formulario tambien seria un notificador
        //entonces utilizamos el servicio publisher y que este se encargue de notificar (idem Stock y Venta)
        private readonly IPublisherAlerta publisherAlerta;

        public Producto(IRubroService rubroService,
            IProductoService productoService,
            IComboService comboService,
            IValidacionService validacionService,
            IPublisherAlerta publisherAlerta)
        {
            InitializeComponent();
            this.rubroService = rubroService;
            this.productoService = productoService;
            this.comboService = comboService;
            this.validacionService = validacionService;
            this.publisherAlerta = publisherAlerta;
        }

        public void SetIdProducto(string idProducto)
        {
            this.idProducto = idProducto;
        }

        private void AltaProducto_Load(object sender, EventArgs e)
        {
            txtDetalleProductoComposicion.Enabled = false;
            txtCantidadComposicion.Enabled = false;
            btnAgregarComponente.Enabled = false;
            CargarCombos();

            if (!string.IsNullOrEmpty(idProducto))
            {
                CargarDatosAlFormulario(idProducto);
            }
        }

        private void CargarCombos()
        {
            var rubros = rubroService.ObtenerRubros();
            cboRubros.Items.AddRange(rubros.ToArray());
            cboRubros.DisplayMember = "Detalle";
            cboRubros.ValueMember = "Codigo";
        }

        private void CargarDatosAlFormulario(string idProduct)
        {
            var producto = productoService.ObtenerProductoPorId(idProduct);
            var combo = comboService.ObtenerCombo(idProduct);
            CargarDatosAlFormulario(this, producto);
            CargarDatosALaGrilla(this, combo);
        }

        private void OnlyNumerics(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
                e.Handled = true;
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
                e.Handled = true;
        }

        private void txtPrecioUnitario_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnlyNumerics(sender, e);
        }

        private void txtStockMinimo_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnlyNumerics(sender, e);
        }

        private void txtCantidadComposicion_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnlyNumerics(sender, e);

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
                validacionService.AgregarValidacion(false, "Debe ingresar un código de producto.");
            }
            else
            {
                var existeProd = productoService.ExisteProductoSegunCodigo(codigoProducto);
                validacionService.AgregarValidacion(existeProd, "No existe un producto activo con el código ingresado.");
            }

            return validacionService.Validar(ref msj);
        }

        private void CompletarItem(string codigoProducto)
        {
            var producto = productoService.ObtenerProductosPorCodigo(codigoProducto)[0];

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
                validacionService.AgregarValidacion(false, "Debe ingresar la cantidad del componente");
            }
            else
            {
                var existeItemEnGrid = ExisteItemEnGrid(this, txtCodigoProductoComposicion.Text.ToString());
                validacionService.AgregarValidacion(int.Parse(txtCantidadComposicion.Text) > 0, "La cantidad del componente no puede ser 0");
                validacionService.AgregarValidacion(!existeItemEnGrid, "El producto " + txtCodigoProductoComposicion.Text.ToString() + " ya fue agregado");
            }

            return validacionService.Validar(ref msj);
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
                        if (string.IsNullOrEmpty(idProducto))
                        {
                            var productosSimilares = productoService.ObtenerProductosSimilares(txtDetalleProducto.Text.ToUpper().Trim());

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

                            productoService.ActualizarProducto(idProducto, codigoProd, detalleProd, precio, codigoRubro, stockMinimo);

                            if (grdComponentes.Rows.Count > 0)
                            {
                                comboService.EliminarComponentes(idProducto);
                                for (int i = 0;i < grdComponentes.Rows.Count;++i)
                                {
                                    var idComponente = grdComponentes.Rows[i].Cells[3].Value.ToString();
                                    var cantidad = grdComponentes.Rows[i].Cells[2].Value.ToString();
                                    comboService.GuardarComponente(int.Parse(idProducto), idComponente, cantidad);
                                }
                            }

                            NotificarAlertas(idProducto);
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

        private void GuardarProducto()
        {
            var codigoProd = txtCodigoProducto.Text.ToUpper().Trim();
            var detalleProd = txtDetalleProducto.Text.ToUpper().Trim();
            var precio = txtPrecioUnitario.Text;
            var codigoRubro = ((Rubro)cboRubros.SelectedItem).Codigo;
            var stockMinimo = txtStockMinimo.Text;

            var idProducto = productoService.GuardarProducto(codigoProd, detalleProd, precio, codigoRubro, stockMinimo);

            if (grdComponentes.Rows.Count > 0)
            {
                for (int i = 0;i < grdComponentes.Rows.Count;++i)
                {
                    var idComponente = grdComponentes.Rows[i].Cells[3].Value.ToString();
                    var cantidad = grdComponentes.Rows[i].Cells[2].Value.ToString();
                    comboService.GuardarComponente(idProducto, idComponente, cantidad);
                }
            }
            MessageBox.Show("El producto ha sido guardado exitosamente");
            NotificarAlertas(idProducto.ToString());
            Close();
        }

        private bool DatosValidos(ref string msj)
        {
            if (!string.IsNullOrEmpty(txtCodigoProducto.Text.Trim()))
            {
                validacionService.AgregarValidacion(txtCodigoProducto.Text.Trim().Count() < 5, "El código del producto no debe superar los 5 caracteres");

                if (string.IsNullOrEmpty(idProducto))
                {
                    var existeProducto = productoService.ExisteProductoSegunCodigo(txtCodigoProducto.Text.ToUpper().Trim());
                    validacionService.AgregarValidacion(!existeProducto, "Ya existe otro producto con el mismo código ingresado.");
                }
                else
                {
                    var products = productoService.ObtenerProductosPorCodigo(txtCodigoProducto.Text.ToUpper().Trim());

                    if (products != null)
                        validacionService.AgregarValidacion(products.Count() == 1 && products[0].Id == idProducto, "Ya existe otro producto con el mismo código ingresado.");
                }
            }
            else
                validacionService.AgregarValidacion(false, "Debe ingresar el código del producto");

            validacionService.AgregarValidacion(!string.IsNullOrEmpty(txtDetalleProducto.Text.Trim()), "Debe ingresar el detalle del producto");
            validacionService.AgregarValidacion(!string.IsNullOrEmpty(txtPrecioUnitario.Text), "Debe ingresar el precio unitario del producto");
            validacionService.AgregarValidacion(cboRubros.SelectedIndex != -1, "Debe seleccionar el rubro del producto");
            validacionService.AgregarValidacion(!string.IsNullOrEmpty(txtStockMinimo.Text), "Debe ingresar el punto de reposición de stock");

            return validacionService.Validar(ref msj);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (Celda != -1)
            {
                grdComponentes.Rows.RemoveAt(Celda);
                Celda = -1;
            }
        }

        private void grdComponentes_CellClick(object sender, DataGridViewCellEventArgs e)
            => Celda = e.RowIndex;

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarFormularioDeComponentes();

            btnAgregarComponente.Enabled = false;
            txtCodigoProductoComposicion.Focus();
        }

        private void CargarDatosAlFormulario(Producto form, Persistencia.DTOs.Producto producto)
        {
            form.txtCodigoProducto.Text = producto.Codigo;
            form.txtDetalleProducto.Text = producto.Detalle;
            form.txtPrecioUnitario.Text = producto.PrecioUnitario.ToString();
            form.cboRubros.Text = producto.Rubro.Detalle;
            form.txtStockMinimo.Text = producto.Stock.CantidadMinima;
        }

        private void CargarDatosALaGrilla(Producto form, Combo combo)
        {
            foreach (var componente in combo.Componentes)
            {
                int rowId = form.grdComponentes.Rows.Add();

                form.grdComponentes.Rows[rowId].Cells[0].Value = componente.Producto.Codigo;
                form.grdComponentes.Rows[rowId].Cells[1].Value = componente.Producto.Detalle;
                form.grdComponentes.Rows[rowId].Cells[2].Value = componente.Cantidad;
                form.grdComponentes.Rows[rowId].Cells[3].Value = componente.Producto.Id;
            }
        }

        private bool ExisteItemEnGrid(Producto form, string codigoProducto)
        {
            foreach (DataGridViewRow row in form.grdComponentes.Rows)
            {
                if (row.Cells[0].Value.ToString() == codigoProducto)
                {
                    return true;
                }
            }

            return false;
        }

        public void NotificarAlertas(string idProduct)
        {
            publisherAlerta.Notificar(idProduct);
        }
    }
}