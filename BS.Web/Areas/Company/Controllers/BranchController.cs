namespace BS.Web.Areas.Company.Controllers
{
    [Area("Company")]
    public class BranchController : BaseController
    {
        private readonly BranchService branchS;
        private readonly BranchTypeService branchTypeS;
        private readonly BusinessService businessS;
        public BranchController(BranchService _branchService,
            BranchTypeService _branchTypeService,
            BusinessService _businessService
            )
        {
            branchS = _branchService;
            branchTypeS = _branchTypeService;
            businessS = _businessService;
        }
        public IActionResult Index()
        {
            var entityList = branchS.GetAll();
            return View(entityList);
        }
        public IActionResult Create()
        {
            Dropdown_CreateEdit();
            return View("AddUpdate", new BRANCH());
        }
        [HttpPost]
        public IActionResult AddUpdate(BRANCH obj)
        {
            Dropdown_CreateEdit();
            EQResult eQResult = new EQResult();
            if (ModelState.IsValid)
            {
                eQResult = branchS.Insert(obj, user_session.USER_ID);
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
                var entity = branchS.GetById(id);
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
            ViewBag.BRANCH_TYPE_ID = new SelectList(branchTypeS.GetAllActive(), "ID", "BRANCH_TYPE_NAME");
            ViewBag.BUSINESS_ID = new SelectList(businessS.GetAllActive(), "ID", "BUSINESS_NAME");
        }
        public IActionResult Delete(string id)
        {
            EQResult eQResult = branchS.Delete(id);
            return Json(eQResult);
        }
    }
}
