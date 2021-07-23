namespace Distribuidora.DTOs
{
    public class Stock
    {
        public string CodigoProducto { get; set; }
        public string CantidadActual { get; set; }
        public string CantidadMinima { get; set; }
        public string ProximaReposicion { get; set; }
        public string UltimaReposicion { get; set; }
    }
}
