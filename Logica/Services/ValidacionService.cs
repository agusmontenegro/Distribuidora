using System.Collections.Generic;

namespace Logica.Services
{
    public class ValidacionService
    {
        public class Validacion
        {
            public bool Condicion { get; set; }
            public string Msj { get; set; }
        }

        private IList<Validacion> Validaciones { get; set; }

        public ValidacionService()
        {
            Validaciones = new List<Validacion>();
        }

        public bool Validar(ref string msj)
        {
            bool valid = true;

            foreach (var validation in Validaciones)
            {
                if (!validation.Condicion)
                {
                    valid = false;
                    msj += validation.Msj + "\n";
                }
            }

            Validaciones.Clear();

            return valid;
        }

        public void AgregarValidacion(bool condicion, string msj)
        {
            Validaciones.Add(new Validacion { Condicion = condicion, Msj = msj });
        }
    }
}