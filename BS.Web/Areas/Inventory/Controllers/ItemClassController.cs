namespace BS.Web.Areas.Inventory.Controllers
{
    [Area("Inventory")]
    public class ItemClassController : BaseController
    {
        private readonly ItemClassService ItemClassS;
        public ItemClassController(ItemClassService itemClassService)
        {
            ItemClassS = itemClassService;
        }
        public IActionResult Index()
        {
            var entityList = ItemClassS.GetAll();
            return View(entityList);
        }
        public IActionResult Create()
        {
            return View("AddUpdate", new ITEM_CLASS());
        }
        [HttpPost]
        public IActionResult AddUpdate(ITEM_CLASS obj)
        {
            EQResult eQResult = new EQResult();
            if (ModelState.IsValid)
            {
                eQResult = ItemClassS.Insert(obj, user_session.USER_ID);
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
                var entity = ItemClassS.GetById(id);
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
            EQResult eQResult = ItemClassS.Delete(id);
            return Json(eQResult);
        }
    }
}
