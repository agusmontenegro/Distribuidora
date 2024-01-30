using System.Collections.Generic;

namespace Persistencia.DTOs
{
    public class Combo
    {
        public Producto Producto { get; set; }
        public List<Componente> Componentes { get; set; }
    }
}
