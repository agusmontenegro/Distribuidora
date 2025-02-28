using Persistencia.Helpers.DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Persistencia.DAOs.Stock
{
    public class DAOStock : IDAOStock
    {
        private readonly IDataBaseHelper dataBaseHelper;

        public DAOStock(IDataBaseHelper dataBaseHelper)
        {
            this.dataBaseHelper = dataBaseHelper;
        }

        public int GuardarReposicion()
        {
            dataBaseHelper.AgregarParametroSalida("@codigo", SqlDbType.Int);
            var salidas = dataBaseHelper.ExecStoredProcedure("dbo.GuardarReposicion");

            return int.Parse(salidas[0]);
        }

        public void ReponerStock(int reposicionCodigo, string idProducto, string cantidadAReponer)
        {
            dataBaseHelper.AgregarParametroEntrada(reposicionCodigo.ToString(), "@reposicion", SqlDbType.Int);
            dataBaseHelper.AgregarParametroEntrada(idProducto, "@idProducto", SqlDbType.Int);
            dataBaseHelper.AgregarParametroEntrada(cantidadAReponer, "@cantidadAReponer", SqlDbType.Int);

            _ = dataBaseHelper.ExecStoredProcedure("dbo.ReponerStock");
        }

        public DataTable ObtenerReposicion(string codigoReposicion)
        {
            string query = "select * from dbo.Reposicion_View where Codigo = @CodigoReposicion";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@CodigoReposicion", codigoReposicion)
            };
            var result = dataBaseHelper.ExecQuery(query, parameters);
            return result;
        }

        public bool HayStock(string codigoProducto, string cantidad)
        {
            var query = "SELECT dbo.HayStockDisponible(@CodigoProducto, @Cantidad);";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@CodigoProducto", codigoProducto),
                new SqlParameter("@Cantidad", cantidad),
            };
            var result = dataBaseHelper.ExecFunction(query, parameters);

            return Convert.ToBoolean(result.ToString());
        }

        public bool HayQueReponer(string idProducto)
        {
            var query = "SELECT dbo.LlegoAPuntoDeReposicion(@IdProducto);";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@IdProducto", idProducto)
            };
            var result = dataBaseHelper.ExecFunction(query, parameters);

            return Convert.ToBoolean(result.ToString());
        }
    }
}
