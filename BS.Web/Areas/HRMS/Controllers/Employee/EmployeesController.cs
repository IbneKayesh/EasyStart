namespace BS.Web.Areas.HRMS.Controllers.Employee
{
    [Area("HRMS")]
    public class EmployeesController : BaseController
    {
        private readonly EmployeesService employeesS;
        private readonly DesignationService designationS;
        public EmployeesController(EmployeesService _employeesService, DesignationService _designationService)
        {
            employeesS = _employeesService;
            designationS = _designationService;
        }
        public IActionResult Index()
        {
            var entityList = employeesS.GetAll();
            return View(ViewPathFinder.ViewName(this.GetType(), "Index"), entityList);
        }
        public IActionResult Create()
        {
            Dropdown_CreateEdit();
            return View(ViewPathFinder.ViewName(this.GetType(), "AddUpdate"), new EMPLOYEES());
        }
        [HttpPost]
        public IActionResult AddUpdate(EMPLOYEES obj)
        {
            EQResult eQResult = new EQResult();
            if (ModelState.IsValid)
            {
                eQResult = employeesS.Insert(obj, user_session.USER_ID);
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
            Dropdown_CreateEdit();
            return View(obj);
        }
        public IActionResult Edit(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var entity = employeesS.GetById(id);
                if (entity != null)
                {
                    Dropdown_CreateEdit();
                    return View(ViewPathFinder.ViewName(this.GetType(), "AddUpdate"), entity);
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
            ViewBag.DESIG_ID = designationS.GetAllActive();
        }

        public IActionResult Delete(string id)
        {
            EQResult eQResult = employeesS.Delete(id, user_session.USER_ID);
            return Json(eQResult);
        }

        //API
        [HttpPost]
        public IActionResult FindEmployee(string search_term)
        {
            var obj = employeesS.GetByName(search_term.ToLower());
            return Json(obj);
        }
    }
}
