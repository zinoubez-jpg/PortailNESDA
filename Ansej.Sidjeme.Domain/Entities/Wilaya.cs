using Ansej.Sidjeme.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ansej.Sidjeme.Domain
{
    public partial class Wilaya : Referentiel
    {
        public ICollection<ContactUs> ContactUs { get; set; }
    }
}
