using Ansej.Domain.Core;
using Ansej.Sidjeme.Business;
using Ansej.Sidjeme.Domain.Entities;
using Ansej.Sidjeme.Identity;
using Ansej.Sidjeme.Web.Models;
using Ansej.Sidjeme.Web.Views.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ansej.Sidjeme.Web.Controllers
{
    
    public class ArticleController : Controller
    {
        private readonly ArticleBusinessService _articleBusinessService;
        private readonly UserService _userService;
        private readonly IMemoryCache _memoryCache;
        public ArticleController(ArticleBusinessService articleBusinessService,
                                 UserService userService,
                                 IMemoryCache memoryCache)
        {
            _articleBusinessService = articleBusinessService;
            _userService = userService;
            _memoryCache = memoryCache;
        }
      

     

        public virtual IActionResult Paged(string search = "", int page = 1, int pageSize = 6)
        {
            if (page < 1)
                page = 1;

            if (pageSize < 1)
                pageSize = 6;

            PagedResult<Article> model;

            if (!string.IsNullOrWhiteSpace(search))
            {
                model = _articleBusinessService.GetAllFilteredPaged(
                    x => (x.Title.Contains(search) || x.Summary.Contains(search)) && x.IsPublished==true && x.IsDeleted==false,
                    "IsPinned DESC, Id DESC",
                    (page - 1) * pageSize,
                    pageSize,
                    _articleBusinessService.GetDefaultLoadProperties());
            }
            else
            {
                model = _articleBusinessService.GetAllFilteredPaged(x=>x.IsPublished == true && x.IsDeleted == false,
                    "IsPinned DESC, Id DESC",
                    (page - 1) * pageSize,
                    pageSize,
                    _articleBusinessService.GetDefaultLoadProperties());
            }

            ViewBag.search = search;

            return View(model);
        }


        [Authorize(Roles = "Admin,Article")]

        public IActionResult All()
        {

            return View();
        }


        [HttpGet]
        [Authorize(Roles = "Admin,Article")]
        public IActionResult Create()

        {
            return View();
        }
        [Authorize(Roles = "Admin,Article")]
        [HttpPost]
        public async Task<IActionResult> Create(ArticleCreateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            // Generate slug if not manually set (you can extend the ViewModel to accept a Slug too)
            var slug = await GenerateUniqueSlugAsync(model.Title);
            string relativePath = $"documents/articles/{slug}";
            string filePath = await SaveUploadedFileAsync(model.Image, relativePath, model.Title);

            var user = await _userService.GetUserAsync(User);

            var article = new Article
            {
                Title = model.Title,
                Summary = model.Summary,
                Content = model.Content,
                ImageUrl = filePath,
                IsPinned = model.IsPinned,
                IsPublished = model.IsPublished,
                CreatedAt = DateTime.Now,
                AuthorId = user.FullName,
                Slug = slug
            };

            var result = _articleBusinessService.Add(article);

            if (!result.Succeeded)
                return View(model);

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Article")]
        public async Task<IActionResult> Edit(int id)
        {
            var article = _articleBusinessService.GetById(id);
            if (article == null)
                return NotFound();

            var viewModel = new ArticleCreateViewModel
            {
                Id = article.Id,
                Title = article.Title,
                Summary = article.Summary,
                Content = article.Content,
                IsPinned = article.IsPinned,
                IsPublished = article.IsPublished
            };

            return View(viewModel);
        }

        [Authorize(Roles = "Admin,Article")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ArticleCreateViewModel model, int id)
        {
            if (!ModelState.IsValid)
                return View(model);

            var article = _articleBusinessService.GetById(id);
            if (article == null)
                return NotFound();
            if (model.Title != article.Title)
            {
                article.Title = model.Title;
                article.Slug = await GenerateUniqueSlugAsync(model.Title);
            }
            article.Summary = model.Summary;
            article.Content = model.Content;
            article.IsPinned = model.IsPinned;
            article.IsPublished = model.IsPublished;
            article.UpdatedAt = DateTime.UtcNow;

            if (model.Image != null && model.Image.Length > 0)
            {
                string relativePath = $"documents/articles/{article.Slug}";
                string filePath = await SaveUploadedFileAsync(model.Image, relativePath, model.Title);
                article.ImageUrl = filePath;
            }


            var result = _articleBusinessService.Update(article);

            if (result.Succeeded)
                return RedirectToAction(nameof(All));

            return View(model);
        }

        [Route("name")]
        public IActionResult Details(string name)
        {
            //if(name!=null)
            //  _memoryCache.Set("slugResearch", name);

            string MotCle;          


            //if (string.IsNullOrWhiteSpace(name))
            //    return NotFound();

            if (string.IsNullOrWhiteSpace(name))
                _memoryCache.TryGetValue("slugResearch", out MotCle);
            else
            {
                _memoryCache.Set("slugResearch", name);
                MotCle = name;
            }
               


            var article = _articleBusinessService.GetSingle(a => a.Slug == MotCle);

            if (article == null || article.IsDeleted || !article.IsPublished)
                return NotFound();

            return View("Details", article);
        }

       

        [Authorize(Roles = "Admin,Article")]
        public IActionResult TogglePublish(int id)
        {
            var article = _articleBusinessService.GetById(id);
            if (article == null)
                return NotFound();

            article.IsPublished = !article.IsPublished;
            article.UpdatedAt = DateTime.Now;

            var result = _articleBusinessService.Update(article);
            return result.Succeeded ? Ok() : StatusCode(500);
        }
        
        [Authorize(Roles = "Admin,Article")]
        public IActionResult Delete(int id)
        {
            var article = _articleBusinessService.GetById(id);
            if (article == null)
                return NotFound();

            article.IsDeleted = true;
            article.UpdatedAt = DateTime.Now;

            var result = _articleBusinessService.Update(article);
            if (result.Succeeded)
                return Ok();

            return StatusCode(500);
        }
        [Authorize(Roles = "Admin,Article")]
        public IActionResult TogglePin(int id)
        {
            var article = _articleBusinessService.GetById(id);
            if (article == null)
                return NotFound();

            article.IsPinned = !article.IsPinned;
            article.UpdatedAt = DateTime.Now;

            var result = _articleBusinessService.Update(article);
            if (result.Succeeded)
                return Ok();

            return StatusCode(500);
        }
        public IActionResult GetAllArticlesDataTable(DataTableAjaxModel model)
        {
            GetDataTableParameters(model, out string search, out string orderBy, out int startRowIndex, out int maxRows);

            if (!string.IsNullOrEmpty(search))
            {
                var result = _articleBusinessService.GetAllFilteredPaged(x => x.Title.Contains(search),
                   orderBy, startRowIndex, maxRows, _articleBusinessService.GetDefaultLoadProperties());


                return Json(new JQueryDataTableRetunedData<Article> { draw = model.draw, recordsFiltered = result.TotalCount, recordsTotal = result.TotalCount, data = result.Items });
            }
            else
            {
                var result = _articleBusinessService.GetAllPaged(orderBy, startRowIndex, maxRows, _articleBusinessService.GetDefaultLoadProperties());


                return Json(new JQueryDataTableRetunedData<Article> { draw = model.draw, recordsFiltered = result.TotalCount, recordsTotal = result.TotalCount, data = result.Items });
            }
        }


        #region helper
        public async Task<string> GenerateUniqueSlugAsync(string title)
        {
            string baseSlug = Slugify(title);
            string slug = baseSlug;
            int i = 1;

            while (_articleBusinessService.GetAllFiltered(a => a.Slug == slug).Any())
            {
                slug = $"{baseSlug}-{i++}";
            }

            return slug;
        }

        private string Slugify(string input)
        {
            input = input.ToLowerInvariant().Trim();
            input = RemoveArabicDiacritics(input);
            input = Regex.Replace(input, @"[^\u0621-\u064Aa-z0-9\s-]", "");
            input = Regex.Replace(input, @"[\s]+", "-");
            input = input.Trim('-');
            return input;
        }

        private string RemoveArabicDiacritics(string text)
        {
            string diacritics = @"[\u0610-\u061A\u064B-\u065F\u0670\u06D6-\u06ED]";
            return Regex.Replace(text, diacritics, "");
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
