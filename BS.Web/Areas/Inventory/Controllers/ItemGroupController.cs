namespace BS.Web.Areas.Inventory.Controllers
{
    [Area("Inventory")]
    public class ItemGroupController : BaseController
    {
        private readonly ItemGroupService ItemGroupS;
        private readonly ItemGroupTypeService ItemGroupTypeS;
        public ItemGroupController(ItemGroupService itemGroupService, ItemGroupTypeService itemGroupTypeService)
        {
            ItemGroupS = itemGroupService;
            ItemGroupTypeS = itemGroupTypeService;
        }
        public IActionResult Index()
        {
            var entityList = ItemGroupS.GetAll();
            return View(entityList);
        }
        public IActionResult Create()
        {
            Dropdown_CreateEdit();
            return View("AddUpdate", new ITEM_GROUP());
        }
        [HttpPost]
        public IActionResult AddUpdate(ITEM_GROUP obj)
        {
            EQResult eQResult = new EQResult();
            if (ModelState.IsValid)
            {
                eQResult = ItemGroupS.Insert(obj, user_session.USER_ID);
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
            Dropdown_CreateEdit();
            return View(obj);
        }
        public IActionResult Edit(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                Dropdown_CreateEdit();
                var entity = ItemGroupS.GetById(id);
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
            ViewBag.ITEM_GROUP_TYPE_ID = new SelectList(ItemGroupTypeS.GetAllActive(), "ID", "ITEM_GROUP_TYPE_NAME");
        }

        public IActionResult Delete(string id)
        {
            EQResult eQResult = ItemGroupS.Delete(id);
            return Json(eQResult);
        }
    }
}
