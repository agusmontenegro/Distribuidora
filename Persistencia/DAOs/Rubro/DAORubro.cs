using Persistencia.Helpers.DataBase;
using System.Data;

namespace Persistencia.DAOs.Rubro
{
    public class DAORubro : IDAORubro
    {
        private readonly IDataBaseHelper dataBaseHelper;

        public DAORubro(IDataBaseHelper dataBaseHelper)
        {
            this.dataBaseHelper = dataBaseHelper;
        }

        public DataTable ObtenerRubros()
        {
            var query = "select * from dbo.Rubro";
            var result = dataBaseHelper.ExecQuery(query, null);
            return result;
        }
    }
}
