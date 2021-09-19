using Distribuidora.Factories;
using Distribuidora.Helpers;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Distribuidora.Services
{
    public class ComboService
    {
        private readonly DataBaseHelper dataBaseHelper;

        public ComboService()
        {
            dataBaseHelper = DataBaseHelperFactory.Crear();
        }

        public bool EsCombo(string codigoProducto)
        {
            var query = "select comb_codigo from dbo.Combo where comb_codigo = " + codigoProducto;
            var result = dataBaseHelper.ExecQuery(query);

            return result.Rows.Count > 0;
        }

        public DTOs.Combo ObtenerCombo(string codigoProducto)
        {
            string query = "select * from dbo.Combo_View where Producto = " + codigoProducto;
            string ConnectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
            SqlConnection Connection = new SqlConnection(ConnectionString);
            using (Connection)
            {
                Connection.Open();
                using (var cmd = new SqlCommand(query, Connection))
                {
                    var reader = cmd.ExecuteReader();
                    var combo = new DTOs.Combo();
                    combo.Componentes = new List<DTOs.Componente>();
                    bool flag = true;

                    while (reader.Read())
                    {
                        if (flag)
                        {
                            combo.Producto = new DTOs.Producto
                            {
                                Codigo = reader["Producto"].ToString(),
                                Detalle = reader["Detalle"].ToString(),
                                PrecioUnitario = decimal.Parse(reader["Precio"].ToString())
                            };

                            flag = false;
                        }
                        var componente = new DTOs.Componente
                        {
                            Producto = new DTOs.Producto
                            {
                                Codigo = reader["CodigoComponente"].ToString(),
                                Detalle = reader["DetalleComponente"].ToString()
                            },
                            Cantidad = reader["CantidadComponente"].ToString()
                        };

                        combo.Componentes.Add(componente);
                    }

                    return combo;
                }
            }
        }

        public void EliminarComponentes(string codigoProductoEditar)
        {
            var query = "delete from dbo.Combo where comb_codigo = " + codigoProductoEditar;
            dataBaseHelper.ExecScript(query);
        }

        public void GuardarComponente(int codigoProducto, string codigoComponente, string cantidad)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            SqlParameter codigoProductoParameter = new SqlParameter("@codigoProducto", SqlDbType.Int);
            codigoProductoParameter.Value = codigoProducto;

            SqlParameter codigoComponenteParameter = new SqlParameter("@codigoComponente", SqlDbType.Int);
            codigoComponenteParameter.Value = int.Parse(codigoComponente);

            SqlParameter cantidadParameter = new SqlParameter("@cantidad", SqlDbType.Int);
            cantidadParameter.Value = int.Parse(cantidad);

            parameters.Add(codigoProductoParameter);
            parameters.Add(codigoComponenteParameter);
            parameters.Add(cantidadParameter);

            dataBaseHelper.ExecStoredProcedure("dbo.InsertarComponente", parameters);
        }
    }
}