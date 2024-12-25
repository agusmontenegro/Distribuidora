namespace Logica.Services.Combo
{
    public interface IComboService
    {
        bool EsCombo_Id(string idProducto);
        bool EsCombo_Codigo(string codigoProducto);
        Persistencia.DTOs.Combo ObtenerCombo(string idProducto);
        void EliminarComponentes(string idProduct);
        void GuardarComponente(int idProducto, string idComponente, string cantidad);
        string InformarStockFaltante(string idProducto, string cantidad);
    }
}