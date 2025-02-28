using Persistencia.Helpers.DataBase;
using System.Data;

namespace Persistencia.DAOs.Alerta
{
    public class DAOAlerta : IDAOAlerta
    {
        private readonly IDataBaseHelper dataBaseHelper;

        public DAOAlerta(IDataBaseHelper dataBaseHelper)
        {
            this.dataBaseHelper = dataBaseHelper;
        }

        public void EmitirAlertaDeReposicion(string idProducto)
        {
            dataBaseHelper.AgregarParametroEntrada(idProducto, "@producto", SqlDbType.Int);
            _ = dataBaseHelper.ExecStoredProcedure("dbo.EmitirAlertaDeReposicion");
        }

        public void QuitarAlertaDeReposicion(string idProducto)
        {
            dataBaseHelper.AgregarParametroEntrada(idProducto, "@producto", SqlDbType.Int);
            _ = dataBaseHelper.ExecStoredProcedure("dbo.QuitarAlertaDeReposicion");
        }

        public int ObtenerCantidadDeAlertas()
        {
            var query = "select * from alerta";
            var result = dataBaseHelper.ExecQuery(query, null);

            return result.Rows.Count;
        }

        public DataTable ObtenerAlertas()
        {
            string query = "select * from dbo.Alerta a join dbo.Tipo_Alerta ta on a.aler_tipo = ta.tale_codigo";
            var result = dataBaseHelper.ExecQuery(query, null);
            return result;
        }
    }
}
