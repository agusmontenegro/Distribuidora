using Persistencia.DTOs;
using Persistencia.Helpers;
using System.Collections.Generic;
using System.Data;

namespace Persistencia.DAOs
{
    public class DAORubro
    {
        private readonly DataBaseHelper dataBaseHelper;

        public DAORubro()
        {
            dataBaseHelper = new DataBaseHelper();
        }

        public List<Rubro> ObtenerRubros()
        {
            var query = "select * from dbo.Rubro";
            var result = dataBaseHelper.ExecQuery(query);
            var rubros = MapearRubros(result.Rows);
            return rubros;
        }

        private List<Rubro> MapearRubros(DataRowCollection rows)
        {
            var rubros = new List<Rubro>();

            foreach (DataRow row in rows)
            {
                rubros.Add(new Rubro
                {
                    Codigo = row["rubr_codigo"].ToString(),
                    Detalle = row["rubr_detalle"].ToString(),
                });
            }

            return rubros;
        }
    }
}
