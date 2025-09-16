using Ansej.Domain.Core;
using Ansej.Sidjeme.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ansej.Sidjeme.Domain
{
    public class DownloadFile : IEntity<int>
    {
        [Display(Name = "Id")]
        [Required(ErrorMessage = "RequiredMessage")]
        public int Id { get; set; }              
        
       
        [Required(ErrorMessage = "RequiredMessage")]
        public string Path { get; set; }

        
        public int TypeFileId { get; set; }
        public virtual TypeFile TypeFile { get; set; }

        public int DeclarationHonneurMIId { get; set; }
        public virtual DeclarationHonneurMI DeclarationHonneurMI { get; set; }

    }
}
