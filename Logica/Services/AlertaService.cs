using Persistencia.DAOs;
using Persistencia.DTOs;
using System.Collections.Generic;

namespace Logica.Services
{
    public class AlertaService
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

        public List<Alerta> ObtenerAlertas()
        {
            var alertas = DAOAlerta.ObtenerAlertas();
            return alertas;
        }
    }
}