namespace Distribuidora.DTOs
{
    public class Alerta
    {
        public string Codigo { get; set; }
        public string Detalle { get; set; }
        public string Objeto { get; set; }
        public TipoAlerta TipoAlerta { get; set; }
        public string Fecha { get; set; }
    }
}