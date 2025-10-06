using Ansej.Domain.Core;
using Ansej.Sidjeme.Business;
using Ansej.Sidjeme.Domain.Entities;
using Ansej.Sidjeme.Web.Models;
using Ansej.Sidjeme.Web.Views.ViewModels;
using Microsoft.AspNetCore.Authorization;
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

            string relativePath = $"documents/journals/{CleanFileName(model.TitleFr)}";
            string filePath = await SaveUploadedFileAsync(model.PdfFile, relativePath, "Journal");
            string filePathCoverImage = await SaveUploadedFileAsync(model.CoverPicture, relativePath,"CoverPic");

            var journal = new Journal
            {
                TitleAr = model.TitleAr,
                TitleFr = model.TitleFr,
                TitleEn = model.TitleEn,
                Description = model.Description,
                PublishDate = model.PublishDate,
                PdfUrl = filePath,
                CoverPicture= filePathCoverImage,
                Author = model.Author,
                CreatedAt = DateTime.Now,
                IsArchived = false
            };

            var businessResult = _journalBusinessService.Add(journal);

            return businessResult.Succeeded ? RedirectToAction("Paged") : View(model);
        }

        [HttpGet("{id}")]
        public  IActionResult Edit(int id)
        {
            var journal =  _journalBusinessService.GetById(id);
            if (journal == null)
                return NotFound();

            var model = new JournalViewModel
            {
                TitleAr = journal.TitleAr,
                TitleFr = journal.TitleFr,
                TitleEn = journal.TitleEn,
                Description = journal.Description,
                PublishDate = journal.PublishDate,
                Author = journal.Author
            };


            var baseUrl = $"{Request.Scheme}://{Request.Host}/";
            ViewBag.ExistingPdf = !string.IsNullOrEmpty(journal.PdfUrl)
                ? baseUrl + journal.PdfUrl
                : null;
            ViewBag.ExistingCover = !string.IsNullOrEmpty(journal.CoverPicture)
                ? baseUrl + journal.CoverPicture
                : null;

            //ViewBag.ExistingPdf = journal.PdfUrl;
            //ViewBag.ExistingCover = journal.CoverPicture;

            return View(model);
        }

        
        [HttpPost("{id}")]
        public async Task<IActionResult> Edit(int id, JournalViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var journal = _journalBusinessService.GetById(id);
            if (journal == null)
                return NotFound();

            string relativePath = $"documents/journals/{CleanFileName(model.TitleFr)}";

            
            if (model.PdfFile != null && model.PdfFile.Length > 0)
            {

                /*
                // Delete old file if exists
                if (!string.IsNullOrEmpty(journal.PdfUrl))
                {
                    string oldPath = Path.Combine(_env.WebRootPath, journal.PdfUrl);
                    if (System.IO.File.Exists(oldPath))
                        System.IO.File.Delete(oldPath);
                }
                */

                
                string newPdfPath = await SaveUploadedFileAsync(model.PdfFile, relativePath, "Journal");
                journal.PdfUrl = newPdfPath;
            }

            // Handle Cover Picture re-upload
            if (model.CoverPicture != null && model.CoverPicture.Length > 0)
            {

                /*
                if (!string.IsNullOrEmpty(journal.CoverPicture))
                {
                    string oldCoverPath = Path.Combine(_env.WebRootPath, journal.CoverPicture);
                    if (System.IO.File.Exists(oldCoverPath))
                        System.IO.File.Delete(oldCoverPath);
                }
                */

                string newCoverPath = await SaveUploadedFileAsync(model.CoverPicture, relativePath, "CoverPic");
                journal.CoverPicture = newCoverPath;
            }

            // Update other fields
            journal.TitleAr = model.TitleAr;
            journal.TitleFr = model.TitleFr;
            journal.TitleEn = model.TitleEn;
            journal.Description = model.Description;
            journal.PublishDate = model.PublishDate;
            journal.Author = model.Author;

            var result = _journalBusinessService.Update(journal);

            if (result.Succeeded)
                return RedirectToAction("Paged");

            return View(model);
        }

        [HttpGet]
        public IActionResult All() {

            return View();
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

            if (journal == null || journal.IsArchived)
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

        [HttpPost]
        public IActionResult GetAllJournalsDataTable(DataTableAjaxModel model)
        {
            GetDataTableParameters(model, out string search, out string orderBy, out int startRowIndex, out int maxRows);

            if (!string.IsNullOrEmpty(search))
            {
                var result = _journalBusinessService.GetAllFilteredPaged(x => x.TitleAr.Contains(search),
                   orderBy, startRowIndex, maxRows, _journalBusinessService.GetDefaultLoadProperties());


                return Json(new JQueryDataTableRetunedData<Journal> { draw = model.draw, recordsFiltered = result.TotalCount, recordsTotal = result.TotalCount, data = result.Items });
            }
            else
            {
                var result = _journalBusinessService.GetAllPaged(orderBy, startRowIndex, maxRows, _journalBusinessService.GetDefaultLoadProperties());


                return Json(new JQueryDataTableRetunedData<Journal> { draw = model.draw, recordsFiltered = result.TotalCount, recordsTotal = result.TotalCount, data = result.Items });
            }
        }

        [Route("{id}")]
        [HttpPost]
        public IActionResult ToggleArchive(int id)
        {
            var journal = _journalBusinessService.GetById(id);
            if (journal == null)
                return NotFound();

            journal.IsArchived = !journal.IsArchived;

            var result = _journalBusinessService.Update(journal);
            return result.Succeeded ? Ok() : StatusCode(500);
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


            string cleanedBaseName = CleanFileName(fileBaseName);
            string safeFileName = $"{cleanedBaseName}_{timestamp}{extension}";

            string fullPath = Path.Combine(absoluteFolderPath, safeFileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
                return $"/{relativeFolderPath}/{safeFileName}";
        }

        string CleanFileName(string input)
        {
            var invalidChars = Path.GetInvalidFileNameChars();
            var cleaned = new string(input.Where(ch => !invalidChars.Contains(ch)).ToArray());
            return cleaned.Replace(" ", "_");
        }


        protected static void GetDataTableParameters(DataTableAjaxModel model, out string search, out string orderBy, out int startRowIndex, out int maxRows)
        {
            maxRows = model.length;
            startRowIndex = model.start;
            string sortBy = "", sortDir = "";

            if (model.order != null)
            {
                // in this example we just default sort on the 1st column
                sortBy = model.columns[model.order[0].column].data;
                sortDir = model.order[0].dir.ToLower();
                orderBy = sortBy + " " + sortDir;
            }
            else
            {
                orderBy = "";
            }

            if (model.search.value != null)
            {
                search = model.search.value;
            }
            else
            {
                search = "";
            }
        }
        #endregion
    }
}