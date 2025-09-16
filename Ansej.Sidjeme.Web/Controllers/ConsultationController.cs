using Ansej.Sidjeme.Business;
using Ansej.Sidjeme.Domain.Entities;
using Ansej.Sidjeme.Web.Models;
using Ansej.Sidjeme.Web.Views.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Ansej.Sidjeme.Web.Controllers
{
    public class ConsultationController : Controller
    {

        StructureManagerBusinessService _administrativeStructureBusinessService;
        ConsultationBusinessService _consultationBusinessService;
        public ConsultationController(StructureManagerBusinessService administrativeStructure,
            ConsultationBusinessService consultationBusinessService)
        {
            _administrativeStructureBusinessService = administrativeStructure;
            _consultationBusinessService = consultationBusinessService;

        }
        public IActionResult Index()
        {

            var NbConsultation = _consultationBusinessService.GetAllFiltered(x => x.IsArchived == false).Count();
            ViewBag.ConsultationVivante = NbConsultation;

            var topStructures = _administrativeStructureBusinessService
              .GetAllFiltered(s => s.ParentStructureManagerId == null, _administrativeStructureBusinessService.GetDefaultLoadProperties())
              .ToList();



            //prepare result list
            var result = new List<TopStructureGroup>();

            foreach (var top in topStructures)
            {
                var hasChildren = top.SubStructures != null && top.SubStructures.Any();

                List<Consultation> consultations;
                bool IsFromChild = false;

                if (!hasChildren)
                {

                    consultations = top.Consultations
                    .OrderByDescending(c => c.PublishDate)
                    .ToList();
                }
                else
                {
                    var childIds = top.SubStructures.Select(s => s.Id).ToList();

                    consultations = _consultationBusinessService
                        .GetAllFiltered(c => childIds.Contains(c.StructureManagerId), _consultationBusinessService.GetDefaultLoadProperties())
                        .OrderByDescending(c => c.PublishDate)
                        .ToList();
                    IsFromChild = true;
                }

                result.Add(new TopStructureGroup
                {
                    TopStructure = top,
                    Consultations = consultations,
                    IsFromChild = IsFromChild
                });
            }
            return View(result);
        }

        [Authorize(Roles = "Admin,GestionConsultation")]
        [HttpGet]
        public IActionResult Add()
        {
            InitializeStructureViewBag();
            return View();
        }
        [Authorize(Roles = "Admin,GestionConsultation")]
        public IActionResult All()
        {

            return View();
        }
        [Authorize(Roles = "Admin,GestionConsultation")]
        [HttpPost]
        public async Task<IActionResult> Add(ConsultationUploadModel model)
        {
            if (!ModelState.IsValid)
            {
                InitializeStructureViewBag();
                return View(model);
            }

            string relativePath = "documents/consultations";
            string filePath = await SaveUploadedFileAsync(model.File, relativePath, model.TitleFr);

            // Eng consultation
            var consultation = new Consultation
            {
                TitleFr = model.TitleFr,
                TitleEn = model.TitleEn,
                TitleAr = model.TitleAr,
                Description = model.Description,
                PublishDate = model.PublishDate,
                FilePath = filePath,
                StructureManagerId = model.SubAdministrativeStructureId ?? model.AdministrativeStructureId,
                IsArchived = model.IsArchived,
                CreatedAt = DateTime.UtcNow
            };

            _consultationBusinessService.Add(consultation);

            return RedirectToAction("All");
        }
        [Authorize(Roles = "Admin,GestionConsultation")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var consultation = _consultationBusinessService.GetById(id);
            InitializeStructureViewBag();
            if (consultation == null)
                return NotFound();

            var viewModel = new ConsultationUploadModel
            {
                TitleAr = consultation.TitleAr,
                TitleFr = consultation.TitleFr,
                TitleEn = consultation.TitleEn,
                Description = consultation.Description,
                PublishDate = consultation.PublishDate,
                IsArchived = consultation.IsArchived,
                AdministrativeStructureId = consultation.StructureManagerId,
                
            };

            ViewBag.ConsultationId = id;
            return View(viewModel);
        }
        [Authorize(Roles = "Admin,GestionConsultation")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ConsultationUploadModel model, int id)
        {
            if (!ModelState.IsValid)
                return View(model);

            var consultation = _consultationBusinessService.GetById(id);
            if (consultation == null)
                return NotFound();

            consultation.TitleAr = model.TitleAr;
            consultation.TitleFr = model.TitleFr;
            consultation.TitleEn = model.TitleEn;
            consultation.Description = model.Description;
            consultation.PublishDate = model.PublishDate;
            consultation.IsArchived = model.IsArchived;

            // Pour telechargement du (mise a jour) fichier
            if (model.File != null && model.File.Length > 0)
            {
                string relativePath = "documents/consultations";
                string filePath = await SaveUploadedFileAsync(model.File, relativePath, model.TitleFr);

                consultation.FilePath = filePath;
            }

            var result = _consultationBusinessService.Update(consultation);
            if (result.Succeeded)
                return RedirectToAction(nameof(All));

            return View(model);
        }


        [HttpGet]
        public IActionResult GetSubStructures(int parentId)
        {
            var children = _administrativeStructureBusinessService
                .GetAll()
                .Where(s => s.ParentStructureManagerId == parentId)
                .Select(s => new { s.Id, s.NameAr })
                .ToList();

            return Json(children);
        }

        [HttpPost]
        public IActionResult GetAllConsultationsDataTable(DataTableAjaxModel model)
        {
            GetDataTableParameters(model, out string search, out string orderBy, out int startRowIndex, out int maxRows);

            if (!string.IsNullOrEmpty(search))
            {
                var result = _consultationBusinessService.GetAllFilteredPaged(x => x.TitleAr.Contains(search),
                   orderBy, startRowIndex, maxRows, _consultationBusinessService.GetDefaultLoadProperties());


                return Json(new JQueryDataTableRetunedData<Consultation> { draw = model.draw, recordsFiltered = result.TotalCount, recordsTotal = result.TotalCount, data = result.Items });
            }
            else
            {
                var result = _consultationBusinessService.GetAllPaged(orderBy, startRowIndex, maxRows, _consultationBusinessService.GetDefaultLoadProperties());


                return Json(new JQueryDataTableRetunedData<Consultation> { draw = model.draw, recordsFiltered = result.TotalCount, recordsTotal = result.TotalCount, data = result.Items });
            }
        }
        [Authorize(Roles = "Admin,GestionConsultation")]
        public IActionResult ToggleArchive(int id)
        {
            var consultation = _consultationBusinessService.GetById(id);
            if (consultation == null)
                return NotFound();

            consultation.IsArchived = !consultation.IsArchived;
            consultation.CreatedAt = DateTime.Now;

            var result = _consultationBusinessService.Update(consultation);
            return result.Succeeded ? Ok() : StatusCode(500);
        }




        #region Helper Methods

        private void InitializeStructureViewBag()
        {
            ViewBag.Structures = _administrativeStructureBusinessService
                .GetAllFiltered(s => s.ParentStructureManagerId == null)
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.NameAr
                }).ToList();
        }

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
