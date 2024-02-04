using Logica.Services;
using Presentacion.Forms.Helpers;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Presentacion.Forms.Producto
{
    public partial class BuscarProducto : Form
    {
        public int Celda = 0;
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
                    producto.Id);
            }
        }

        private void btnEliminarProducto_Click(object sender, EventArgs e)
        {
            if (Celda != -1)
            {
                var codigoProducto = grdResult.Rows[Celda].Cells[0].Value.ToString();
                var detalleProducto = grdResult.Rows[Celda].Cells[1].Value.ToString();

                var dialogResult = MessageBox.Show("Se eliminará el producto " + codigoProducto + " - " + detalleProducto + ".\n¿Continuar? ", "Eliminar producto", MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    try
                    {
                        ProductoService.EliminarProducto(codigoProducto);
                        grdResult.Rows.RemoveAt(Celda);
                        Celda = -1;
                        MessageBox.Show("Se ha eliminado el producto exitosamente");

                        if (grdResult.Rows.Count == 0)
                        {
                            btnEliminarProducto.Enabled = false;
                            btnEditarProducto.Enabled = false;
                        }
                    }
                    catch
                    {
                        throw new Exception("Hubo un error al querer eliminar el producto");
                    }
                }
            }
            else
                MessageBox.Show("Seleccione un producto");
        }

        private void grdResult_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Celda = e.RowIndex;
        }

        private void btnNuevoProducto_Click(object sender, EventArgs e)
        {
            var altaProducto = new Producto(Menu);
            altaProducto.ShowDialog();
            RealizarBusqueda();
        }

        private void btnEditarProducto_Click(object sender, EventArgs e)
        {
            if (Celda != -1)
            {
                var idProducto = grdResult.Rows[Celda].Cells[7].Value.ToString();
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
    }
}