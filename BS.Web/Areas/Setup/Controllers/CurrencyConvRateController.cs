namespace BS.Web.Areas.Setup.Controllers
{
    [Area("Setup")]
    public class CurrencyConvRateController : BaseController
    {
        private readonly CurrencyConvRateService currencyConvRateS;
        private readonly CurrencyInfoService currencyInfoS;
        public CurrencyConvRateController(CurrencyConvRateService _currencyConvRateService, CurrencyInfoService _currencyInfoService)
        {
            currencyConvRateS = _currencyConvRateService;
            currencyInfoS = _currencyInfoService;
        }
        public IActionResult Index()
        {
            var entityList = currencyConvRateS.GetAll();
            return View(entityList);
        }
        public IActionResult Create()
        {
            Dropdown_CreateEdit();
            return View("AddUpdate", new CURRENCY_CONV_RATE());
        }
        [HttpPost]
        public IActionResult AddUpdate(CURRENCY_CONV_RATE obj)
        {
            Dropdown_CreateEdit();
            EQResult eQResult = new EQResult();
            if (ModelState.IsValid)
            {
                eQResult = currencyConvRateS.Insert(obj, user_session.USER_ID);
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
                var entity = currencyConvRateS.GetById(id);
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
            ViewBag.CURRENCY_ID = new SelectList(currencyInfoS.GetAllActive(), "ID", "CURRENCY_NAME");
        }
        public IActionResult Delete(string id)
        {
            EQResult eQResult = currencyConvRateS.Delete(id);
            return Json(eQResult);
        }
    }
}
