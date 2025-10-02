using Ansej.Sidjeme.Business;
using Ansej.Sidjeme.Domain.Entities;
using Ansej.Sidjeme.Web.Views.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Ansej.Sidjeme.Web.Controllers
{
    public class JournalController : Controller
    {

        private readonly JournalBusinessService _journalBusinessService;
        private readonly IWebHostEnvironment _env;

        public JournalController(JournalBusinessService journalBusinessService, IWebHostEnvironment env)
        {
            _journalBusinessService = journalBusinessService;
            _env = env;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Add(JournalViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string relativePath = "documents/journals";
            string filePath = await SaveUploadedFileAsync(model.PdfFile, relativePath, model.TitleFr);

            var journal = new Journal
                {
                    TitleAr = model.TitleAr,
                    TitleFr = model.TitleFr,
                    TitleEn = model.TitleEn,
                    Description = model.Description,
                    PublishDate = model.PublishDate,
                    PdfUrl = filePath,
                    Author = model.Author,
                    CreatedAt = DateTime.Now,
                    IsArchived = false 
                };

            var businessResult = _journalBusinessService.Add(journal);

            return businessResult.Succeeded ? RedirectToAction("All") : View(model);
        }


        #region Helper Methods

        private async Task<string> SaveUploadedFileAsync(IFormFile file, string relativeFolderPath, string fileBaseName)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Le fichier est vide.");

            string wwwrootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

            if (relativeFolderPath.StartsWith("/"))
                relativeFolderPath = relativeFolderPath.TrimStart('/');

            string absoluteFolderPath = Path.Combine(wwwrootPath, relativeFolderPath);

            if (!Directory.Exists(absoluteFolderPath))
                Directory.CreateDirectory(absoluteFolderPath);

            string extension = Path.GetExtension(file.FileName);
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");

            string CleanFileName(string input)
            {
                var invalidChars = Path.GetInvalidFileNameChars();
                var cleaned = new string(input.Where(ch => !invalidChars.Contains(ch)).ToArray());
                return cleaned.Replace(" ", "_");
            }

            string cleanedBaseName = CleanFileName(fileBaseName);
            string safeFileName = $"{cleanedBaseName}_{timestamp}{extension}";

            string fullPath = Path.Combine(absoluteFolderPath, safeFileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return $"/{relativeFolderPath}/{safeFileName}";
        }

        #endregion
    }
}

