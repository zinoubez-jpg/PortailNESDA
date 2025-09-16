using Ansej.Sidjeme.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ansej.Sidjeme.Domain
{
    public partial class Wilaya : Referentiel
    {
        public Wilaya()
        {
            this.Communes = new HashSet<Commune>();
        }
        public ICollection<ContactUs> ContactUs { get; set; }
        public virtual ICollection<Commune> Communes { get; set; }
    }
}
