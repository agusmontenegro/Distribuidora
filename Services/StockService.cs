using Distribuidora.Helpers;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Distribuidora.Services
{
    public static class StockService
    {
        public static void ReponerStock(string codigoProducto, string cantidadAReponer)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            SqlParameter codigoProductoParameter = new SqlParameter("@codigoProducto", SqlDbType.Int);
            codigoProductoParameter.Value = int.Parse(codigoProducto);

            SqlParameter cantidadParameter = new SqlParameter("@cantidadAReponer", SqlDbType.Int);
            cantidadParameter.Value = int.Parse(cantidadAReponer);

            parameters.Add(codigoProductoParameter);
            parameters.Add(cantidadParameter);

            DataBaseHelper.ExecStoredProcedure("dbo.ReponerStock", parameters);
        }
    }
}
