using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ansej.Sidjeme.Domain
{
	public partial class Activite : Referentiel
	{
        [Required]
        public int secteurActiviteId { get; set; }
        public virtual SecteurActivite secteur { get; set; }
    }
}
