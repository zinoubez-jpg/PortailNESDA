using Ansej.Domain.Core;
using System.ComponentModel.DataAnnotations;

namespace Ansej.Sidjeme.Domain
{
    public class MotifTraitement : IEntity<int>
    {
        [Display(Name = "Id")]
        [Required(ErrorMessage = "RequiredMessage")]
        public int Id { get; set; }
        
                       
        [Display(Name = "DesignationAr")]
        [Required(ErrorMessage = "RequiredMessage"), StringLength(256, ErrorMessage = "StringLengthMessage")]
        public string DesignationAr { get; set; }
        
       
    }
}
