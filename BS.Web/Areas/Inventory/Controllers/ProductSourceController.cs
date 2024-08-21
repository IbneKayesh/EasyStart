namespace BS.Web.Areas.Inventory.Controllers
{
    [Area("Inventory")]
    public class ProductSourceController : BaseController
    {
        private readonly ProductSourceService productSourceS;
        public ProductSourceController(ProductSourceService _productSourceService)
        {
            productSourceS = _productSourceService;
        }
        public IActionResult Index()
        {
            var entityList = productSourceS.GetAll();
            return View(entityList);
        }
        public IActionResult Create()
        {
            return View("AddUpdate", new PRODUCT_SOURCE());
        }
        [HttpPost]
        public IActionResult AddUpdate(PRODUCT_SOURCE obj)
        {
            EQResult eQResult = new EQResult();
            if (ModelState.IsValid)
            {
                eQResult = productSourceS.Insert(obj, user_session.USER_ID);
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
            if (!string.IsNullOrWhiteSpace(id))
            {
                var entity = productSourceS.GetById(id);
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
            EQResult eQResult = productSourceS.Delete(id);
            return Json(eQResult);
        }
    }
}
