namespace Persistencia.DTOs
{
    public class Producto
    {
        public string Id { get; set; }
        public string Codigo { get; set; }
        public string Detalle { get; set; }
        public decimal PrecioUnitario { get; set; }
        public Stock Stock { get; set; }
        public Rubro Rubro { get; set; }
        public bool Activo { get; set; }
        public string UltimaModificacion { get; set; }
    }
}