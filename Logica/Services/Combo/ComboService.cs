using Persistencia.DAOs;

namespace Logica.Services.Combo
{
    public class ComboService : IComboService
    {
        private readonly DAOCombo DAOCombo;

        public ComboService()
        {
            DAOCombo = new DAOCombo();
        }

        public bool EsCombo_Id(string idProducto)
        {
            var esCombo = DAOCombo.EsCombo_Id(idProducto);
            return esCombo;
        }

        public bool EsCombo_Codigo(string codigoProducto)
        {
            var esCombo = DAOCombo.EsCombo_Codigo(codigoProducto);
            return esCombo;
        }

        public Persistencia.DTOs.Combo ObtenerCombo(string idProducto)
        {
            var combo = DAOCombo.ObtenerCombo(idProducto);
            return combo;
        }

        public void EliminarComponentes(string idProduct)
        {
            DAOCombo.EliminarComponentes(idProduct);
        }

        public void GuardarComponente(int idProducto, string idComponente, string cantidad)
        {
            DAOCombo.GuardarComponente(idProducto, idComponente, cantidad);
        }

        public string InformarStockFaltante(string idProducto, string cantidad)
        {
            var stockFaltante = DAOCombo.InformarStockFaltante(idProducto, cantidad);
            return stockFaltante;
        }
    }
}