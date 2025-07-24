using Ansej.Business.Core;
using Ansej.Sidjeme.Business;
using Ansej.Sidjeme.Domain;
using Ansej.Sidjeme.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ansej.Sidjeme.Web.Controllers
{
    [Route("[controller]/[action]")]
    public class PreInscriptionSousTraitenceController : Controller
    {
        private readonly WilayaBusinessService _wilayaBusinessService;
        private readonly PreInscriptionSousTraitenceBusinessService _preInscriptionSousTraitenceBusinessService;
        private readonly ReferentielBusinessService<Activite, int> _activiteBusinessService;
        private readonly ReferentielBusinessService<PerimetreActivite, int> _perimetreActiviteBusinessService;
        private readonly IRecaptchaService _recaptchaService;
        public PreInscriptionSousTraitenceController(WilayaBusinessService wilayaBusinessService, PreInscriptionSousTraitenceBusinessService preInscriptionSousTraitenceBusinessService, ReferentielBusinessService<Activite, int> activiteBusinessService , ReferentielBusinessService<PerimetreActivite, int> perimetreActiviteBusinessService, IRecaptchaService recaptchaService)
        {
            _wilayaBusinessService = wilayaBusinessService;
            _preInscriptionSousTraitenceBusinessService = preInscriptionSousTraitenceBusinessService;
            _activiteBusinessService = activiteBusinessService;
            _perimetreActiviteBusinessService = perimetreActiviteBusinessService;
            _recaptchaService = recaptchaService;

        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {

            InitializeListViewBags();


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(PreInscriptionSousTraitence model)
        
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

            model.DateInscription = DateTime.Now;

            if (ModelState.IsValid)
            {
                var businessResult = _preInscriptionSousTraitenceBusinessService.Add(model);
                if (businessResult.Succeeded)
                {
                   

                    return RedirectToAction("ThankYouPage");
                }
                TempData["Message"] = businessResult.ToBootstrapAlerts();

                InitializeListViewBags();
            }
            InitializeListViewBags();
            return View(model);
        }
        public IActionResult ThankYouPage()
        {
            return View();
        }
        private void InitializeListViewBags()
        {
            var wilayas = _wilayaBusinessService.GetAll();
            var activites = _activiteBusinessService.GetAll();
            var perimetres = _perimetreActiviteBusinessService.GetAll();
            ViewBag.WilayaId = new SelectList(wilayas, nameof(Wilaya.Id), nameof(Wilaya.DisplayNameAr));
            ViewBag.ActiviteId = new SelectList(activites, nameof(Activite.Id), nameof(Activite.DesignationAr));
            ViewBag.PerimetreId = new SelectList(perimetres, nameof(PerimetreActivite.Id), nameof(Wilaya.DesignationAr));
        }


    }
}
