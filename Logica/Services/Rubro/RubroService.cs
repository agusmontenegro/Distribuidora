using Persistencia.DAOs.Rubro;
using System.Collections.Generic;

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
            var rubros = dAORubro.ObtenerRubros();
            return rubros;
        }
    }
}