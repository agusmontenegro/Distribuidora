using System.Collections.Generic;

namespace Distribuidora.Services
{
    public class ValidacionService
    {
        public class Validacion
        {
            public bool condicion { get; set; }
            public string msj { get; set; }
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
                if (!validation.condicion)
                {
                    valid = false;
                    msj += validation.msj + "\n";
                }
            }

            Validaciones.Clear();

            return valid;
        }

        public void AgregarValidacion(bool condicion, string msj)
        {
            Validaciones.Add(new Validacion { condicion = condicion, msj = msj });
        }
    }
}