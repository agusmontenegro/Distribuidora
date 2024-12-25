using Persistencia.DAOs;
using System.Collections.Generic;

namespace Logica.Services.Rubro
{
    public class RubroService : IRubroService
    {
        private readonly DAORubro DAORubro;

        public RubroService()
        {
            DAORubro = new DAORubro();
        }

        public List<Persistencia.DTOs.Rubro> ObtenerRubros()
        {
            var rubros = DAORubro.ObtenerRubros();
            return rubros;
        }
    }
}