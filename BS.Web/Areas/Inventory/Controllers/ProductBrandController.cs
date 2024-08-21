namespace BS.Web.Areas.Inventory.Controllers
{
    [Area("Inventory")]
    public class ProductBrandController : BaseController
    {
        private readonly ProductBrandService productBrandS;
        private readonly CountryInfoService countryInfoS;
        public ProductBrandController(ProductBrandService _productBrandService, CountryInfoService _countryInfoService)
        {
            productBrandS = _productBrandService;
            countryInfoS = _countryInfoService;
        }
        public IActionResult Index()
        {
            var entityList = productBrandS.GetAll();
            return View(entityList);
        }
        public IActionResult Create()
        {
            Dropdown_CreateEdit();
            return View("AddUpdate", new PRODUCT_BRAND());
        }
        [HttpPost]
        public IActionResult AddUpdate(PRODUCT_BRAND obj)
        {
            Dropdown_CreateEdit();
            EQResult eQResult = new EQResult();
            if (ModelState.IsValid)
            {
                eQResult = productBrandS.Insert(obj, user_session.USER_ID);
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
                var entity = productBrandS.GetById(id);
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
            EQResult eQResult = productBrandS.Delete(id);
            return Json(eQResult);
        }
    }
}
