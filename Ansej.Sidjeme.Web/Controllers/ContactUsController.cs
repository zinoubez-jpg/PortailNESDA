using Ansej.Business.Core;
using Ansej.Sidjeme.Business;
using Ansej.Sidjeme.Domain;
using Ansej.Sidjeme.Domain.Entities;
using Ansej.Sidjeme.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ansej.Sidjeme.Web.Controllers
{
    [Route("[controller]/[action]")]
    public class ContactUsController : Controller
    {
        private readonly ContactUsBusinessService _contactUsBusinessService;
        private readonly WilayaBusinessService _wilayaBusinessService;
        private readonly IRecaptchaService _recaptchaService;

        public ContactUsController(ContactUsBusinessService contactUsBusinessService, WilayaBusinessService wilayaBusinessService, IRecaptchaService recaptchaService)
        {
            this._contactUsBusinessService = contactUsBusinessService;
            this._wilayaBusinessService = wilayaBusinessService;
            _recaptchaService = recaptchaService;

        }
        public IActionResult Index()
        {

            var wilayas = _wilayaBusinessService.GetAll();
            ViewBag.Wilayas = new SelectList(wilayas, nameof(Wilaya.Id), nameof(Wilaya.DisplayNameAr));

          
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(ContactUs model)
        {
            var isRecaptchaValid = _recaptchaService.ValidateRecaptchaAsync(model.RecaptchaResponse).Result;
            if (!isRecaptchaValid)
            {
                ModelState.AddModelError("Recaptcha", "لم تتمكن من اجتياز اختبار الـ CAPTCHA. يرجى المحاولة مرة أخرى.");
                InitializeListViewBags();

                var bR = new BusinessResult();
                var msgR = new MessageResult()
                {
                    Message = "لم تتمكن من اجتياز اختبار الـ CAPTCHA. يرجى المحاولة مرة أخرى.",
                    MessageType = MessageType.Error,
                };

                bR.Messages.Add(msgR);
                TempData["Message"] = bR.ToBootstrapAlerts();

                return View(model);
            }
            if (ModelState.IsValid)
            {
                model.IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
                model.UserAgent = HttpContext.Request.Headers["User-Agent"].ToString();
                model.CreatedAt = DateTime.Now;
                model.IsRead = false;

                var result = _contactUsBusinessService.Add(model);

                if (result.Succeeded)
                {
                    return RedirectToAction("ThankYouPage");
                }

                // En cas d'échec de sauvegarde malgré un modèle valide
                ModelState.AddModelError(string.Empty, "Une erreur s’est produite lors de l’envoi du formulaire. Veuillez réessayer.");
            }

            // Si le modèle est invalide ou l'enregistrement a échoué
            InitializeListViewBags();

            return View(model);
        }

        [Authorize]
        [Route("{id}")]
        public IActionResult MessageTraitement(int id)
        {

            var model = _contactUsBusinessService.GetById(id, _contactUsBusinessService.GetDefaultLoadProperties());
            if (model == null)
                return NotFound();
            return View(model);

        }
        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost("{id}")]
        public virtual IActionResult MessageTraitement(ContactUs model)
        {
            if (ModelState.IsValid)
            {
               
                var businessResult = _contactUsBusinessService.Update(model);
                if (businessResult.Succeeded)
                {
                    TempData["Message"] = businessResult.ToBootstrapAlerts();
                    return RedirectToAction(nameof(MessagesList));
                }
               
                ViewData["Message"] = businessResult.ToBootstrapAlerts();
            }
           
            return View(model);
        }
        public IActionResult ThankYouPage()
        {
            return View();
        }
        [Authorize]
        public IActionResult MessagesList()
        {

            DashboardViewModel dash = new DashboardViewModel()
            {
                NbMessages = _contactUsBusinessService.GetAll().Count,
                NbMessagesNow = _contactUsBusinessService.GetAllFiltered(x => x.CreatedAt.Date == System.DateTime.Today.Date).Count(),
                NbMsgTraites =_contactUsBusinessService.GetAllFiltered(x=>x.IsTraited==true).Count,
                NbMsgEnAttenteDeTraitement = _contactUsBusinessService.GetAllFiltered(x => x.IsTraited == false).Count,


            };

            return View(dash);
        }

        [HttpPost]
        public virtual async Task<IActionResult> DataTableMessages(DataTableAjaxModel model)
        {
      
            GetDataTableParameters(model, out string search, out string orderBy, out int startRowIndex, out int maxRows);
            if (!string.IsNullOrEmpty(search))
            {
               
                    var result = _contactUsBusinessService.GetAllFilteredPaged(x =>( x.FullName.Contains(search) || x.Email.Contains(search) || x.Message.Contains(search) || x.Subject.Contains(search) || x.Wilaya.DesignationAr.Contains(search)) && x.IsTraited==false,
                    orderBy, startRowIndex, maxRows, _contactUsBusinessService.GetDefaultLoadProperties());

                    return Json(new JQueryDataTableRetunedData<ContactUs> { draw = model.draw, recordsFiltered = result.TotalCount, recordsTotal = result.TotalCount, data = result.Items });
               
            }
            else
            {
                
                    var result = _contactUsBusinessService.GetAllFilteredPaged(x=> x.IsTraited == false ,orderBy, startRowIndex, maxRows, _contactUsBusinessService.GetDefaultLoadProperties());
                   
                    return Json(new JQueryDataTableRetunedData<ContactUs> { draw = model.draw, recordsFiltered = result.TotalCount, recordsTotal = result.TotalCount, data = result.Items });

               
            }
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

        private void InitializeListViewBags()
        {
            var wilayas = _wilayaBusinessService.GetAll();
         
            ViewBag.Wilayas = new SelectList(wilayas, nameof(Wilaya.Id), nameof(Wilaya.DisplayNameAr));
        }







    }
}
