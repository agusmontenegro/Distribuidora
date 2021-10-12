using System.Collections.Generic;

namespace Distribuidora.DTOs.Reportes
{
    public class Venta
    {
        public string Fecha { get; set; }
        public decimal Total { get; set; }
        public List<ItemVenta> Items { get; set; }
    }
}