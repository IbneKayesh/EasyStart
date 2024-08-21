namespace BS.Web.Areas.Inventory.Controllers
{
    [Area("Inventory")]
    public class ProductClassController : BaseController
    {
        private readonly ProductClassService productClassS;
        public ProductClassController(ProductClassService _productClassService)
        {
            productClassS = _productClassService;
        }
        public IActionResult Index()
        {
            var entityList = productClassS.GetAll();
            return View(entityList);
        }
        public IActionResult Create()
        {
            return View("AddUpdate", new PRODUCT_CLASS());
        }
        [HttpPost]
        public IActionResult AddUpdate(PRODUCT_CLASS obj)
        {
            EQResult eQResult = new EQResult();
            if (ModelState.IsValid)
            {
                eQResult = productClassS.Insert(obj, user_session.USER_ID);
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
                var entity = productClassS.GetById(id);
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
            EQResult eQResult = productClassS.Delete(id);
            return Json(eQResult);
        }
    }
}
