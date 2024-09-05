namespace BS.Web.Areas.Inventory.Controllers
{
    [Area("Inventory")]
    public class ItemSubGroupController : BaseController
    {
        private readonly ItemSubGroupService ItemSubGroupS;
        private readonly ItemGroupService ItemGroupS;
        public ItemSubGroupController(ItemSubGroupService itemSubGroupService, ItemGroupService itemGroupService)
        {
            ItemSubGroupS = itemSubGroupService;
            ItemGroupS = itemGroupService;
        }
        public IActionResult Index()
        {
            var entityList = ItemSubGroupS.GetAll();
            return View(entityList);
        }
        public IActionResult Create()
        {
            Dropdown_CreateEdit();
            return View("AddUpdate", new ITEM_SUB_GROUP());
        }
        [HttpPost]
        public IActionResult AddUpdate(ITEM_SUB_GROUP obj)
        {
            EQResult eQResult = new EQResult();
            if (ModelState.IsValid)
            {
                eQResult = ItemSubGroupS.Insert(obj, user_session.USER_ID);
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
            Dropdown_CreateEdit();
            return View(obj);
        }
        public IActionResult Edit(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                Dropdown_CreateEdit();
                var entity = ItemSubGroupS.GetById(id);
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
            ViewBag.ITEM_GROUP_ID = new SelectList(ItemGroupS.GetAllActive(), "ID", "ITEM_GROUP_NAME");
        }

        public IActionResult Delete(string id)
        {
            EQResult eQResult = ItemSubGroupS.Delete(id);
            return Json(eQResult);
        }
    }
}
