using Ansej.Domain.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ansej.Sidjeme.Domain.Entities
{
    public class Article : IEntity<int>
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        [RegularExpression(@"^[\u0621-\u064A\s]+$", ErrorMessage = "يجب استخدام الحروف العربية فقط")]
        public string Title { get; set; }

        [Required]
        [MaxLength(255)]
        [Column(TypeName = "varchar(255)")]
        [RegularExpression(@"^[A-Za-z0-9\s.,!?-]+$", ErrorMessage = "Please use English characters only.")]
        public string TitleEn { get; set; }

       // [Required]
        [MaxLength(255)]
        [Column(TypeName = "varchar(255)")]
        [RegularExpression(@"^[A-Za-z0-9\s.,!?-]+$", ErrorMessage = "Please use English characters only.")]
        public string TitleFr { get; set; }



        [MaxLength(500)]
        [RegularExpression(@"^[\u0621-\u064A\s]+$", ErrorMessage = "يجب استخدام الحروف العربية فقط")]
        public string Summary { get; set; }

        [MaxLength(900)]
        [Column(TypeName = "varchar(900)")]
        [RegularExpression(@"^[A-Za-z0-9\s.,!?-]+$", ErrorMessage = "Please use English characters only.")]
        public string SummaryEn { get; set; }

        [MaxLength(900)]
        [Column(TypeName = "varchar(900)")]
        [RegularExpression(@"^[A-Za-z0-9\s.,!?-]+$", ErrorMessage = "Please use English characters only.")]
        public string SummaryFr{ get; set; }

        [Required]
        [RegularExpression(@"^[\u0621-\u064A\s]+$", ErrorMessage = "يجب استخدام الحروف العربية فقط")]
        public string Content { get; set; }

        [Required]
        [Column(TypeName = "varchar(Max)")]
        [RegularExpression(@"^[A-Za-z0-9\s.,!?-]+$", ErrorMessage = "Please use English characters only.")]
        public string ContentEn { get; set; }

       // [Required]
        [Column(TypeName = "varchar(Max)")]
        [RegularExpression(@"^[A-Za-z0-9\s.,!?-]+$", ErrorMessage = "Please use English characters only.")]
        public string ContentFr { get; set; }

        public string ImageUrl { get; set; }

        [MaxLength(500)]
        public string Slug { get; set; }

        [MaxLength(500)]
        [Column(TypeName = "varchar(500)")]
        public string SlugEn { get; set; }

        [MaxLength(500)]
        [Column(TypeName = "varchar(500)")]
        public string SlugFr { get; set; }

        public bool IsPinned { get; set; } = false;
        public bool IsPublished { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        [Required]
        public string AuthorId { get; set; }

        public bool IsDeleted { get; set; } = false;
        public string deletedBy { get; set; }
    }
}
