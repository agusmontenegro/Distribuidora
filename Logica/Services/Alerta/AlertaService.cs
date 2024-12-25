using Persistencia.DAOs;
using System.Collections.Generic;

namespace Logica.Services.Alerta
{
    public class AlertaService : IAlertaService
    {
        private readonly DAOAlerta DAOAlerta;

        public AlertaService()
        {
            DAOAlerta = new DAOAlerta();
        }

        public void EmitirAlertaDeReposicion(string idProducto)
        {
            DAOAlerta.EmitirAlertaDeReposicion(idProducto);
        }

        public void QuitarAlertaDeReposicion(string idProducto)
        {
            DAOAlerta.QuitarAlertaDeReposicion(idProducto);
        }

        public int ObtenerCantidadDeAlertas()
        {
            var cantidad = DAOAlerta.ObtenerCantidadDeAlertas();
            return cantidad;
        }

        public List<Persistencia.DTOs.Alerta> ObtenerAlertas()
        {
            var alertas = DAOAlerta.ObtenerAlertas();
            return alertas;
        }
    }
}