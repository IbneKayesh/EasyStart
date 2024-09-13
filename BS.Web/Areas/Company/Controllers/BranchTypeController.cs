namespace BS.Web.Areas.Company.Controllers
{
    [Area("Company")]
    public class BranchTypeController : BaseController
    {
        private readonly BranchTypeService branchTypeS;
        public BranchTypeController(BranchTypeService _branchTypeService)
        {
            branchTypeS = _branchTypeService;
        }
        public IActionResult Index()
        {
            var entityList = branchTypeS.GetAll();
            return View(entityList);
        }
        public IActionResult Create()
        {
            return View("AddUpdate", new BRANCH_TYPE());
        }
        [HttpPost]
        public IActionResult AddUpdate(BRANCH_TYPE obj)
        {
            EQResult eQResult = new EQResult();
            if (ModelState.IsValid)
            {
                eQResult = branchTypeS.Insert(obj, user_session.USER_ID);
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
            if (!string.IsNullOrWhiteSpace(id))
            {
                var entity = branchTypeS.GetById(id);
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
        public IActionResult Delete(string id)
        {
            EQResult eQResult = branchTypeS.Delete(id);
            return Json(eQResult);
        }
    }
}
