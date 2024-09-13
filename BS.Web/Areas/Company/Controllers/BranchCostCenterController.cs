namespace BS.Web.Areas.Company.Controllers
{
    [Area("Company")]
    public class BranchCostCenterController : BaseController
    {
        private readonly BranchCostCenterService branchCostCenterS;
        private readonly BranchService branchS;
        private readonly BankBranchService bankBranchS;
        public BranchCostCenterController(BranchCostCenterService _branchCostCenterService,
            BranchService _branchService,
            BankBranchService _bankBranchService
            )
        {
            branchCostCenterS = _branchCostCenterService;
            branchS = _branchService;
            bankBranchS = _bankBranchService;
        }

        public IActionResult Index()
        {
            var entityList = branchCostCenterS.GetAll();
            return View(entityList);
        }
        public IActionResult Create()
        {
            Dropdown_CreateEdit();
            return View("AddUpdate", new BRANCH_COST_CENTER());
        }
        [HttpPost]
        public IActionResult AddUpdate(BRANCH_COST_CENTER obj)
        {
            Dropdown_CreateEdit();
            EQResult eQResult = new EQResult();
            if (ModelState.IsValid)
            {
                eQResult = branchCostCenterS.Insert(obj, user_session.USER_ID);
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
                var entity = branchCostCenterS.GetById(id);
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
            ViewBag.BANK_BRANCH_ID = new SelectList(bankBranchS.GetAllActive(), "ID", "BRANCH_NAME");
        }
        public IActionResult Delete(string id)
        {
            EQResult eQResult = branchCostCenterS.Delete(id);
            return Json(eQResult);
        }
    }
}
