namespace BS.Web.Areas.Inventory.Controllers
{
    [Area("Inventory")]
    public class ProductGroupController : BaseController
    {
        private readonly ProductGroupService productGroupS;
        public ProductGroupController(ProductGroupService _productGroupService)
        {
            productGroupS = _productGroupService;
        }
        public IActionResult Index()
        {
            var entityList = productGroupS.GetAll();
            return View(entityList);
        }
        public IActionResult Create()
        {
            return View("AddUpdate", new PRODUCT_GROUP());
        }
        [HttpPost]
        public IActionResult AddUpdate(PRODUCT_GROUP obj)
        {
            EQResult eQResult = new EQResult();
            if (ModelState.IsValid)
            {
                eQResult = productGroupS.Insert(obj, user_session.USER_ID);
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
                var entity = productGroupS.GetById(id);
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
            EQResult eQResult = productGroupS.Delete(id);
            return Json(eQResult);
        }
    }
}
