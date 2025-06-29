using Ansej.Sidjeme.Identity;

using Ansej.Sidjeme.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ansej.Sidjeme.Web.Controllers
{
    [Authorize(Roles = "Admin,AdminCPNational,DelegueLocal,AdminCP")]
    public class StructureController : Controller
    {
        private readonly StructureService _structureService;
        private readonly UserService _userService;

        public StructureController(StructureService structureService, UserService userService)
        {
            _structureService = structureService;
            _userService = userService;
        }
        public async Task<IActionResult> Index()
        {
            List<Structure> model = new List<Structure>();
            return View(model);
        }

        public virtual async Task<IActionResult> Details(int id)
        {

            var model = await _userService.GetUsersByStructuresAsync(id);

            foreach (var item in model)
            {
                var user = _userService.GetById(item.UserId, _userService.GetDefaultLoadProperties());

                item.User = user;


            }




            if (model == null)
                return NotFound();
            return View(model);

        }




        [HttpPost]
        public virtual async Task<IActionResult> DataTable(DataTableAjaxModel model)
        {
            var user = await _userService.GetUserEagerLoadedAsync(User);
            var structure = await _userService.GetStructureFromUserAsync(user.Id);


            GetDataTableParameters(model, out string search, out string orderBy, out int startRowIndex, out int maxRows);

            if (!string.IsNullOrEmpty(search))
            {
                if (structure.Designation == "DG" || structure.Designation == "CP BNational")
                {
                    var result = _structureService.GetAllFilteredPaged(x => x.Designation.Contains(search) || x.CodeStructure.StartsWith(search),
                   orderBy, startRowIndex, maxRows, _structureService.GetDefaultLoadProperties());
                    return Json(new JQueryDataTableRetunedData<Structure> { draw = model.draw, recordsFiltered = result.TotalCount, recordsTotal = result.TotalCount, data = result.Items });

                }
                else
                {
                    var result = _structureService.GetAllFilteredPaged(x => (x.Designation.Contains(search) || x.CodeStructure.StartsWith(search)) && x.CodeStructure == structure.CodeStructure,
                   orderBy, startRowIndex, maxRows, _structureService.GetDefaultLoadProperties());
                    return Json(new JQueryDataTableRetunedData<Structure> { draw = model.draw, recordsFiltered = result.TotalCount, recordsTotal = result.TotalCount, data = result.Items });


                }
            }
            else
            {
                if (structure.Designation == "DG" || structure.Designation == "CP BNational")
                {
                    var result = _structureService.GetAllPaged(orderBy, startRowIndex, maxRows, _structureService.GetDefaultLoadProperties());

                    return Json(new JQueryDataTableRetunedData<Structure> { draw = model.draw, recordsFiltered = result.TotalCount, recordsTotal = result.TotalCount, data = result.Items });

                }
                else
                {
                    var result = _structureService.GetAllFilteredPaged(x => x.CodeStructure == structure.CodeStructure, orderBy, startRowIndex, maxRows, _structureService.GetDefaultLoadProperties());

                    return Json(new JQueryDataTableRetunedData<Structure> { draw = model.draw, recordsFiltered = result.TotalCount, recordsTotal = result.TotalCount, data = result.Items });

                }

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





    }
}
