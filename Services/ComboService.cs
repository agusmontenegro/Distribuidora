using Distribuidora.DTOs;
using Distribuidora.Helpers;
using System.Collections.Generic;
using System.Data;

namespace Distribuidora.Services
{
    public class ComboService
    {
        private readonly DataBaseHelper dataBaseHelper;

        public ComboService()
        {
            dataBaseHelper = new DataBaseHelper();
        }

        public bool EsCombo(string codigoProducto)
        {
            var query = "select comb_codigo from dbo.Combo where comb_codigo = " + codigoProducto;
            var result = dataBaseHelper.ExecQuery(query);

            return result.Rows.Count > 0;
        }

        public Combo ObtenerCombo(string codigoProducto)
        {
            string query = "select * from dbo.Combo_View where Producto = " + codigoProducto;
            var result = dataBaseHelper.ExecQuery(query);

            var combo = MapearCombo(result.Rows);

            return combo;
        }

        private Combo MapearCombo(DataRowCollection rows)
        {
            var combo = new Combo();
            combo.Componentes = new List<Componente>();
            bool flag = true;

            foreach (DataRow row in rows)
            {
                if (flag)
                {
                    combo.Producto = new Producto
                    {
                        Codigo = row["Producto"].ToString(),
                        Detalle = row["Detalle"].ToString(),
                        PrecioUnitario = decimal.Parse(row["Precio"].ToString())
                    };

                    flag = false;
                }
                var componente = new Componente
                {
                    Producto = new Producto
                    {
                        Codigo = row["CodigoComponente"].ToString(),
                        Detalle = row["DetalleComponente"].ToString()
                    },
                    Cantidad = row["CantidadComponente"].ToString()
                };

                combo.Componentes.Add(componente);
            }

            return combo;
        }

        public void EliminarComponentes(string codigoProductoEditar)
        {
            var query = "delete from dbo.Combo where comb_codigo = " + codigoProductoEditar;
            dataBaseHelper.ExecScript(query);
        }

        public void GuardarComponente(int codigoProducto, string codigoComponente, string cantidad)
        {
            dataBaseHelper.AgregarParametroEntrada(codigoProducto.ToString(), "@codigoProducto", SqlDbType.Int);
            dataBaseHelper.AgregarParametroEntrada(codigoComponente, "@codigoComponente", SqlDbType.Int);
            dataBaseHelper.AgregarParametroEntrada(cantidad, "@cantidad", SqlDbType.Int);

            _ = dataBaseHelper.ExecStoredProcedure("dbo.InsertarComponente");
        }
    }
}