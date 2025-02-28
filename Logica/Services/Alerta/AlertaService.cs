using Logica.Services.Combo;
using Logica.Services.Stock;
using Persistencia.DAOs.Alerta;
using Persistencia.DTOs;
using System.Collections.Generic;
using System.Data;

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
            var result = dAOAlerta.ObtenerAlertas();
            var alertas = MapearAlertas(result.Rows);
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

        private List<Persistencia.DTOs.Alerta> MapearAlertas(DataRowCollection rows)
        {
            var alertas = new List<Persistencia.DTOs.Alerta>();

            foreach (DataRow row in rows)
            {
                var alerta = new Persistencia.DTOs.Alerta
                {
                    Codigo = row["aler_codigo"].ToString(),
                    Detalle = row["aler_detalle"].ToString(),
                    Fecha = row["aler_fecha"].ToString(),
                    Objeto = row["aler_objeto"].ToString(),
                    TipoAlerta = new TipoAlerta
                    {
                        Codigo = row["tale_codigo"].ToString(),
                        Detalle = row["tale_detalle"].ToString()
                    }
                };

                alertas.Add(alerta);
            }

            return alertas;
        }
    }
}