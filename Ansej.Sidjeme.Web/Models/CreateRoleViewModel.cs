using System.ComponentModel.DataAnnotations;

namespace Ansej.Sidjeme.Web.Models
{
    public class CreateRoleViewModel
    {
        [Required]
        [Display(Name = "Role")]
        public string RoleName { get; set; }
    }
}
