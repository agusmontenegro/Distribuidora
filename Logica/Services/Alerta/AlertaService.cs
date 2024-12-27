using Logica.Services.Combo;
using Logica.Services.Stock;
using Persistencia.DAOs;
using System.Collections.Generic;

namespace Logica.Services.Alerta
{
    public class AlertaService : IAlertaService
    {
        private readonly DAOAlerta DAOAlerta;
        private readonly IComboService comboService;
        private readonly IStockService stockService;

        public AlertaService(IComboService comboService,
            IStockService stockService)
        {
            DAOAlerta = new DAOAlerta();
            this.comboService = comboService;
            this.stockService = stockService;
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

        public void ActualizarAlertaDeReposicion(string idProducto)
        {
            if (!comboService.EsCombo_Id(idProducto))
            {
                if (stockService.HayQueReponer(idProducto))
                    EmitirAlertaDeReposicion(idProducto);
                else
                    QuitarAlertaDeReposicion(idProducto);
            }
        }
    }
}