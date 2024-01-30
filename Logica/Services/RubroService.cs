using Persistencia.DAOs;
using Persistencia.DTOs;
using System.Collections.Generic;

namespace Logica.Services
{
    public class RubroService
    {
        private readonly DAORubro DAORubro;

        public RubroService()
        {
            DAORubro = new DAORubro();
        }

        public List<Rubro> ObtenerRubros()
        {
            var rubros = DAORubro.ObtenerRubros();
            return rubros;
        }
    }
}