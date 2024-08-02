namespace BS.Web.Areas.Setup.Controllers
{
    [Area("Setup")]
    public class FinancialYearController : BaseController
    {
        private readonly FinancialYearService financialYearS;
        public FinancialYearController(FinancialYearService _financialYearService)
        {
            financialYearS = _financialYearService;
        }
        public IActionResult Index()
        {
            var entityList = financialYearS.GetAll();
            return View(entityList);
        }
        public IActionResult Create()
        {
            return View("AddUpdate", new FINANCIAL_YEAR());
        }
        [HttpPost]
        public IActionResult AddUpdate(FINANCIAL_YEAR obj)
        {
            EQResult eQResult = new EQResult();
            if (ModelState.IsValid)
            {
                eQResult = financialYearS.Insert(obj, user_session.USER_ID);
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
            ViewData["editmode"] = "readonly";
            if (!string.IsNullOrWhiteSpace(id))
            {
                var entity = financialYearS.GetById(id);
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
            EQResult eQResult = financialYearS.Delete(id);
            return Json(eQResult);
        }
    }
}
