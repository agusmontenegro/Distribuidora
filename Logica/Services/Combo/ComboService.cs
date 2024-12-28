using Persistencia.DAOs.Combo;

namespace Logica.Services.Combo
{
    public class ComboService : IComboService
    {
        private readonly IDAOCombo dAOCombo;

        public ComboService(IDAOCombo dAOCombo)
        {
            this.dAOCombo = dAOCombo;
        }

        public bool EsCombo_Id(string idProducto)
        {
            var esCombo = dAOCombo.EsCombo_Id(idProducto);
            return esCombo;
        }

        public bool EsCombo_Codigo(string codigoProducto)
        {
            var esCombo = dAOCombo.EsCombo_Codigo(codigoProducto);
            return esCombo;
        }

        public Persistencia.DTOs.Combo ObtenerCombo(string idProducto)
        {
            var combo = dAOCombo.ObtenerCombo(idProducto);
            return combo;
        }

        public void EliminarComponentes(string idProduct)
        {
            dAOCombo.EliminarComponentes(idProduct);
        }

        public void GuardarComponente(int idProducto, string idComponente, string cantidad)
        {
            dAOCombo.GuardarComponente(idProducto, idComponente, cantidad);
        }

        public string InformarStockFaltante(string idProducto, string cantidad)
        {
            var stockFaltante = dAOCombo.InformarStockFaltante(idProducto, cantidad);
            return stockFaltante;
        }
    }
}