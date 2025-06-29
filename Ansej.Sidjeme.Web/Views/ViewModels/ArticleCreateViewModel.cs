using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ansej.Sidjeme.Web.Views.ViewModels
{
    public class ArticleCreateViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        [MaxLength(500)]
        public string? Summary { get; set; }

        [Required]
        public string Content { get; set; }

        public IFormFile Image { get; set; }

        public bool IsPinned { get; set; } = false;

        public bool IsPublished { get; set; } = false;
    }
}
