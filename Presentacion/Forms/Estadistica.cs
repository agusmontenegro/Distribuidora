using Logica.Services.Producto;
using Logica.Services.Rubro;
using Persistencia.DTOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace Presentacion.Forms
{
    public partial class Estadistica : Form
    {
        private readonly IRubroService rubroService;
        private readonly IProductoService productoService;

        public Estadistica(
            IRubroService rubroService,
            IProductoService productoService)
        {
            InitializeComponent();
            this.rubroService = rubroService;
            this.productoService = productoService;
        }

        private void Estadistica_Load(object sender, EventArgs e)
        {
            CargarCombos();
            ObtenerEstadisticaDeProductos();
        }

        private void CargarCombos()
        {
            cboRubros.Items.AddRange(rubroService.ObtenerRubros().ToArray());
            cboRubros.DisplayMember = "Detalle";
            cboRubros.ValueMember = "Codigo";

            cboAños.Items.AddRange(Enumerable.Range(DateTime.Now.Year - 10, 11).Cast<object>().ToArray());
            cboMeses.Items.AddRange(CultureInfo.InvariantCulture.DateTimeFormat.MonthNames.Take(12).ToArray());
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            cboRubros.SelectedIndex = -1;
            cboMeses.SelectedIndex = -1;
            cboAños.SelectedIndex = -1;
            chkIncludeNoActivo.Checked = false;
            grdEstadisticas.Rows.Clear();
        }

        private void ObtenerEstadisticaDeProductos()
        {
            var estadistica = CompletarObjeto();
            var resultados = productoService.BuscarParaEstadistica(estadistica);
            CargarGrid(resultados);

            if (grdEstadisticas.Rows.Count == 0)
            {
                MessageBox.Show("No hay estadísticas que mostrar");
            }
        }

        private Persistencia.DTOs.Estadistica CompletarObjeto()
        {
            var estadistica = new Persistencia.DTOs.Estadistica();

            if (!chkIncludeNoActivo.Checked)
            {
                estadistica.EstaActivoProducto = true;
            }

            if (cboAños.SelectedIndex != -1)
            {
                estadistica.Año = cboAños.SelectedItem.ToString();
            }

            if (cboMeses.SelectedIndex != -1)
            {
                estadistica.Mes = cboMeses.SelectedItem.ToString();
            }

            if (cboRubros.SelectedIndex != -1)
            {
                estadistica.RubroProducto = ((Rubro)cboRubros.SelectedItem).Codigo;
            }

            return estadistica;
        }

        private void CargarGrid(List<Persistencia.DTOs.Estadistica> estadisticas)
        {
            grdEstadisticas.Rows.Clear();

            foreach (var estadistica in estadisticas)
            {
                grdEstadisticas.Rows.Add(estadistica.CodigoProducto,
                    estadistica.DetalleProducto,
                    estadistica.PrecioUnitarioProducto.ToString(),
                    estadistica.StockActualProducto.ToString(),
                    estadistica.CantidadTotal.ToString());
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ObtenerEstadisticaDeProductos();
        }
    }
}