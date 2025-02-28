using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Persistencia.Helpers.DataBase
{
    public interface IDataBaseHelper
    {
        void AgregarParametroEntrada(string parametro, string parametroSQL, SqlDbType sqlDbType);
        void AgregarParametroSalida(string parametroSQL, SqlDbType sqlDbType);
        List<string> ExecStoredProcedure(string storedProcedure);
        object ExecFunction(string query, List<SqlParameter> parameters);
        DataTable ExecQuery(string query, List<SqlParameter> parameters);
        void ExecNonQuery(string query, List<SqlParameter> parameters);
    }
}