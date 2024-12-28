using Logica.Services.Combo;
using Logica.Services.Stock;
using Persistencia.DAOs.Alerta;
using System.Collections.Generic;

namespace Logica.Services.Alerta
{
    public class AlertaService : IAlertaService
    {
        private readonly IDAOAlerta dAOAlerta;
        private readonly IComboService comboService;
        private readonly IStockService stockService;

        public AlertaService(IDAOAlerta dAOAlerta,
            IComboService comboService,
            IStockService stockService)
        {
            this.dAOAlerta = dAOAlerta;
            this.comboService = comboService;
            this.stockService = stockService;
        }

        public void EmitirAlertaDeReposicion(string idProducto)
        {
            dAOAlerta.EmitirAlertaDeReposicion(idProducto);
        }

        public void QuitarAlertaDeReposicion(string idProducto)
        {
            dAOAlerta.QuitarAlertaDeReposicion(idProducto);
        }

        public int ObtenerCantidadDeAlertas()
        {
            var cantidad = dAOAlerta.ObtenerCantidadDeAlertas();
            return cantidad;
        }

        public List<Persistencia.DTOs.Alerta> ObtenerAlertas()
        {
            var alertas = dAOAlerta.ObtenerAlertas();
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