using Ansej.Sidjeme.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Ansej.Sidjeme.Web.ViewModels
{
    public class RegistrantDeclarationHonorViewModel
    {
        public DeclarationHonneurMI Registrant { get; set; }
        [Required(ErrorMessage = "الحقل مطلوب")]
        public IFormFile CertificateNifFile { get; set; }
        [Required(ErrorMessage = "الحقل مطلوب")]
        public IFormFile BusinessRegisterFile { get; set; }
        [Required(ErrorMessage = "الحقل مطلوب")]
        public IFormFile DeclarationHonorFile { get; set; }
        [Required(ErrorMessage = "الحقل مطلوب")]
        public IFormFile ForecastProgramFile { get; set; }
    }
}
