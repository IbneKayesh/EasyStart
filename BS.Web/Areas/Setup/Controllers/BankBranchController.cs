namespace BS.Web.Areas.Setup.Controllers
{
    [Area("Setup")]
    public class BankBranchController : BaseController
    {
        private readonly BankBranchService bankBranchS;
        private readonly BankInfoService bankInfoS;
        public BankBranchController(BankBranchService _bankBranchService, BankInfoService _bankInfoService)
        {
            bankBranchS = _bankBranchService;
            bankInfoS = _bankInfoService;
        }
        public IActionResult Index(string bank)
        {
            var entityList = bankBranchS.GetAll(bank);
            return View(entityList);
        }
        public IActionResult Create()
        {
            Dropdown_CreateEdit();
            return View("AddUpdate", new BANK_BRANCH());
        }
        [HttpPost]
        public IActionResult AddUpdate(BANK_BRANCH obj)
        {
            EQResult eQResult = new EQResult();
            if (ModelState.IsValid)
            {
                eQResult = bankBranchS.Insert(obj, user_session.USER_ID);
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
            Dropdown_CreateEdit();
            return View(obj);
        }
        public IActionResult Edit(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var entity = bankBranchS.GetById(id);
                if (entity != null)
                {
                    Dropdown_CreateEdit();
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
            ViewBag.BANK_ID = bankInfoS.GetAllActive();
        }

        public IActionResult Delete(string id)
        {
            EQResult eQResult = bankBranchS.Delete(id);
            return Json(eQResult);
        }
    }
}
