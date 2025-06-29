using System.Collections.Generic;

namespace Ansej.Sidjeme.Identity
{
    public class Niveau : Referentiel
    {
        public virtual ICollection<Structure> Structures { get; set; }

    }
}
