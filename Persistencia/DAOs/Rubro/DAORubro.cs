using Persistencia.Helpers.DataBase;
using System.Collections.Generic;
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

        public List<DTOs.Rubro> ObtenerRubros()
        {
            var query = "select * from dbo.Rubro";
            var result = dataBaseHelper.ExecQuery(query);
            var rubros = MapearRubros(result.Rows);
            return rubros;
        }

        private List<DTOs.Rubro> MapearRubros(DataRowCollection rows)
        {
            var rubros = new List<DTOs.Rubro>();

            foreach (DataRow row in rows)
            {
                rubros.Add(new DTOs.Rubro
                {
                    Codigo = row["rubr_codigo"].ToString(),
                    Detalle = row["rubr_detalle"].ToString(),
                });
            }

            return rubros;
        }
    }
}
