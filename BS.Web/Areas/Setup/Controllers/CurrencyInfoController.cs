namespace BS.Web.Areas.Setup.Controllers
{
    [Area("Setup")]
    public class CurrencyInfoController : BaseController
    {
        private readonly CurrencyInfoService currencyInfoS;
        private readonly CountryInfoService countryInfoS;
        public CurrencyInfoController(CurrencyInfoService _currencyInfoService, CountryInfoService _countryInfoService)
        {
            currencyInfoS = _currencyInfoService;
            countryInfoS = _countryInfoService;
        }
        public IActionResult Index()
        {
            var entityList = currencyInfoS.GetAll();
            return View(entityList);
        }
        public IActionResult Create()
        {
            Dropdown_CreateEdit();
            return View("AddUpdate", new CURRENCY_INFO());
        }
        [HttpPost]
        public IActionResult AddUpdate(CURRENCY_INFO obj)
        {
            Dropdown_CreateEdit();
            EQResult eQResult = new EQResult();
            if (ModelState.IsValid)
            {
                eQResult = currencyInfoS.Insert(obj, user_session.USER_ID);
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
                var entity = currencyInfoS.GetById(id);
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
            ViewBag.COUNTRY_ID = new SelectList(countryInfoS.GetAllActive(), "ID", "COUNTRY_NAME");
        }
        public IActionResult Delete(string id)
        {
            EQResult eQResult = currencyInfoS.Delete(id);
            return Json(eQResult);
        }
    }
}
