namespace Distribuidora.DTOs
{
    public class Producto
    {
        public string Codigo { get; set; }
        public string Detalle { get; set; }
        public decimal PrecioUnitario { get; set; }
        public Stock Stock { get; set; }
        public Rubro Rubro { get; set; }
    }
}