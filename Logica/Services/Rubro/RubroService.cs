using Persistencia.DAOs.Rubro;
using System.Collections.Generic;
using System.Data;

namespace Logica.Services.Rubro
{
    public class RubroService : IRubroService
    {
        private readonly IDAORubro dAORubro;

        public RubroService(IDAORubro dAORubro)
        {
            this.dAORubro = dAORubro;
        }

        public List<Persistencia.DTOs.Rubro> ObtenerRubros()
        {
            var result = dAORubro.ObtenerRubros();
            var rubros = MapearRubros(result.Rows);
            return rubros;
        }

        private List<Persistencia.DTOs.Rubro> MapearRubros(DataRowCollection rows)
        {
            var rubros = new List<Persistencia.DTOs.Rubro>();

            foreach (DataRow row in rows)
            {
                rubros.Add(new Persistencia.DTOs.Rubro
                {
                    Codigo = row["rubr_codigo"].ToString(),
                    Detalle = row["rubr_detalle"].ToString(),
                });
            }

            return rubros;
        }
    }
}