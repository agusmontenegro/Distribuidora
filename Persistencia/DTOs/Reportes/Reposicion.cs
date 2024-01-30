using System.Collections.Generic;

namespace Persistencia.DTOs.Reportes
{
    public class Reposicion
    {
        public string Fecha { get; set; }
        public List<ItemReposicion> Items { get; set; }
    }
}