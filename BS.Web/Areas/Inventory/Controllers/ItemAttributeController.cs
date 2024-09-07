namespace BS.Web.Areas.Inventory.Controllers
{
    [Area("Inventory")]
    public class ItemAttributeController : BaseController
    {
        private readonly ItemAttributeService ItemAttributeS;
        private readonly ItemAttributeValueService ItemAttributeValueS;
        public ItemAttributeController(ItemAttributeService _itemAttributeService, ItemAttributeValueService _itemAttributeValueService)
        {
            ItemAttributeS = _itemAttributeService;
            ItemAttributeValueS = _itemAttributeValueService;
        }
        public IActionResult Index()
        {
            var entityList = ItemAttributeS.GetAll();
            return View(entityList);
        }
        public IActionResult Create()
        {
            return View("AddUpdate", new ITEM_ATTRIBUTE());
        }
        [HttpPost]
        public IActionResult AddUpdate(ITEM_ATTRIBUTE obj)
        {
            EQResult eQResult = new EQResult();
            if (ModelState.IsValid)
            {
                eQResult = ItemAttributeS.Insert(obj, user_session.USER_ID);
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
                var entity = ItemAttributeS.GetById(id);
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
            EQResult eQResult = ItemAttributeS.Delete(id);
            return Json(eQResult);
        }
        public IActionResult ViewAttributeValue(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var entity = ItemAttributeValueS.GetAllByItemAttributeId(id);
                if (entity != null)
                {
                    return View("ViewAttribute", entity);
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
    }
}
