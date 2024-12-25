using System.Collections.Generic;

namespace Logica.Services.Rubro
{
    public interface IRubroService
    {
        List<Persistencia.DTOs.Rubro> ObtenerRubros();
    }
}