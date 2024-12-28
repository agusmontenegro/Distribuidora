using System.Collections.Generic;
using System.Data;

namespace Persistencia.Helpers.DataBase
{
    public interface IDataBaseHelper
    {
        void AgregarParametroEntrada(string parametro, string parametroSQL, SqlDbType sqlDbType);
        void AgregarParametroSalida(string parametroSQL, SqlDbType sqlDbType);
        List<string> ExecStoredProcedure(string storedProcedure);
        object ExecFunction(string query);
        DataTable ExecQuery(string query);
        void ExecScript(string query);
    }
}