using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ansej.Sidjeme.Web.Views.ViewModels
{
    public class ConsultationUploadModel
    {
        [Required]
        [StringLength(500)]
        [Display(Name = "Titre (Arabe)")]
        public string TitleAr { get; set; }

        [Required]
        [StringLength(500)]
        [Display(Name = "Titre (Français)")]
        public string TitleFr { get; set; }

        [Required]
        [StringLength(500)]
        [Display(Name = "Title (English)")]
        public string TitleEn { get; set; }


        public string Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime PublishDate { get; set; }

        [Required]
        public int AdministrativeStructureId { get; set; }

        public int? SubAdministrativeStructureId { get; set; }

        public IFormFile File { get; set; }

        public bool IsArchived { get; set; }
    }
}
