using System.Collections.Concurrent;

namespace Ansej.Sidjeme.Web.Services
{
    public class GenererNumeroService : IGenererNumeroService
    {
        private readonly ConcurrentDictionary<string, int> _numerosSequentiels;

        public GenererNumeroService()
        {
            _numerosSequentiels = new ConcurrentDictionary<string, int>();
        }

        public string GenererNumero(string codeStructure)
        {
            // Try to update the value, if the key exists. If the key does not exist, initialize it to 0.
            int numeroSequentiel = _numerosSequentiels.AddOrUpdate(codeStructure, 1, (key, oldValue) => oldValue + 1);



            // Generate the sequential number string with leading zeros
            return $"{codeStructure}{numeroSequentiel:D5}";
        }
    }

}
