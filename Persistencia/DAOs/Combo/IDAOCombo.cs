namespace Persistencia.DAOs.Combo
{
    public interface IDAOCombo
    {
        bool EsCombo_Id(string idProducto);
        bool EsCombo_Codigo(string codigoProducto);
        DTOs.Combo ObtenerCombo(string idProducto);
        void EliminarComponentes(string idProduct);
        void GuardarComponente(int idProducto, string idComponente, string cantidad);
        string InformarStockFaltante(string idProducto, string cantidad);
    }
}
