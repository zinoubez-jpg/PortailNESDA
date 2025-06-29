using Ansej.Domain.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ansej.Sidjeme.Domain.Entities
{
    public class Article : IEntity<int>
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; }


        [MaxLength(500)]
        public string Summary { get; set; }

        [Required]
        public string Content { get; set; }

        public string ImageUrl { get; set; }

        [MaxLength(500)]
        public string Slug { get; set; }

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
