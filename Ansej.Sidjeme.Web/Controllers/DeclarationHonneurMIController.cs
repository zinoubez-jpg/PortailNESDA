using Ansej.Business.Core;
using Ansej.Sidjeme.Business;
using Ansej.Sidjeme.Domain;
using Ansej.Sidjeme.Domain.Entities;
using Ansej.Sidjeme.Identity;
using Ansej.Sidjeme.Web.Models;
using Ansej.Sidjeme.Web.ViewModels;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
//using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace Ansej.Sidjeme.Web.Controllers
{
    [Route("[controller]/[action]")]
    public class DeclarationHonneurMIController : Controller
    {
        //private readonly UserManager<IdentityUser> _userManager;
        private readonly UserService _userService;

        private readonly DeclarationHonneurMIBusinessService _declarationHonneurMIService;
        private readonly WilayaBusinessService _wilayaBusinessService;
        private readonly ActiviteBusinessService _activiteBusinessService;
        private readonly SecteurActiviteBusinessService _secteurActiviteBusiness;
        private readonly GenreBusinessService _genreBusinessService;
        private readonly FormeJuridiqueBusinessService _formeJuridiqueBusiness;
        private readonly TypeOfNeedBusinessService _typeOfNeedBusiness;
        private readonly StructureManagerBusinessService _structureManagerBusiness;
        private readonly MotifTraitementBusinessService _motifTraitementBusiness;
        private readonly DownloadFileBusinessService _downloadFileBusiness;

       public DeclarationHonneurMIController( //UserManager<IdentityUser> userManager,
                                           UserService userService,
                                              DeclarationHonneurMIBusinessService DeclarationHonneurMIService,
                                              WilayaBusinessService wilayaBusinessService,
                                              ActiviteBusinessService activiteBusinessService,
                                              SecteurActiviteBusinessService secteurActiviteBusiness,
                                              GenreBusinessService genreBusinessService,
                                              FormeJuridiqueBusinessService formeJuridiqueBusiness,
                                              TypeOfNeedBusinessService typeOfNeedBusiness,
                                              StructureManagerBusinessService structureManagerBusiness,
                                              MotifTraitementBusinessService motifTraitementBusiness,
                                              DownloadFileBusinessService downloadFileBusiness
                                              )
        {
            //_userManager = userManager;
            _userService = userService;
            _declarationHonneurMIService = DeclarationHonneurMIService;
            _wilayaBusinessService = wilayaBusinessService;
            _activiteBusinessService = activiteBusinessService;
            _secteurActiviteBusiness = secteurActiviteBusiness;
            _genreBusinessService = genreBusinessService;
            _formeJuridiqueBusiness = formeJuridiqueBusiness;
            _typeOfNeedBusiness = typeOfNeedBusiness;
            _structureManagerBusiness = structureManagerBusiness;
            _motifTraitementBusiness = motifTraitementBusiness;
            _downloadFileBusiness = downloadFileBusiness;


        }

        [Authorize(Roles = "ConsultantDG")]
        public IActionResult DashboardDG()
        {
            
            DashBoardDeclarationHonorDGViewModel dashBoard = new();

            dashBoard.NbrDemandeTotal = _declarationHonneurMIService.GetAll().Count;
            dashBoard.NbrDemandeTraite = _declarationHonneurMIService.GetAllFiltered(x => x.RequestProcessed == true).Count;
            dashBoard.NbrDemandeNonTraite = _declarationHonneurMIService.GetAllFiltered(x => x.RequestProcessed == false).Count;
            dashBoard.NbrDemandeValide = _declarationHonneurMIService.GetAllFiltered(x => x.granted == true && x.RequestProcessed == true).Count;
            dashBoard.NbrDemandeNonValide = _declarationHonneurMIService.GetAllFiltered(x => x.granted == false && x.RequestProcessed == true).Count;
            dashBoard.NbrDemandeValideMC = _declarationHonneurMIService.GetAllFiltered(x => x.granted == true && x.GarantedMC == true && x.RequestProcessed == true).Count;

            var demandeParAgence = _declarationHonneurMIService.GetAll(x => x.StructureManager)
                                     .GroupBy(x => x.StructureManager.NameAr)
                                     .Select(x => new
                                     {
                                         Agence = x.Key,
                                         Nombre = x.Count()
                                     })
                                     .ToList();

            dashBoard.DemandeParAgence = demandeParAgence.Select(d => new DemandeParAgenceDto
            {
                Agence = d.Agence,
                Nombre = d.Nombre
            }).ToList();


            var valideParAgence = _declarationHonneurMIService.GetAllFiltered(x=>x.granted==true && x.RequestProcessed == true,x => x.StructureManager)
                                    .GroupBy(x => x.StructureManager.NameAr)
                                    .Select(x => new
                                    {
                                        Agence = x.Key,
                                         Nombre = x.Count()
                                    })
                                    .ToList();

            dashBoard.ValideParAgence = valideParAgence.Select(d => new DemandeParAgenceDto
            {
                Agence = d.Agence,
                Nombre = d.Nombre
            }).ToList();


            var nonValideParAgence = _declarationHonneurMIService.GetAllFiltered(x=>x.granted==false && x.RequestProcessed == true, x => x.StructureManager)
                                    .GroupBy(x => x.StructureManager.NameAr)
                                    .Select(x => new
                                    {
                                        Agence = x.Key,
                                         Nombre = x.Count()
                                    })
                                    .ToList();

            dashBoard.NonValideParAgence = nonValideParAgence.Select(d => new DemandeParAgenceDto
            {
                Agence = d.Agence,
                Nombre = d.Nombre
            }).ToList();

            var demandeParSecteur = _declarationHonneurMIService.GetAll(x=>x.Activity, x => x.Activity.secteur)
                                     .GroupBy(x => x.Activity.secteur.DesignationAr)
                                     .Select(x => new
                                     {
                                         Secteur = x.Key,//Activity.secteur.,
                                         Nombre = x.Count()
                                     })
                                     .ToList();

            dashBoard.DemandeParSecteur = demandeParSecteur.Select(d => new DemandeParSecteurDto
            {
                Secteur = d.Secteur,
                Nombre = d.Nombre
            }).ToList();

            
            return View(dashBoard);
        }


        [Authorize(Roles = "ConsultantDG")]
        public IActionResult IndexDG()
        {
            return View();
        }

        [Authorize(Roles = "ConsultantDG")]
        [HttpPost]
        public virtual IActionResult DatatableDG(DataTableAjaxModel model)
        {

            GetDataTableParameters(model, out string search, out string orderBy, out int startRowIndex, out int maxRows);

            if (!string.IsNullOrEmpty(search))
            {
                var result = _declarationHonneurMIService.GetAllFilteredPaged(x => x.StructureManager.NameAr.Contains(search) || x.NIN.StartsWith(search) || x.FirstlName.StartsWith(search) || x.LastName.StartsWith(search) || x.ProjectNumber.StartsWith(search),
                    orderBy, startRowIndex, maxRows, x => x.TypeOfNeed, x => x.LegalStatus, x => x.StructureManager);
                return Json(new JQueryDataTableRetunedData<DeclarationHonneurMI> { draw = model.draw, recordsFiltered = result.TotalCount, recordsTotal = result.TotalCount, data = result.Items });
            }
            else
            {
                var result = _declarationHonneurMIService.GetAllPaged(orderBy, startRowIndex, maxRows, x => x.TypeOfNeed, x => x.LegalStatus, x => x.StructureManager);

                return Json(new JQueryDataTableRetunedData<DeclarationHonneurMI> { draw = model.draw, recordsFiltered = result.TotalCount, recordsTotal = result.TotalCount, data = result.Items });

            }



        }


        [Authorize(Roles = "Directeur,ChefServiceAccompagnement,ConsultantDG")]
        public async Task<IActionResult> DashboardAgence(string agence)
        {
            var user = await _userService.GetUserEagerLoadedAsync(User);
            var userStructure = await _userService.GetUserStructureAsync(user.Id);

            if (userStructure.Niveau.Id == 3) // AgenceWilaya
            {
                ViewBag.StructureName = "ملخص";
                   //get structure
                   var structure = userStructure.Designation;

                DashBoardDeclarationHonorDGViewModel dashBoard = new();

                dashBoard.NbrDemandeTotal = _declarationHonneurMIService.GetAllFiltered(x => x.StructureManager.NameFr.Contains(structure)).Count;
                dashBoard.NbrDemandeTraite = _declarationHonneurMIService.GetAllFiltered(x => x.StructureManager.NameFr.Contains(structure) && x.RequestProcessed == true).Count;
                dashBoard.NbrDemandeNonTraite = _declarationHonneurMIService.GetAllFiltered(x => x.StructureManager.NameFr.Contains(structure) && x.RequestProcessed == false).Count;
                dashBoard.NbrDemandeValide = _declarationHonneurMIService.GetAllFiltered(x => x.StructureManager.NameFr.Contains(structure) && x.granted == true && x.RequestProcessed == true).Count;
                dashBoard.NbrDemandeNonValide = _declarationHonneurMIService.GetAllFiltered(x => x.StructureManager.NameFr.Contains(structure) && x.granted == false && x.RequestProcessed == true).Count;
                dashBoard.NbrDemandeValideMC = _declarationHonneurMIService.GetAllFiltered(x => x.StructureManager.NameFr.Contains(structure) && x.granted == true && x.GarantedMC == true && x.RequestProcessed == true).Count;


                var demandeParSecteur = _declarationHonneurMIService.GetAllFiltered(x => x.StructureManager.NameFr.Contains(structure), x => x.Activity, x => x.Activity.secteur)
                                         .GroupBy(x => x.Activity.secteur.DesignationAr)
                                         .Select(x => new
                                         {
                                             Secteur = x.Key,//Activity.secteur.,
                                         Nombre = x.Count()
                                         })
                                         .ToList();

                dashBoard.DemandeParSecteur = demandeParSecteur.Select(d => new DemandeParSecteurDto
                {
                    Secteur = d.Secteur,
                    Nombre = d.Nombre
                }).ToList();
                return View(dashBoard);
            }
            else // DG
            {
                ViewBag.StructureName = _structureManagerBusiness.GetSingle(x => x.NameAr.Contains(agence)).NameAr;
                DashBoardDeclarationHonorDGViewModel dashBoard = new();

                dashBoard.NbrDemandeTotal = _declarationHonneurMIService.GetAllFiltered(x => x.StructureManager.NameAr.Contains(agence)).Count;
                dashBoard.NbrDemandeTraite = _declarationHonneurMIService.GetAllFiltered(x => x.StructureManager.NameAr.Contains(agence) && x.RequestProcessed == true).Count;
                dashBoard.NbrDemandeNonTraite = _declarationHonneurMIService.GetAllFiltered(x => x.StructureManager.NameAr.Contains(agence) && x.RequestProcessed == false).Count;
                dashBoard.NbrDemandeValide = _declarationHonneurMIService.GetAllFiltered(x => x.StructureManager.NameAr.Contains(agence) && x.granted == true && x.RequestProcessed == true).Count;
                dashBoard.NbrDemandeNonValide = _declarationHonneurMIService.GetAllFiltered(x => x.StructureManager.NameAr.Contains(agence) && x.granted == false && x.RequestProcessed == true).Count;
                dashBoard.NbrDemandeValideMC = _declarationHonneurMIService.GetAllFiltered(x => x.StructureManager.NameAr.Contains(agence) && x.granted == true && x.GarantedMC == true && x.RequestProcessed == true).Count;


                var demandeParSecteur = _declarationHonneurMIService.GetAllFiltered(x => x.StructureManager.NameAr.Contains(agence), x => x.Activity, x => x.Activity.secteur)
                                         .GroupBy(x => x.Activity.secteur.DesignationAr)
                                         .Select(x => new
                                         {
                                             Secteur = x.Key,//Activity.secteur.,
                                             Nombre = x.Count()
                                         })
                                         .ToList();

                dashBoard.DemandeParSecteur = demandeParSecteur.Select(d => new DemandeParSecteurDto
                {
                    Secteur = d.Secteur,
                    Nombre = d.Nombre
                }).ToList();
                return View(dashBoard);
            }
            
        }


        [Authorize(Roles = "Directeur,ChefServiceAccompagnement")]
        public IActionResult Index()
        {
           return View();
        }

        [Authorize(Roles = "Directeur,ChefServiceAccompagnement")]
        [HttpPost]
        public virtual async Task<IActionResult> Datatable(DataTableAjaxModel model)
        {


            var user =  await _userService.GetUserEagerLoadedAsync(User);
            var userStructure = await  _userService.GetUserStructureAsync(user.Id);         

           
            //get structure
            var structure = userStructure.Id;



            GetDataTableParameters(model, out string search, out string orderBy, out int startRowIndex, out int maxRows);

            if (!string.IsNullOrEmpty(search))
            {
                var result = _declarationHonneurMIService.GetAllFilteredPaged(x =>x.StructureManagerId==structure && (x.NIN.StartsWith(search) || x.FirstlName.StartsWith(search) || x.LastName.StartsWith(search) || x.ProjectNumber.StartsWith(search)),
                    orderBy, startRowIndex, maxRows, x => x.TypeOfNeed, x => x.LegalStatus);
                return Json(new JQueryDataTableRetunedData<DeclarationHonneurMI> { draw = model.draw, recordsFiltered = result.TotalCount, recordsTotal = result.TotalCount, data = result.Items });
            }
            else
            {
                var result = _declarationHonneurMIService.GetAllFilteredPaged(x => x.StructureManagerId == structure, orderBy, startRowIndex, maxRows, x => x.TypeOfNeed, x => x.LegalStatus);

                return Json(new JQueryDataTableRetunedData<DeclarationHonneurMI> { draw = model.draw, recordsFiltered = result.TotalCount, recordsTotal = result.TotalCount, data = result.Items });

            }



        }

        static void GetDataTableParameters(DataTableAjaxModel model, out string search, out string orderBy, out int startRowIndex, out int maxRows)

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

        public IActionResult Register()
        {
            
            InitializeListViewBags();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegistrantDeclarationHonorViewModel model)
        { 
            
            if (!ModelState.IsValid)
            {
                InitializeListViewBags();
                return View(model);
            }

            List<DownloadFile> DownloadFiles = new();
            //initialiser le chemin du repertoir pour l'inscrit avec le numero du projet afin de charger ces document           
            string relativePath = $"documents/DeclarationHonneurMI/{model.Registrant.ProjectNumber}";
            //enregistrer les fichier et recuperer leurs chemins dans le model
            //IsPdf(model.CertificateNifFile) 
            //verifier si le format est en PDF ou Image avant le chargement du fichier dans le repertoir 
            if (IsPdf(model.CertificateNifFile)|| IsImage(model.CertificateNifFile))
            {
                var f = new DownloadFile
                {
                    TypeFileId = 1,
                    Path = await SaveUploadedFileAsync(model.CertificateNifFile, relativePath, "CertificateNifFile")
                };


                DownloadFiles.Add(f);
            }
            else
            {
                InitializeListViewBags();
                ViewBag.CertificateNifFileError = "يجب أن يكون ملف شهادة تخصيص صندوق الاستثمار الوطني بصيغة PDF أو صورة ويجب ألا يتجاوز الحجم 2 ميجابايت";

                return View(model);
            }


            //verifier si le format est en PDF ou Image avant le chargement du fichier dans le repertoir 
            if (IsPdf(model.BusinessRegisterFile) || IsImage(model.BusinessRegisterFile))
            {
                var f = new DownloadFile
                {
                    TypeFileId = 2,
                    Path = await SaveUploadedFileAsync(model.BusinessRegisterFile, relativePath, "BusinessRegisterFile")
                };

                DownloadFiles.Add(f);
            }
            else
            {
                InitializeListViewBags();
                ViewBag.BusinessRegisteFileError = "يجب أن يكون ملف لسجل التجاري بصيغة PDF أو صورة ويجب ألا يتجاوز الحجم 2 ميجابايت";

                return View(model);
            }

            //verifier si le format est en PDF avant le chargement du fichier dans le repertoir 
            if (IsPdf(model.DeclarationHonorFile))
            {
                var f = new DownloadFile
                {
                    TypeFileId = 3,
                    Path = await SaveUploadedFileAsync(model.DeclarationHonorFile, relativePath, "DeclarationHonorFile")
                };

                DownloadFiles.Add(f);
            }
            else
            {
                InitializeListViewBags();
                ViewBag.DeclarationHonorFileError = " ويجب ألا يتجاوز الحجم 2 ميجاPDF يجب أن يكون ملف التصريح الشرفي بصيغة";
                return View(model);
            }

            //verifier si le format est en PDF avant le chargement du fichier dans le repertoir 
            if (IsPdf(model.ForecastProgramFile))
            {
                var f = new DownloadFile
                {
                    TypeFileId = 4,
                    Path = await SaveUploadedFileAsync(model.ForecastProgramFile, relativePath, "ForecastProgramFile")
                };

                DownloadFiles.Add(f);

            }
            else
            {
                InitializeListViewBags();
                ViewBag.ForecastProgramFileError = " ويجب ألا يتجاوز الحجم 2 ميجاPDF يجب أن يكون ملف البرنامج بصيغة";
                return View(model);
            }
            
            model.Registrant.Requestdate = DateTime.Today;
            model.Registrant.MotifTraitementId = 2;
            model.Registrant.DownloadFiles = DownloadFiles;
            BusinessResult result = _declarationHonneurMIService.Add(model.Registrant);
            if (result.Succeeded)
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("NESDA", "NOREPLY@nesda.dz"));
                message.To.Add(new MailboxAddress(model.Registrant.LastName, model.Registrant.Email));
                message.Subject = "تسجيل";

                message.Body = new TextPart("plain")
                {
                    Text = "(ة)السلام عليكم السيد " + model.Registrant.FirstlName + "  " + model.Registrant.LastName + " \n\n " + " شكراً لتواصلكم معنا — لقد تم تسجيل طلبكم يرجى تقربكم من وكالتنا على مستوى " + _structureManagerBusiness.GetById(model.Registrant.StructureManagerId).NameAr + "  مرفوقين بالبرنامج التقديري للعتاد " + "\n\n" + " تقبلوا منا خالص التحيات"
                };

                using var client = new SmtpClient();
                client.Connect("mail.nesda.dz", 465);//, SecureSocketOptions.StartTls);
                client.Authenticate("NOREPLY@nesda.dz", "14wM_SzQm7zV");
                client.Send(message);
                client.Disconnect(true);

                return RedirectToAction("ThankYouPage");
            }

            else
            {
                ViewData["Message"] = result.ToBootstrapAlerts();               
                return View();
            }

           
        }



        [Authorize(Roles = "Directeur,ChefServiceAccompagnement")]
        public IActionResult Notification(int requestId)
        {
            var dateLimite = DateTime.Today.AddDays(7).ToShortDateString();
            DeclarationHonneurMI request = _declarationHonneurMIService.GetById(requestId,_declarationHonneurMIService.GetDefaultLoadProperties());
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("NESDA", "NOREPLY@nesda.dz"));
                message.To.Add(new MailboxAddress(request.LastName, request.Email));
                message.Subject = "دعوة";

                message.Body = new TextPart("plain")
                {
                    Text = "(ة)السلام عليكم السيد " + request.FirstlName +  "  "  + request.LastName + " \n\n " +  " يرجى منكم التقرب الى " + request.StructureManager.NameAr + "  في أجل أقصاه 7 أيام من تاريخ اليوم (" + dateLimite + ") وذلك لدراسة طلبك لاستراد العتاد و المواد الأولية  " + "\n\n" + " تقبلوا منا خالص التحيات"
                };

                using var client = new SmtpClient();
                client.Connect("mail.nesda.dz", 465);//, SecureSocketOptions.StartTls);
                client.Authenticate("NOREPLY@nesda.dz", "14wM_SzQm7zV");
                client.Send(message);
                client.Disconnect(true);
                request.Notified = true;
                request.NotificationDate = DateTime.Today;
                request.MotifTraitementId = 6;

                var result = _declarationHonneurMIService.Update(request);
                ViewBag.Message = "تم اخطار صاحب المشروع عن طريق البريد الالكتروني بنجاح";
                ViewBag.result = 1;
                
            }
            catch (Exception e)
            {
                ViewBag.Message =" ERROR " + e.Message + ",Please contact Administrator." ;
                ViewBag.result = 0;
                //throw;
            }


            return PartialView($"_{nameof(Notification)}");
        }

        [Authorize(Roles = "Directeur,ChefServiceAccompagnement")]
        public IActionResult Treatment(int requestId)
        {
            DeclarationHonneurMI request = _declarationHonneurMIService.GetById(requestId);

            var motifTraitement = _motifTraitementBusiness.GetAllFiltered(x => x.Id != 1 && x.Id != 2);
            ViewBag.motifTraitement = new SelectList(motifTraitement, nameof(MotifTraitement.Id), nameof(MotifTraitement.DesignationAr));


            return PartialView($"_{nameof(Treatment)}", request);
        }

        [Authorize(Roles = "Directeur,ChefServiceAccompagnement")]
        [HttpPost]
        public IActionResult Treatment(DeclarationHonneurMI request,string decision)
        {
            var model = _declarationHonneurMIService.GetById(request.Id);

            
            if (decision == "1" )
            {
                model.granted = true;
                model.MotifTraitementId = 1;
                model.Observation = request.Observation;
                model.Processingdate = request.Processingdate;
            }
            else
            {
                model.granted = false;
                model.MotifTraitementId = request.MotifTraitementId;
                model.Observation = request.Observation;
             
            }

            model.RequestProcessed = true;
            var result = _declarationHonneurMIService.Update(model);
            List<string> resp = new();
            if (result.Succeeded)
            {

                resp.Add("true");
                resp.Add(model.Id.ToString());
                return Json(resp);
            }
            else
            {
                resp.Add("false");
                resp.Add(result.ToBootstrapAlerts());
                return Json(resp);
            }

          
        }




        [Authorize(Roles = "Directeur,ChefServiceAccompagnement")]
        public IActionResult TreatmentMC(int requestId)
        {
            DeclarationHonneurMI request = _declarationHonneurMIService.GetById(requestId);

            var motifTraitement = _motifTraitementBusiness.GetAllFiltered(x => x.Id != 1 && x.Id != 2);
            ViewBag.motifTraitement = new SelectList(motifTraitement, nameof(MotifTraitement.Id), nameof(MotifTraitement.DesignationAr));


            return PartialView($"_{nameof(TreatmentMC)}", request);
        }

        [Authorize(Roles = "Directeur,ChefServiceAccompagnement")]
        [HttpPost]
        public IActionResult TreatmentMC(DeclarationHonneurMI request, string decisionMC)
        {
            var model = _declarationHonneurMIService.GetById(request.Id);

            if (decisionMC == "1")
                model.GarantedMC = true;
            else
                model.GarantedMC = false;

            model.GarantedMCDate = request.GarantedMCDate;
            model.ObservationMC = request.ObservationMC;


            var result = _declarationHonneurMIService.Update(model);
            List<string> resp = new();
            if (result.Succeeded)
            {

                resp.Add("true");
                resp.Add(model.Id.ToString());
                return Json(resp);
            }
            else
            {
                resp.Add("false");
                resp.Add(result.ToBootstrapAlerts());
                return Json(resp);
            }


        }




        [Authorize(Roles = "Directeur,ChefServiceAccompagnement")]
        [HttpGet]
        public IActionResult Edit(int requestId)
        {
            DeclarationHonneurMI registrant = _declarationHonneurMIService.GetById(requestId, _declarationHonneurMIService.GetDefaultLoadProperties());

            registrant.DownloadFiles = _downloadFileBusiness.GetAllFiltered(x => x.DeclarationHonneurMIId == requestId, x => x.TypeFile); 
            
            InitializeListViewBags();
            ViewBag.activites = GetActivitiesBySector(registrant.Activity.secteurActiviteId);
            ViewBag.activite = registrant.Activity.DesignationAr;
            ViewBag.activiteId = registrant.ActivityId;
             RegistrantDeclarationHonorViewModel model=new();

            model.Registrant = registrant;

            return View(model);
            
        }


        [Authorize(Roles = "Directeur,ChefServiceAccompagnement")]
        [HttpPost]
        public async Task<IActionResult> Edit(RegistrantDeclarationHonorViewModel model)
        {

            var registrant = _declarationHonneurMIService.GetById(model.Registrant.Id);
            registrant.DownloadFiles = _downloadFileBusiness.GetAllFiltered(x => x.DeclarationHonneurMIId == model.Registrant.Id, x => x.TypeFile);

            List<DownloadFile> DownloadFiles = new();
            //initialiser le chemin du repertoir pour l'inscrit avec le numero du projet afin de charger ces document           
            string relativePath = $"documents/DeclarationHonneurMI/{model.Registrant.ProjectNumber}";
           
           
            if (model.CertificateNifFile != null) //si le document est modifié
            {
                if (IsRealPdf(model.CertificateNifFile) || IsImage(model.CertificateNifFile))
                {
                    foreach (var item in registrant.DownloadFiles)
                    {
                        if (item.TypeFileId == 1)
                            item.Path = await SaveUploadedFileAsync(model.CertificateNifFile, relativePath, "CertificateNifFile");
                    }
                }
                else
                {
                    InitializeListViewBags();
                    ViewBag.CertificateNifFileError = "يجب أن يكون ملف شهادة تخصيص صندوق الاستثمار الوطني بصيغة PDF أو صورة ويجب ألا يتجاوز الحجم 2 ميجابايت";

                    return View(model);
                }

            }

            if (model.BusinessRegisterFile != null) //si le document est modifié
            {
                if (IsRealPdf(model.BusinessRegisterFile) || IsImage(model.BusinessRegisterFile))
                {
                    foreach (var item in registrant.DownloadFiles)
                    {
                        if (item.TypeFileId == 2)
                            item.Path = await SaveUploadedFileAsync(model.BusinessRegisterFile, relativePath, "BusinessRegisterFile");
                    }
                }
                else
                {
                    InitializeListViewBags();
                    ViewBag.BusinessRegisteFileError = "يجب أن يكون ملف شهادة تخصيص صندوق الاستثمار الوطني بصيغة PDF أو صورة ويجب ألا يتجاوز الحجم 2 ميجابايت";

                    return View(model);
                }

            }

            if (model.DeclarationHonorFile != null) //si le document est modifié
            {
                if (IsRealPdf(model.DeclarationHonorFile) || IsImage(model.DeclarationHonorFile))
                {
                    foreach (var item in registrant.DownloadFiles)
                    {
                        if (item.TypeFileId == 3)
                            item.Path = await SaveUploadedFileAsync(model.DeclarationHonorFile, relativePath, "DeclarationHonorFile");
                    }
                }
                else
                {
                    InitializeListViewBags();
                    ViewBag.DeclarationHonorFileError = "يجب أن يكون ملف شهادة تخصيص صندوق الاستثمار الوطني بصيغة PDF أو صورة ويجب ألا يتجاوز الحجم 2 ميجابايت";

                    return View(model);
                }

            }

            if (model.ForecastProgramFile != null) //si le document est modifié
            {
                if (IsRealPdf(model.ForecastProgramFile))
                {
                    foreach (var item in registrant.DownloadFiles)
                    {
                        if (item.TypeFileId == 4)
                            item.Path = await SaveUploadedFileAsync(model.ForecastProgramFile, relativePath, "ForecastProgramFile");
                    }
                }
                else
                {
                    InitializeListViewBags();
                    ViewBag.ForecastProgramFileError = "يجب أن يكون ملف شهادة تخصيص صندوق الاستثمار الوطني بصيغة PDF أو صورة ويجب ألا يتجاوز الحجم 2 ميجابايت";

                    return View(model);
                }

            }


            registrant.ActivityId = model.Registrant.ActivityId;
            registrant.Adresse = model.Registrant.Adresse;
            registrant.BusinessRegisterNumber = model.Registrant.BusinessRegisterNumber;
            registrant.CompanyName= model.Registrant.CompanyName;
            registrant.DateOfBirth = model.Registrant.DateOfBirth;
            registrant.Email = model.Registrant.Email;
            registrant.FirstlName = model.Registrant.FirstlName;
            registrant.GenderId = model.Registrant.GenderId;
            registrant.LastName = model.Registrant.LastName;
            registrant.LegalStatusId = model.Registrant.LegalStatusId;
            registrant.NIF = model.Registrant.NIF;
            registrant.NIN = model.Registrant.NIN;
            registrant.Phone = model.Registrant.Phone;
            registrant.ProjectNumber = model.Registrant.ProjectNumber;
            registrant.TypeOfNeedId = model.Registrant.TypeOfNeedId;
              
           
            BusinessResult result = _declarationHonneurMIService.Update(registrant);
            if (result.Succeeded)
            {
                TempData["Message"] = result.ToBootstrapAlerts();
                return RedirectToAction("Index");
            }

            else
            {
                ViewData["Message"] = result.ToBootstrapAlerts();
                return View();
            }

        }



        [Authorize(Roles = "Directeur,ChefServiceAccompagnement")]
        public IActionResult Details(int requestId)
        {
            DeclarationHonneurMI request = _declarationHonneurMIService.GetById(requestId, _declarationHonneurMIService.GetDefaultLoadProperties());

            request.DownloadFiles = _downloadFileBusiness.GetAllFiltered(x => x.DeclarationHonneurMIId == requestId, x => x.TypeFile);

            return PartialView($"_{nameof(Details)}", request);
        }

        public IActionResult GetActivitiesBySector(int sectorId)
        {
            var activites = _activiteBusinessService.GetAllFiltered(x => x.secteurActiviteId == sectorId)
                .Select(a => new {
                    id = a.Id,
                    designationAr = a.DesignationAr,
                    designationEn = a.DesignationEn
                })
                .ToList();

            return Json(activites);
           
        }
       
        public IActionResult ThankYouPage()
        {
            return View();
        }


        public bool IsPdf(IFormFile file)
        {
            // Taille max = 2 Mo
            int maxSize = 2 * 1024 * 1024; // en octets

            if (file == null || file.Length > maxSize)
                return false;

            using (var reader = new BinaryReader(file.OpenReadStream()))
            {
                var buffer = reader.ReadBytes(4);
                var header = System.Text.Encoding.ASCII.GetString(buffer);
                return header == "%PDF";
            }
        }

        public bool IsImage(IFormFile file)
        {
            string[] validExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
            int maxSize = 2 * 1024 * 1024; // en octets

            string extension = Path.GetExtension(file.FileName);

            if (!validExtensions.Contains(extension.ToLower()) || file.Length > maxSize)
            {

                return false;
            }
            return true;
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


        private void InitializeListViewBags()
        {
            var wilayas = _wilayaBusinessService.GetAll();
            var structures = _structureManagerBusiness.GetAllFiltered(x => x.ParentStructureManagerId == 3 && x.Id <= 54);
            var secteurs = _secteurActiviteBusiness.GetAll();
            var formesJuridique = _formeJuridiqueBusiness.GetAll();
            var genre = _genreBusinessService.GetAll();
            var typeOfNeed = _typeOfNeedBusiness.GetAll();
            
            
            ViewBag.Wilayas = new SelectList(wilayas, nameof(Wilaya.Id), nameof(Wilaya.DisplayNameAr));
            ViewBag.structures = new SelectList(structures, nameof(StructureManager.Id), nameof(StructureManager.NameAr));
            ViewBag.secteurs = new SelectList(secteurs, nameof(SecteurActivite.Id), nameof(SecteurActivite.DesignationAr));
            ViewBag.formesJuridique = new SelectList(formesJuridique, nameof(FormeJuridique.Id), nameof(FormeJuridique.DesignationAr));
            ViewBag.genre = new SelectList(genre, nameof(Genre.Id), nameof(Genre.DesignationAr));
            ViewBag.typeOfNeed = new SelectList(typeOfNeed, nameof(TypeOfNeed.Id), nameof(TypeOfNeed.DesignationAr));
            
            ViewBag.WilayasEn = new SelectList(wilayas, nameof(Wilaya.Id), nameof(Wilaya.DisplayNameEn));
            ViewBag.structuresEn = new SelectList(structures, nameof(StructureManager.Id), nameof(StructureManager.NameEn));
            ViewBag.secteursEn = new SelectList(secteurs, nameof(SecteurActivite.Id), nameof(SecteurActivite.DesignationEn));
            ViewBag.secteursFr = new SelectList(secteurs, nameof(SecteurActivite.Id), nameof(SecteurActivite.DesignationFr));
            ViewBag.formesJuridiqueEn = new SelectList(formesJuridique, nameof(FormeJuridique.Id), nameof(FormeJuridique.DesignationEn));
            ViewBag.genreEn = new SelectList(genre, nameof(Genre.Id), nameof(Genre.DesignationEn));
            ViewBag.genreFr = new SelectList(genre, nameof(Genre.Id), nameof(Genre.DesignationFr));
            ViewBag.typeOfNeedEn = new SelectList(typeOfNeed, nameof(TypeOfNeed.Id), nameof(TypeOfNeed.DesignationEn));
            ViewBag.typeOfNeedFr = new SelectList(typeOfNeed, nameof(TypeOfNeed.Id), nameof(TypeOfNeed.DesignationFr));

        }


        public string CreateFile(string nomRepertoir)
        {
            string wwwrootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

            string absoluteFolderPath = Path.Combine(wwwrootPath, @"documents\DeclarationHonneurMI" + nomRepertoir);

            string path = "";

            // Vérifie si le répertoire existe déjà pour éviter les exceptions
            if (!Directory.Exists(absoluteFolderPath))
            {
                // Crée le répertoire
                Directory.CreateDirectory(absoluteFolderPath);
                path = @"documents\DeclarationHonneurMI\" + nomRepertoir;
            }

            return path;

        }

        //check if a file is a real pdf file

        public bool IsRealPdf(IFormFile file)
        {
            // Check for basic conditions first
            if (file == null || file.Length == 0)
            {
                return false;
            }

            // Define the PDF magic bytes
            byte[] pdfMagicBytes = Encoding.ASCII.GetBytes("%PDF-");

            // Read the first few bytes of the file
            using (var stream = file.OpenReadStream())
            {
                byte[] fileHeader = new byte[pdfMagicBytes.Length];
                stream.Read(fileHeader, 0, pdfMagicBytes.Length);

                // Compare the read bytes with the magic bytes
                for (int i = 0; i < pdfMagicBytes.Length; i++)
                {
                    if (fileHeader[i] != pdfMagicBytes[i])
                    {
                        return false; // Not a PDF
                    }
                }
            }

            // At this point, it's a real PDF file
            return true;
        }



        [Authorize(Roles = "Directeur,ChefServiceAccompagnement,ConsultantDG")]
        public async Task<IActionResult> DownloadExcelFile()
        {
            var workbook = new XLWorkbook();
            IXLWorksheet worksheet = workbook.Worksheets.Add("InscritsMaterielImporte");
            worksheet.Range("D5:AC5").AddConditionalFormat()
                     .WhenGreaterThan(20000).Fill.SetBackgroundColor(XLColor.LightGreen);

            worksheet.Columns("3").Width = 20;
            worksheet.Columns("4").Width = 15;
            worksheet.Columns("5").Width = 20;
            worksheet.Columns("6").Width = 20;
            worksheet.Columns("7").Width = 20;
            worksheet.Columns("8").Width = 20;
            worksheet.Columns("9").Width = 25;
            worksheet.Columns("10").Width = 40;
            worksheet.Columns("11").Width = 20;
            worksheet.Columns("12").Width = 20;
            worksheet.Columns("13").Width = 80;
            worksheet.Columns("14").Width = 20;
            worksheet.Columns("15").Width = 30;
            worksheet.Columns("16").Width = 20;
            worksheet.Columns("17").Width = 10;
            worksheet.Columns("18").Width = 20;
            worksheet.Columns("19").Width = 10;
            worksheet.Columns("20").Width = 10;
            worksheet.Columns("21").Width = 30;
            worksheet.Columns("22").Width = 20;
            worksheet.Columns("23").Width = 20;
            worksheet.Columns("24").Width = 25;
            worksheet.Columns("25").Width = 15;
            worksheet.Columns("26").Width = 15;
            worksheet.Columns("27").Width = 15;
            worksheet.Columns("28").Width = 15;
            worksheet.Columns("29").Width = 30;


            worksheet.Cell(3, 8).Value = "قائمة المسجلين لطلبات المعدات المستوردة";
            worksheet.Cell(3, 8).Style.Font.FontSize = 20;


            worksheet.Cell(5, 4).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
            worksheet.Cell(5, 4).Value = "الوكالة الولائية";

            worksheet.Cell(5, 5).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
            worksheet.Cell(5, 5).Value = "رقم الملف";

            worksheet.Cell(5, 6).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
            worksheet.Cell(5, 6).Value = "اللقب";

            worksheet.Cell(5, 7).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
            worksheet.Cell(5, 7).Value = "الاسم";

            worksheet.Cell(5, 8).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
            worksheet.Cell(5, 8).Value = "تاريخ الميلاد";

            worksheet.Cell(5, 9).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
            worksheet.Cell(5, 9).Value = "الجنس";

            worksheet.Cell(5, 10).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
            worksheet.Cell(5, 10).Value = "NIN";

            worksheet.Cell(5, 11).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
            worksheet.Cell(5, 11).Value = "اسم المؤسسة";

            worksheet.Cell(5, 12).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
            worksheet.Cell(5, 12).Value = "رقم السجل التجاري ";

            worksheet.Cell(5, 13).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
            worksheet.Cell(5, 13).Value = "NIF";

            worksheet.Cell(5, 14).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
            worksheet.Cell(5, 14).Value = "عنوان المؤسسة";

            worksheet.Cell(5, 15).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
            worksheet.Cell(5, 15).Value = "الهاتف";

            worksheet.Cell(5, 16).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
            worksheet.Cell(5, 16).Value = "البريد الالكتروني";

            worksheet.Cell(5, 17).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
            worksheet.Cell(5, 17).Value = "تاريخ الطلب";

            worksheet.Cell(5, 18).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
            worksheet.Cell(5, 18).Value = "نوع الاحتياج";

            worksheet.Cell(5, 19).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
            worksheet.Cell(5, 19).Value = " معالج؟";

            worksheet.Cell(5, 20).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
            worksheet.Cell(5, 20).Value = "موافقة الوكالة؟";

            worksheet.Cell(5, 21).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
            worksheet.Cell(5, 21).Value = "تاريخ الموافقة";

            worksheet.Cell(5, 22).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
            worksheet.Cell(5, 22).Value = "النشاط";

            worksheet.Cell(5, 23).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
            worksheet.Cell(5, 23).Value = "سبب عدم الموافقة";

            worksheet.Cell(5, 24).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
            worksheet.Cell(5, 24).Value = "ملاحظة";

            worksheet.Cell(5, 25).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
            worksheet.Cell(5, 25).Value = "تم اخطاره؟";

            worksheet.Cell(5, 26).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
            worksheet.Cell(5, 26).Value = "تاريخ الاخطار";

            worksheet.Cell(5, 27).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
            worksheet.Cell(5, 27).Value = "رد الوزارة";

            worksheet.Cell(5, 28).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
            worksheet.Cell(5, 28).Value = "تاريخ الرد";

            worksheet.Cell(5, 29).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
            worksheet.Cell(5, 29).Value = "ملاحظة الوزارة";

            var user = await _userService.GetUserEagerLoadedAsync(User);
            var userStructure = await _userService.GetUserStructureAsync(user.Id);


            //get structure
            var structure = userStructure.Id;
            List<DeclarationHonneurMI> list = new();

            if (userStructure.Niveau.Id == 3)
                list = _declarationHonneurMIService.GetAllFiltered(x => x.StructureManagerId == structure, _declarationHonneurMIService.GetDefaultLoadProperties());
            else
                list = _declarationHonneurMIService.GetAll(_declarationHonneurMIService.GetDefaultLoadProperties());



            for (int index = 1; index <= list.Count; index++)
            {

                worksheet.Cell(index + 5, 4).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
                worksheet.Cell(index + 5, 4).Value = list[index - 1].StructureManager.NameAr;

                worksheet.Cell(index + 5, 5).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
                worksheet.Cell(index + 5, 5).Value = list[index - 1].ProjectNumber;

                worksheet.Cell(index + 5, 6).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
                worksheet.Cell(index + 5, 6).Value = list[index - 1].FirstlName;

                worksheet.Cell(index + 5, 7).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
                worksheet.Cell(index + 5, 7).Value = list[index - 1].LastName;

                worksheet.Cell(index + 5, 8).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
                worksheet.Cell(index + 5, 8).Value = list[index - 1].DateOfBirth;

                worksheet.Cell(index + 5, 9).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
                worksheet.Cell(index + 5, 9).Value = list[index - 1].Gender.DesignationAr;

                worksheet.Cell(index + 5, 10).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
                worksheet.Cell(index + 5, 10).Value = list[index - 1].NIN;

                worksheet.Cell(index + 5, 11).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
                worksheet.Cell(index + 5, 11).Value = list[index - 1].CompanyName;

                worksheet.Cell(index + 5, 12).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
                worksheet.Cell(index + 5, 12).Value = list[index - 1].BusinessRegisterNumber;

                worksheet.Cell(index + 5, 13).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
                worksheet.Cell(index + 5, 13).Value = list[index - 1].NIF;

                worksheet.Cell(index + 5, 14).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
                worksheet.Cell(index + 5, 14).Value = list[index - 1].Adresse + " - " + list[index - 1].Wilaya.DesignationAr;

                worksheet.Cell(index + 5, 15).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
                worksheet.Cell(index + 5, 15).Value = list[index - 1].Phone;

                worksheet.Cell(index + 5, 16).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
                worksheet.Cell(index + 5, 16).Value = list[index - 1].Email;

                worksheet.Cell(index + 5, 17).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
                worksheet.Cell(index + 5, 17).Value = list[index - 1].Requestdate.Date;



                worksheet.Cell(index + 5, 18).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
                worksheet.Cell(index + 5, 18).Value = list[index - 1].TypeOfNeed.DesignationAr;

                worksheet.Cell(index + 5, 19).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
                if (list[index - 1].RequestProcessed == true)
                    worksheet.Cell(index + 5, 19).Value = "نعم";
                else
                    worksheet.Cell(index + 5, 19).Value = "لا";

                worksheet.Cell(index + 5, 20).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
                if (list[index - 1].granted == true)
                    worksheet.Cell(index + 5, 20).Value = "نعم";
                else
                    worksheet.Cell(index + 5, 20).Value = "لا";


                worksheet.Cell(index + 5, 21).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
                if (list[index - 1].RequestProcessed == true)
                    worksheet.Cell(index + 5, 21).Value = list[index - 1].Processingdate.Day + "/" + list[index - 1].Processingdate.Month + "/" + list[index - 1].Processingdate.Year;
                else
                    worksheet.Cell(index + 5, 21).Value = "/";


                worksheet.Cell(index + 5, 22).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
                worksheet.Cell(index + 5, 22).Value = list[index - 1].Activity.DesignationAr;


                if (list[index - 1].MotifTraitementId != null)
                {
                    worksheet.Cell(index + 5, 23).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
                    worksheet.Cell(index + 5, 23).Value = list[index - 1].MotifTraitement.DesignationAr;
                }


                worksheet.Cell(index + 5, 24).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
                worksheet.Cell(index + 5, 24).Value = list[index - 1].Observation;


                if (list[index - 1].Notified == true)
                {
                    worksheet.Cell(index + 5, 25).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
                    worksheet.Cell(index + 5, 25).Value = "نعم";

                    worksheet.Cell(index + 5, 26).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
                    worksheet.Cell(index + 5, 26).Value = list[index - 1].NotificationDate;
                }
                else
                {
                    worksheet.Cell(index + 5, 25).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
                    worksheet.Cell(index + 5, 25).Value = "لا";

                    worksheet.Cell(index + 5, 26).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
                    worksheet.Cell(index + 5, 26).Value = "/";
                }

                worksheet.Cell(index + 5, 27).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
                if (list[index - 1].granted == true)
                {
                    if (list[index - 1].GarantedMC == true)
                           worksheet.Cell(index + 5, 27).Value = "مقبول";
                    else
                          worksheet.Cell(index + 5, 27).Value = "غير مقبول";
                }
                 else
                    worksheet.Cell(index + 5, 27).Value = "/";



            worksheet.Cell(index + 5, 28).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
                if (list[index - 1].GarantedMCDate != null)
                    worksheet.Cell(index + 5, 28).Value = list[index - 1].GarantedMCDate;
                else
                    worksheet.Cell(index + 5, 28).Value = "/";


                worksheet.Cell(index + 5, 29).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
                if (list[index - 1].ObservationMC != null)
                    worksheet.Cell(index + 5, 29).Value = list[index - 1].ObservationMC;
                else
                    worksheet.Cell(index + 5, 29).Value = "/";
            }

            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileName = "ListeMaterielImporte_" + DateTime.Now.ToShortDateString() + ".xlsx";
            using (var stream = new MemoryStream())
            {
                workbook.SaveAs(stream);
                var content = stream.ToArray();
                return File(content, contentType, fileName);
            }
        }

    }
    }
