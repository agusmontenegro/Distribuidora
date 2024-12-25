namespace Logica.Services.Validacion
{
    public interface IValidacionService
    {
        bool Validar(ref string msj);
        void AgregarValidacion(bool condicion, string msj);
    }
}