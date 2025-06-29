using Ansej.Domain.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ansej.Sidjeme.Domain.Entities
{
    public class ContactUs : IEntity<int>
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(255)]
        public string Email { get; set; }


        [Required]
        [StringLength(150)]
        public string Subject { get; set; }

        [Required]
        public string Message { get; set; }

        [Phone]
        [StringLength(10, ErrorMessage = "Le numéro de téléphone ne doit pas dépasser 10 chiffres.")]
        [RegularExpression(@"^\d{9,10}$", ErrorMessage = "Le numéro de téléphone ne doit pas dépasser 10 chiffres.")]
        public string Phone { get; set; }

        [StringLength(45)]
        public string IpAddress { get; set; }

        [StringLength(512)]
        public string UserAgent { get; set; }

        public bool IsRead { get; set; } = false;
       


        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsTraited { get; set; } = false;
        public DateTime TraitedAt { get; set; } = DateTime.UtcNow;
        public string Observation { get; set; }


        [Required(ErrorMessage = "Veuillez sélectionner wilaya.")]
        [Display(Name = "Wilaya")]
        public int WilayaId { get; set; }

        public Wilaya Wilaya { get; set; }
    }
}
