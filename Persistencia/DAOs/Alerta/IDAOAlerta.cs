using System.Collections.Generic;

namespace Persistencia.DAOs.Alerta
{
    public interface IDAOAlerta
    {
        void EmitirAlertaDeReposicion(string idProducto);
        void QuitarAlertaDeReposicion(string idProducto);
        int ObtenerCantidadDeAlertas();
        List<DTOs.Alerta> ObtenerAlertas();
    }
}
