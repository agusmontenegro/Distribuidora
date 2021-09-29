using Distribuidora.DTOs;
using Distribuidora.Helpers;
using System.Collections.Generic;
using System.Data;

namespace Distribuidora.Services
{
    public class RubroService
    {
        private readonly DataBaseHelper dataBaseHelper;

        public RubroService()
        {
            dataBaseHelper = new DataBaseHelper();
        }

        public List<Rubro> ObtenerRubros()
        {
            var query = "select * from dbo.Rubro";
            var result = dataBaseHelper.ExecQuery(query);

            var producto = MapearRubros(result.Rows);

            return producto;
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