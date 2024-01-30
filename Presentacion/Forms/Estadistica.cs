using Distribuidora.Forms.Helpers;
using Logica.Services;
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
        private readonly RubroService RubroService;
        private readonly ProductoService ProductoService;
        private readonly EstadisticaFormHelper EstadisticaFormHelper;

        public Estadistica()
        {
            InitializeComponent();
            RubroService = new RubroService();
            ProductoService = new ProductoService();
            EstadisticaFormHelper = new EstadisticaFormHelper();
        }

        private void Estadistica_Load(object sender, EventArgs e)
        {
            CargarCombos();
            ObtenerEstadisticaDeProductos();
        }

        private void CargarCombos()
        {
            cboRubros.Items.AddRange(RubroService.ObtenerRubros().ToArray());
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
            var estadistica = EstadisticaFormHelper.CompletarObjeto(this);
            var resultados = ProductoService.BuscarParaEstadistica(estadistica);
            CargarGrid(resultados);

            if (grdEstadisticas.Rows.Count == 0)
            {
                MessageBox.Show("No hay estadísticas que mostrar");
            }
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