namespace BS.Web.Areas.Setup.Controllers
{
    [Area("Setup")]
    public class CurrencyConvRateController : BaseController
    {
        private readonly CurrencyConvRateService CurrencyConvRateS;
        private readonly CurrencyInfoService CurrencyInfoS;
        public CurrencyConvRateController(CurrencyConvRateService _currencyConvRateService, CurrencyInfoService _currencyInfoService)
        {
            CurrencyConvRateS = _currencyConvRateService;
            CurrencyInfoS = _currencyInfoService;
        }
        public IActionResult Index()
        {
            var entityList = CurrencyConvRateS.GetAll();
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
                eQResult = CurrencyConvRateS.Insert(obj, UserId);
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
                var entity = CurrencyConvRateS.GetById(id);
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
            ViewBag.CURRENCY_ID = new SelectList(CurrencyInfoS.GetAllActive(), "ID", "CURRENCY_NAME");
        }
        public IActionResult Delete(string id)
        {
            EQResult eQResult = CurrencyConvRateS.Delete(id);
            return Json(eQResult);
        }
    }
}
