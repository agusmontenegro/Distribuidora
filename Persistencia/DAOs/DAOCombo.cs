using Persistencia.DTOs;
using Persistencia.Helpers;
using System.Collections.Generic;
using System.Data;

namespace Persistencia.DAOs
{
    public class DAOCombo
    {
        private readonly DataBaseHelper DataBaseHelper;

        public DAOCombo()
        {
            DataBaseHelper = new DataBaseHelper();
        }

        public bool EsCombo_Id(string idProducto)
        {
            var query = "select * from dbo.Combo_View where Id = " + idProducto;
            var result = DataBaseHelper.ExecQuery(query);

            return result.Rows.Count > 0;
        }

        public bool EsCombo_Codigo(string codigoProducto)
        {
            var query = "select * from dbo.Combo_View where Codigo = '" + codigoProducto + "'";
            var result = DataBaseHelper.ExecQuery(query);

            return result.Rows.Count > 0;
        }

        public Combo ObtenerCombo(string idProducto)
        {
            string query = "select * from dbo.Combo_View where Id = " + idProducto;
            var result = DataBaseHelper.ExecQuery(query);

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
                        Id = row["Id"].ToString(),
                        Codigo = row["Codigo"].ToString(),
                        Detalle = row["Detalle"].ToString(),
                        PrecioUnitario = decimal.Parse(row["Precio"].ToString())
                    };

                    flag = false;
                }
                var componente = new Componente
                {
                    Producto = new Producto
                    {
                        Id = row["IdComponente"].ToString(),
                        Detalle = row["DetalleComponente"].ToString(),
                        Codigo = row["CodigoComponente"].ToString(),
                    },
                    Cantidad = row["CantidadComponente"].ToString()
                };

                combo.Componentes.Add(componente);
            }

            return combo;
        }

        public void EliminarComponentes(string idProduct)
        {
            var query = "delete from dbo.Combo where comb_id = " + idProduct;
            DataBaseHelper.ExecScript(query);
        }

        public void GuardarComponente(int idProducto, string idComponente, string cantidad)
        {
            DataBaseHelper.AgregarParametroEntrada(idProducto.ToString(), "@idProducto", SqlDbType.Int);
            DataBaseHelper.AgregarParametroEntrada(idComponente, "@idComponente", SqlDbType.Int);
            DataBaseHelper.AgregarParametroEntrada(cantidad, "@cantidad", SqlDbType.Int);

            _ = DataBaseHelper.ExecStoredProcedure("dbo.InsertarComponente");
        }

        public string InformarStockFaltante(string idProducto, string cantidad)
        {
            var query = "SELECT dbo.InformarStockFaltante(" + idProducto + "," + cantidad + ");";
            var result = DataBaseHelper.ExecFunction(query);

            return result.ToString();
        }
    }
}
