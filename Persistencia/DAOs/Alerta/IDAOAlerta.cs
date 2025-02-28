using System.Data;

namespace Persistencia.DAOs.Alerta
{
    public interface IDAOAlerta
    {
        void EmitirAlertaDeReposicion(string idProducto);
        void QuitarAlertaDeReposicion(string idProducto);
        int ObtenerCantidadDeAlertas();
        DataTable ObtenerAlertas();
    }
}
