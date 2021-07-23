using System.Collections.Generic;

namespace Distribuidora.DTOs
{
    public class Combo
    {
        public Producto Producto { get; set; }
        public List<Componente> Componentes { get; set; }
    }
}
