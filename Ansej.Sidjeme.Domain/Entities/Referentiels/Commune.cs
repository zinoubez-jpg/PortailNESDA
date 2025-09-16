using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ansej.Sidjeme.Domain.Entities
{
    public partial class Commune : Referentiel
    {

        [Display(Name = "WilayaId")]
        [Required(ErrorMessage = "RequiredMessage")]
        public int WilayaId { get; set; }
        public virtual Wilaya Wilaya { get; set; }
        public virtual ICollection<PreInscriptionSousTraitence> InscriptionsCNais { get; set; }
       
    }
}
