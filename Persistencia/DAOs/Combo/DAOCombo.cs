using Persistencia.Helpers.DataBase;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Persistencia.DAOs.Combo
{
    public class DAOCombo : IDAOCombo
    {
        private readonly IDataBaseHelper dataBaseHelper;

        public DAOCombo(IDataBaseHelper dataBaseHelper)
        {
            this.dataBaseHelper = dataBaseHelper;
        }

        public bool EsCombo_Id(string idProducto)
        {
            var query = "select * from dbo.Combo_View where Id = @IdProducto";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@IdProducto", idProducto)
            };
            var result = dataBaseHelper.ExecQuery(query, parameters);

            return result.Rows.Count > 0;
        }

        public bool EsCombo_Codigo(string codigoProducto)
        {
            var query = "select * from dbo.Combo_View where Codigo = '@CodigoProducto'";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@CodigoProducto", codigoProducto)
            };
            var result = dataBaseHelper.ExecQuery(query, parameters);

            return result.Rows.Count > 0;
        }

        public DataTable ObtenerCombo(string idProducto)
        {
            string query = "select * from dbo.Combo_View where Id = @IdProducto";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@IdProducto", idProducto)
            };
            var result = dataBaseHelper.ExecQuery(query, parameters);
            return result;
        }

        public void EliminarComponentes(string idProduct)
        {
            var query = "delete from dbo.Combo where comb_id = @IdProducto";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@IdProducto", idProduct)
            };
            dataBaseHelper.ExecNonQuery(query, parameters);
        }

        public void GuardarComponente(int idProducto, string idComponente, string cantidad)
        {
            dataBaseHelper.AgregarParametroEntrada(idProducto.ToString(), "@idProducto", SqlDbType.Int);
            dataBaseHelper.AgregarParametroEntrada(idComponente, "@idComponente", SqlDbType.Int);
            dataBaseHelper.AgregarParametroEntrada(cantidad, "@cantidad", SqlDbType.Int);

            _ = dataBaseHelper.ExecStoredProcedure("dbo.InsertarComponente");
        }

        public string InformarStockFaltante(string idProducto, string cantidad)
        {
            var query = "SELECT dbo.InformarStockFaltante(@idProducto, @cantidad);";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@idProducto", idProducto),
                new SqlParameter("@cantidad", cantidad),
            };

            var result = dataBaseHelper.ExecFunction(query, parameters);
            return result.ToString();
        }

    }
}
