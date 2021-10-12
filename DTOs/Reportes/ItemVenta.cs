namespace Distribuidora.DTOs.Reportes
{
    public class ItemVenta
    {
        public string Producto { get; set; }
        public string Detalle { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int Cantidad { get; set; }
        public decimal Subtotal { get; set; }
    }
}