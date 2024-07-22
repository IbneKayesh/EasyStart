namespace BS.Web.Areas.Company.Controllers
{
    [Area("Company")]
    public class SectionController : BaseController
    {
        private readonly DepartmentService departmentS;
        private readonly SectionService sectionS;
        public SectionController(DepartmentService _departmentService, SectionService _sectionService)
        {
            departmentS = _departmentService;
            sectionS = _sectionService;
        }
        public IActionResult Index()
        {
            var entityList = sectionS.GetAll();
            return View(entityList);
        }
        public IActionResult Create()
        {
            Dropdown_CreateEdit();
            return View("AddUpdate", new SECTIONS());
        }
        [HttpPost]
        public IActionResult AddUpdate(SECTIONS obj)
        {
            Dropdown_CreateEdit();
            EQResult eQResult = new EQResult();
            if (ModelState.IsValid)
            {
                eQResult = sectionS.Insert(obj, user_session.USER_ID);
                TempData["msg"] = eQResult.messages;

                if (eQResult.success && eQResult.rows > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                var errors = UtilityService.GET_MODEL_ERRORS(ModelState);
                ModelState.AddModelError("", errors);
            }
            return View(obj);
        }
        public IActionResult Edit(string id)
        {
            Dropdown_CreateEdit();
            if (!string.IsNullOrWhiteSpace(id))
            {
                var entity = sectionS.GetById(id);
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
            ViewBag.DEPARTMENT_ID = new SelectList(departmentS.GetAllActive(), "ID", "DEPARTMENT_NAME");
        }
        public IActionResult Delete(string id)
        {
            EQResult eQResult = sectionS.Delete(id);
            return Json(eQResult);
        }
    }
}
