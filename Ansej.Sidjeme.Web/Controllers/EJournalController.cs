using Ansej.Domain.Core;
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
    [Route("[controller]/[action]")]
    public class EJournalController : Controller
    {
        private readonly JournalBusinessService _journalBusinessService;
        private readonly IWebHostEnvironment _env;

        public EJournalController(JournalBusinessService journalBusinessService, IWebHostEnvironment env)
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

        public virtual IActionResult Paged(string search = "", int page = 1, int pageSize = 6)
        {
            if (page < 1)
                page = 1;

            if (pageSize < 1)
                pageSize = 6;

            PagedResult<Journal> model;

            if (!string.IsNullOrWhiteSpace(search))
            {
                model = _journalBusinessService.GetAllFilteredPaged(
                    x => (x.TitleAr.Contains(search) || x.Description.Contains(search)) && x.IsArchived == false,
                    "Id DESC",
                    (page - 1) * pageSize,
                    pageSize,
                    _journalBusinessService.GetDefaultLoadProperties());
            }
            else
            {
                model = _journalBusinessService.GetAllFilteredPaged(x => x.IsArchived == false,
                    "Id DESC",
                    (page - 1) * pageSize,
                    pageSize,
                    _journalBusinessService.GetDefaultLoadProperties());
            }

            ViewBag.search = search;

            return View(model);
        }

    
        [Route("{id}")]
        public IActionResult View(int id)
        {
            var journal = _journalBusinessService.GetById(id);

            if (journal == null)
            {
                return NotFound();
            }

            // Vérification du fichier
            if (!string.IsNullOrEmpty(journal.PdfUrl))
            {
                // Extraire seulement le nom du fichier
                string fileName = journal.PdfUrl.TrimStart('/').Replace("documents/journals/", "");

                var filePath = Path.Combine(_env.WebRootPath, "documents", "journals", fileName);
                ViewBag.FileExists = System.IO.File.Exists(filePath);
                ViewBag.FullPath = $"/documents/journals/{fileName}";
            }
            else
            {
                ViewBag.FileExists = false;
                ViewBag.FullPath = "";
            }

            return View(journal);
        }

        
        public IActionResult DownloadPdf(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return BadRequest("Le nom du fichier est requis.");

            // Nettoyer le nom du fichier pour éviter les attaques de traversée de répertoire
            fileName = Path.GetFileName(fileName);

            var filePath = Path.Combine(_env.WebRootPath, "documents", "journals", fileName);

            if (!System.IO.File.Exists(filePath))
                return NotFound("Le fichier PDF est introuvable.");

            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            var contentType = "application/pdf";

            return File(fileBytes, contentType, fileName);
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

            // Retourner le chemin relatif complet
            return $"/documents/journals/{safeFileName}";
        }

        #endregion
    }
}