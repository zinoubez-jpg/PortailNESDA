using Ansej.Domain.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ansej.Sidjeme.Domain.Entities
{
    public class StructureManager : IEntity<int>
    {
       public int Id { get; set; }

        [Required]
        [StringLength(150)]
        [Display(Name = "Nom (Arabe)")]
        public string NameAr { get; set; }

        [Required]
        [StringLength(150)]
        [Display(Name = "Nom (Français)")]
        public string NameFr { get; set; }

        [StringLength(150)]
        [Display(Name = "Name (English)")]
        public string NameEn { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int? ParentStructureManagerId { get; set; }
        public StructureManager ParentStructureManager { get; set; }

        // Navigation property
        public ICollection<Consultation> Consultations { get; set; }
        public ICollection<StructureManager> SubStructures { get; set; }

    }
}
