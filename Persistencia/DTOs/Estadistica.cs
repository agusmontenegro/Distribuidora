namespace Persistencia.DTOs
{
    public class Estadistica
    {
        public string CodigoProducto { get; set; }
        public string DetalleProducto { get; set; }
        public decimal PrecioUnitarioProducto { get; set; }
        public int StockActualProducto { get; set; }
        public int CantidadTotal { get; set; }
        public bool EstaActivoProducto { get; set; }
        public string RubroProducto { get; set; }
        public string Año { get; set; }
        public string Mes { get; set; }
    }
}
