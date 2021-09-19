using Distribuidora.Commons;
using Distribuidora.DTOs;
using Distribuidora.Factories;
using Distribuidora.Helpers;
using Distribuidora.Services;
using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace Distribuidora
{
    public partial class Estadistica : Form
    {
        private readonly RubroService rubroService;
        private readonly DataBaseHelper dataBaseHelper;
        private readonly FormsCommon formsCommon;

        public Estadistica()
        {
            InitializeComponent();
            rubroService = RubroServiceFactory.Crear();
            dataBaseHelper = DataBaseHelperFactory.Crear();
            formsCommon = FormsCommonFactory.Crear();
        }

        private void Estadistica_Load(object sender, EventArgs e)
        {
            CargarCombos();
            ProductoMasVendido();
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
            grdEstadisticas.Rows.Clear();
        }

        private void ProductoMasVendido()
        {
            string query = "select top 100 p.prod_codigo," +
                "                          p.prod_detalle," +
                "            			   p.prod_precio," +
                "            			   s.stoc_cantidad_actual," +
                "                          sum(i.item_cantidad)" +
                "           from Producto p" +
                "           join Stock s on p.prod_codigo = s.stoc_producto" +
                "           join Item_Venta i on i.item_producto = p.prod_codigo" +
                "           join Venta v on i.item_venta = v.vent_codigo";

            if (cboAños.SelectedIndex != -1)
            {
                query += " where year(v.vent_fecha) = " + cboAños.SelectedItem.ToString();
            }

            if (cboMeses.SelectedIndex != -1)
            {
                if (query.Contains("where"))
                    query += " and MONTH(v.vent_fecha) = " + (cboMeses.SelectedIndex + 1).ToString();
                else
                    query += " where MONTH(v.vent_fecha) = " + (cboMeses.SelectedIndex + 1).ToString();
            }

            if (cboRubros.SelectedIndex != -1)
            {
                if (query.Contains("where"))
                    query += " and p.prod_rubro = " + ((Rubro)cboRubros.SelectedItem).Codigo;
                else
                    query += " where p.prod_rubro = " + ((Rubro)cboRubros.SelectedItem).Codigo;
            }

            query += " group by p.prod_codigo, p.prod_Detalle, p.prod_precio, s.stoc_cantidad_actual";

            if (cboAños.SelectedIndex != -1)
            {
                query += " ,year(v.vent_fecha) ";
            }

            if (cboMeses.SelectedIndex != -1)
            {
                query += " ,MONTH(v.vent_fecha) ";
            }

            if (cboRubros.SelectedIndex != -1)
            {
                query += " ,p.prod_rubro";
            }

            query += " order by sum(i.item_cantidad) desc";

            CargarGrid(dataBaseHelper.ExecQuery(query));

            if (grdEstadisticas.Rows.Count == 0)
            {
                MessageBox.Show("No hay estadísticas que mostrar");
            }
        }

        private void CargarGrid(DataTable result)
        {
            grdEstadisticas.Rows.Clear();

            for (int i = 0;i < result.Rows.Count;i++)
            {
                grdEstadisticas.Rows.Add(
                    result.Rows[i][0].ToString(),
                    result.Rows[i][1].ToString(),
                    result.Rows[i][2].ToString(),
                    result.Rows[i][3].ToString(),
                    result.Rows[i][4].ToString());
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ProductoMasVendido();
        }
    }
}