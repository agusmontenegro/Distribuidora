namespace Presentacion.Forms.Observer
{
    public interface IPublisherAlerta
    {
        void Subscribe(ISuscriptorAlerta suscriptor);
        void Unsubscribe(ISuscriptorAlerta suscriptor);
        void Notificar(string idProduct);
    }
}
