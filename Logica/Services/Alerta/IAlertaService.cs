using System.Collections.Generic;

namespace Logica.Services.Alerta
{
    public interface IAlertaService
    {
        void EmitirAlertaDeReposicion(string idProducto);
        void QuitarAlertaDeReposicion(string idProducto);
        int ObtenerCantidadDeAlertas();
        List<Persistencia.DTOs.Alerta> ObtenerAlertas();
    }
}