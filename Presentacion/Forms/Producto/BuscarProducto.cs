using Logica.Services;
using Presentacion.Forms.Helpers;
using System;
using System.Collections.Generic;
using System.Transactions;
using System.Windows.Forms;
using System.Linq;

namespace Presentacion.Forms.Producto
{
    public partial class BuscarProducto : Form
    {
        public int Fila = 0;
        private readonly Menu Menu;

        private readonly BuscarProductoFormHelper BuscarProductoFormHelper;
        private readonly RubroService RubroService;
        private readonly ProductoService ProductoService;
        private readonly ExcelService ExcelService;
        private readonly ValidacionService ValidacionService;

        public BuscarProducto(Menu Menu)
        {
            InitializeComponent();
            this.Menu = Menu;

            BuscarProductoFormHelper = new BuscarProductoFormHelper();
            RubroService = new RubroService();
            ProductoService = new ProductoService();
            ExcelService = new ExcelService();
            ValidacionService = new ValidacionService();
        }

        private void Producto_Load(object sender, EventArgs e)
        {
            btnEliminarProducto.Enabled = false;
            btnEditarProducto.Enabled = false;
            CargarCombos();
        }

        private void txtCodigoProducto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                RealizarBusqueda();
            }
        }

        private void txtDetalleProducto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                RealizarBusqueda();
            }
        }

        private void CargarCombos()
        {
            var rubros = RubroService.ObtenerRubros();
            cboRubros.Items.AddRange(rubros.ToArray());
            cboRubros.DisplayMember = "detalle";
            cboRubros.ValueMember = "codigo";
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarBusqueda();
        }

        private void LimpiarBusqueda()
        {
            cboRubros.SelectedIndex = -1;
            txtCodigoProducto.Text = string.Empty;
            txtDetalleProducto.Text = string.Empty;
            txtCodigoProducto.Focus();
            grdResult.Rows.Clear();
            btnEliminarProducto.Enabled = false;
            btnEditarProducto.Enabled = false;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            RealizarBusqueda();
        }

        private void RealizarBusqueda()
        {
            string msj = string.Empty;

            if (BusquedaValida(ref msj))
            {
                var producto = BuscarProductoFormHelper.CompletarObjeto(this);
                var resultados = ProductoService.Buscar(producto);
                CargarGrid(resultados);
                btnEliminarProducto.Enabled = true;
                btnEditarProducto.Enabled = true;

                if (grdResult.Rows.Count == 0)
                {
                    btnEliminarProducto.Enabled = false;
                    btnEditarProducto.Enabled = false;
                    MessageBox.Show("La búsqueda no arrojó ningún resultado");
                }
            }
            else
            {
                MessageBox.Show(msj);
                txtCodigoProducto.Focus();
            }
        }

        private bool BusquedaValida(ref string msj)
        {
            ValidacionService.AgregarValidacion(
                !string.IsNullOrEmpty(txtCodigoProducto.Text) ||
                !string.IsNullOrEmpty(txtDetalleProducto.Text) ||
                cboRubros.SelectedIndex != -1,
                "Ingrese algún criterio de búsqueda");

            return ValidacionService.Validar(ref msj);
        }

        private void CargarGrid(List<Persistencia.DTOs.Producto> productos)
        {
            grdResult.Rows.Clear();
            foreach (var producto in productos)
            {
                grdResult.Rows.Add(producto.Codigo,
                    producto.Detalle,
                    producto.PrecioUnitario.ToString(),
                    producto.Rubro.Detalle,
                    producto.Stock.CantidadActual,
                    producto.Stock.UltimaReposicion,
                    producto.UltimaModificacion,
                    producto.Id,
                    false);
            }
        }

        private void btnEliminarProducto_Click(object sender, EventArgs e)
        {
            if (Fila != -1)
            {
                var codigoProducto = grdResult.Rows[Fila].Cells[0].Value.ToString();
                var detalleProducto = grdResult.Rows[Fila].Cells[1].Value.ToString();

                var dialogResult = MessageBox.Show("Se eliminará el producto " + codigoProducto + " - " + detalleProducto + ".\n¿Continuar? ", "Eliminar producto", MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    try
                    {
                        ProductoService.EliminarProducto(codigoProducto);
                        grdResult.Rows.RemoveAt(Fila);
                        Fila = -1;
                        MessageBox.Show("Se ha eliminado el producto exitosamente");

                        if (grdResult.Rows.Count == 0)
                        {
                            btnEliminarProducto.Enabled = false;
                            btnEditarProducto.Enabled = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Hubo un error al intentar eliminar el producto. " + ex.Message);
                    }
                }
            }
            else
                MessageBox.Show("Seleccione un producto");
        }

        private void grdResult_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Fila = e.RowIndex;
        }

        private void btnNuevoProducto_Click(object sender, EventArgs e)
        {
            var altaProducto = new Producto(Menu);
            altaProducto.ShowDialog();
            RealizarBusqueda();
        }

        private void btnEditarProducto_Click(object sender, EventArgs e)
        {
            if (Fila != -1)
            {
                var idProducto = grdResult.Rows[Fila].Cells[7].Value.ToString();
                var editarProducto = new Producto(Menu, idProducto);
                editarProducto.ShowDialog();
                RealizarBusqueda();
            }
            else
                MessageBox.Show("Seleccione un producto");
        }

        private void btnInfoProductos_Click(object sender, EventArgs e)
        {
            var reporte = new ReporteInfoProductos();
            reporte.ShowDialog();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            MessageBox.Show(ExcelService.ImportarProductos());
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            MessageBox.Show(ExcelService.ExportarProductos());
        }

        //Solo se permite actualizar detalle y precio
        //Para stock, rubro y combos se hace en Editar Producto y Seguimiento de Stock
        private void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            using (var scope = new TransactionScope())
            {
                try
                {
                    string msj = string.Empty;
                    foreach (DataGridViewRow fila in grdResult.Rows.Cast<DataGridViewRow>().Where(f => (bool)f.Cells[8].Value))
                    {
                        if (ItemValido(fila, ref msj))
                        {
                            var codigoProd = fila.Cells[0]?.Value?.ToString();
                            var IdProduct = fila.Cells[7].Value.ToString();
                            var detalleProd = fila.Cells[1].Value.ToString();
                            var precio = fila.Cells[2].Value.ToString();

                            ProductoService.ActualizarProductoLazy(IdProduct, codigoProd, detalleProd, precio);
                        }
                    }
                    if (string.IsNullOrEmpty(msj))
                    {
                        scope.Complete();
                        MessageBox.Show("Se han guardado los cambios correctamente");
                    }
                    else
                    {
                        scope.Dispose();
                        MessageBox.Show(msj);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hubo un error al intentar guardar los cambios. " + ex.Message);
                }
            }
        }

        private bool ItemValido(DataGridViewRow fila, ref string msj)
        {
            var codigoProd = fila.Cells[0]?.Value?.ToString();
            ValidacionService.AgregarValidacion(!string.IsNullOrEmpty(fila.Cells[1]?.Value?.ToString()), codigoProd + " - Ingrese el DETALLE del producto");
            ValidacionService.AgregarValidacion(!string.IsNullOrEmpty(fila.Cells[2]?.Value?.ToString()), codigoProd + " - Ingrese el PRECIO del producto");
            return ValidacionService.Validar(ref msj);
        }

        private void grdResult_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (!grdResult.Columns[e.ColumnIndex].ReadOnly)
                {
                    grdResult.Rows[Fila].Cells[8].Value = true;
                }
            }
        }
    }
}