using System.Collections.Generic;

namespace Logica.Services.Alerta
{
    public interface IAlertaService
    {
        void ActualizarAlertaDeReposicion(string idProducto);
        int ObtenerCantidadDeAlertas();
        List<Persistencia.DTOs.Alerta> ObtenerAlertas();
    }
}