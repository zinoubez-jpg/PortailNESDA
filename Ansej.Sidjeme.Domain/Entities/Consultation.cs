using Ansej.Domain.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ansej.Sidjeme.Domain.Entities
{
    public partial class Consultation : IEntity<int>
    {
        public int Id { get; set; }

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
        public string FilePath { get; set; }

        public bool IsArchived { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Foreign key
        public int StructureManagerId { get; set; }
        public StructureManager StructureManager { get; set; }
    }
}
