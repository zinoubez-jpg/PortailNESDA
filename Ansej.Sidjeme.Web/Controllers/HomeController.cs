using Ansej.Sidjeme.Business;
using Ansej.Sidjeme.Domain;
using Ansej.Sidjeme.Identity;
using Ansej.Sidjeme.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Ansej.Sidjeme.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ArticleBusinessService _articleBusinessService;
        private readonly ILogger<HomeController> _logger;
        private readonly UserService _userService;
        private readonly ReferentielBusinessService<ContactAgenceWilaya, int> _contactAgenceWilayaBusinessServices;

        public HomeController(ILogger<HomeController> logger, ArticleBusinessService articleBusinessService,
                              UserService userService, ReferentielBusinessService<ContactAgenceWilaya,int> contactAgenceWilayaBusinessServices)
        {
            _logger = logger;
            _userService = userService;
            _contactAgenceWilayaBusinessServices = contactAgenceWilayaBusinessServices;
            _articleBusinessService = articleBusinessService;
        }

        public IActionResult Index()
        {
            var articles = _articleBusinessService.GetAllFiltered(article => article.IsPublished && !article.IsDeleted && article.IsPinned, _articleBusinessService.GetDefaultLoadProperties()).Take(6).ToList();
           


            //Tester
            return View(articles);

        }

        public IActionResult MotDG()
        {


            return View();

        }

        public IActionResult Apropos()
        {


            return View();

        }

        public IActionResult Mission()
        {


            return View();

        }
        public IActionResult Organisation()
        {


            return View();

        }

        public IActionResult Formation()
        {


            return View();

        }

        public IActionResult Accompagnement()
        {


            return View();

        }


        public IActionResult Financement()
        {


            return View();

        }

        public IActionResult Activite()
        {


            return View();

        }
        public IActionResult Fcmg()
        {


            return View();

        }
        public IActionResult Avantage()
        {


            return View();

        }

        public IActionResult RegistrationCDE()
        {


            return View();

        }
        public IActionResult InscriptionDispositif()
        {


            return View();

        }

        public IActionResult InscriptionCNRC()
        {


            return View();

        }

        public IActionResult InscriptionMPDZ()
        {


            return View();

        }
        public IActionResult NesdaExtension()
        {


            return View();

        }

        public IActionResult Impact70()
        {


            return View();

        }
        public IActionResult SousTraitance()
        {


            return View();

        }

        public IActionResult DeclarationHonneurMI()
        {


            return View();

        }
        
        public IActionResult Partenariat()
        {


            return View();

        }

        public IActionResult Cooperation()
        {


            return View();

        }

        public IActionResult Contact()
        {


            return View();

        }

        public IActionResult legislativeTexts()
        {


            return View();

        }

        public IActionResult Organigramme()
        {


            return View();

        }
        public IActionResult Privacy()
        {


            return View();

        }

        [HttpGet]
        public JsonResult GetContactAgencewilayaList(string codeWilaya)
        {
            var contactList = _contactAgenceWilayaBusinessServices
                .GetAll()
                .Where(c => c.CodeWilaya == codeWilaya)
                .Select(c => new
                {
                    c.CodeWilaya,
                    c.Code,
                    c.DesignationAr,
                    c.TelephoneFixe,
                    c.Email,
                    c.Fax,
                    c.AdresseAr,
                    Wilaya = c.CodeWilaya
                })
                .ToList();

            return Json(contactList);
        }



        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }
     
      
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
