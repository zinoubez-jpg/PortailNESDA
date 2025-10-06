using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Ansej.Sidjeme.Web.Views.ViewModels
{
    public class JournalViewModel
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

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date de publication")]
        public DateTime PublishDate { get; set; }

        
        [Display(Name = "Journal PDF")]
        public IFormFile PdfFile { get; set; }

        
        [Display(Name = "Photo de couverture")]
        public IFormFile CoverPicture { get; set; }

        // ABED: Blk to remove later if assigned the user authoer from code !
        [Display(Name = "Auteur")]
        public string Author { get; set; }
    }
}
