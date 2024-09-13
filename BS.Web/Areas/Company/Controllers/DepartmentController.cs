using BS.DMO.Models.Setup;

namespace BS.Web.Areas.Company.Controllers
{
    [Area("Company")]
    public class DepartmentController : BaseController
    {
        private readonly DepartmentService departmentS;
        private readonly BranchService branchS;
        public DepartmentController(DepartmentService _departmentService, BranchService _branchService)
        {
            departmentS = _departmentService;
            branchS = _branchService;
        }
        public IActionResult Index()
        {
            var entityList = departmentS.GetAll();
            return View(entityList);
        }
        public IActionResult Create()
        {
            Dropdown_CreateEdit();
            return View("AddUpdate", new DEPARTMENTS());
        }
        [HttpPost]
        public IActionResult AddUpdate(DEPARTMENTS obj)
        {
            Dropdown_CreateEdit();
            EQResult eQResult = new EQResult();
            if (ModelState.IsValid)
            {
                eQResult = departmentS.Insert(obj, user_session.USER_ID);
                TempData["msg"] = eQResult.messages;

                if (eQResult.success && eQResult.rows > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                var errors = ValidateModelData.GET_MODEL_ERRORS(ModelState);
                ModelState.AddModelError("", errors);
            }
            return View(obj);
        }
        public IActionResult Edit(string id)
        {
            Dropdown_CreateEdit();
            if (!string.IsNullOrWhiteSpace(id))
            {
                var entity = departmentS.GetById(id);
                if (entity != null)
                {
                    return View("AddUpdate", entity);
                }
                else
                {
                    TempData["msg"] = NotifyService.NotFound();
                }
            }
            else
            {
                TempData["msg"] = NotifyService.Error("Invalid ID, Parameter is required");
            }
            return RedirectToAction(nameof(Index));
        }
        private void Dropdown_CreateEdit()
        {
            ViewBag.BRANCH_ID = new SelectList(branchS.GetAllActive(), "ID", "BRANCH_NAME");
        }
        public IActionResult Delete(string id)
        {
            EQResult eQResult = departmentS.Delete(id);
            return Json(eQResult);
        }
    }
}
