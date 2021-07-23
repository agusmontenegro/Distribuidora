using System.Collections.Generic;

namespace Distribuidora.Services
{
    public class ValidationService
    {
        public class Validation
        {
            public bool condition { get; set; }
            public string msj { get; set; }
        }

        public IList<Validation> Validations { get; set; }

        public ValidationService()
        {
            Validations = new List<Validation>();
        }

        public bool validate(ref string msj)
        {
            bool valid = true;

            foreach (Validation validation in Validations)
            {
                if (!validation.condition)
                {
                    valid = false;
                    msj += validation.msj + "\n";
                }
            }

            return valid;
        }
    }
}
