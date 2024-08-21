namespace BS.Web.Areas.Inventory.Controllers
{
    [Area("Inventory")]
    public class ProductCategoryController : BaseController
    {
        private readonly ProductCategoryService productCategoryS;
        public ProductCategoryController(ProductCategoryService _productCategoryService)
        {
            productCategoryS = _productCategoryService;
        }
        public IActionResult Index()
        {
            var entityList = productCategoryS.GetAll();
            return View(entityList);
        }
        public IActionResult Create()
        {
            return View("AddUpdate", new PRODUCT_CATEGORY());
        }
        [HttpPost]
        public IActionResult AddUpdate(PRODUCT_CATEGORY obj)
        {
            EQResult eQResult = new EQResult();
            if (ModelState.IsValid)
            {
                eQResult = productCategoryS.Insert(obj, user_session.USER_ID);
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
                var entity = productCategoryS.GetById(id);
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
            EQResult eQResult = productCategoryS.Delete(id);
            return Json(eQResult);
        }
    }
}
