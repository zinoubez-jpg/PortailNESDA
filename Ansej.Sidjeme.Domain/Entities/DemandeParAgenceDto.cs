using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ansej.Sidjeme.Domain.Entities
{
    public class DemandeParAgenceDto
    {
        [Display(Name = "الوكالة الولائية")]
        public string Agence { get; set; }

        [Display(Name = "عدد التصريحات")]
        public int Nombre { get; set; }
    }
}
