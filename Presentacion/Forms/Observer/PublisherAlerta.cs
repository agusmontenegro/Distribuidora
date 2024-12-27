using System.Collections.Generic;

namespace Presentacion.Forms.Observer
{
    public class PublisherAlerta : IPublisherAlerta
    {
        private readonly List<ISuscriptorAlerta> suscriptores;

        public PublisherAlerta()
        {
            suscriptores = new List<ISuscriptorAlerta>();
        }

        public void Notificar(string idProduct)
        {
            suscriptores.ForEach(s => s.ActualizarAlertas());
        }

        public void Subscribe(ISuscriptorAlerta suscriptor)
        {
            if (!suscriptores.Contains(suscriptor))
            {
                suscriptores.Add(suscriptor);
            }
        }

        public void Unsubscribe(ISuscriptorAlerta suscriptor)
        {
            if (suscriptores.Contains(suscriptor))
            {
                suscriptores.Remove(suscriptor);
            }
        }
    }
}
