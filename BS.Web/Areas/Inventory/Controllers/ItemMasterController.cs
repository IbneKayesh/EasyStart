using BS.DMO.ViewModels.Inventory;

namespace BS.Web.Areas.Inventory.Controllers
{
    [Area("Inventory")]
    public class ItemMasterController : BaseController
    {
        private readonly ItemMasterService ItemMasterS;
        private readonly ItemSubGroupService ItemSubGroupS;
        private readonly ItemClassService ItemClassS;
        private readonly ItemCategoryService ItemCategoryS;
        private readonly ItemTypeService ItemTypeS;
        private readonly ItemStatusService ItemStatusS;
        private readonly UnitChildService UnitChildS;

        public ItemMasterController(ItemMasterService itemMasterService,
         ItemSubGroupService itemSubGroupService,
         ItemClassService itemClassService,
         ItemCategoryService itemCategoryService,
         ItemTypeService itemTypeService,
         ItemStatusService itemStatusService,
         UnitChildService unitChildService)
        {
            ItemMasterS = itemMasterService;
            ItemSubGroupS = itemSubGroupService;
            ItemClassS = itemClassService;
            ItemCategoryS = itemCategoryService;
            ItemTypeS = itemTypeService;
            ItemStatusS = itemStatusService;
            UnitChildS = unitChildService;
        }
        public IActionResult Index(ITEM_MASTER_IDX obj)
        {
            var entityList = ItemMasterS.GetAll(obj);
            obj.ITEM_MASTER_VM = entityList;
            return View(obj);
        }
        public IActionResult Create()
        {
            Dropdown_CreateEdit();
            return View("AddUpdate", new ITEM_MASTER());
        }
        public IActionResult Edit(string id)
        {
            Dropdown_CreateEdit();
            if (!string.IsNullOrWhiteSpace(id))
            {
                var entity = ItemMasterS.GetById(id);
                if (entity != null)
                {
                    return View("AddUpdate", entity);
                }
                else
                {
                    TempData["msg"] = NotifyService.NotFound();
                }
            }
            return View("AddUpdate", new ITEM_MASTER());
        }
        [HttpPost]
        public IActionResult AddUpdate(ITEM_MASTER obj)
        {
            Dropdown_CreateEdit();
            EQResult eQResult = new EQResult();
            if (ModelState.IsValid)
            {
                var buttonClicked = Request.Form["submitButton"];
                if (buttonClicked == "CopySave")
                {
                    ModelState.Clear();
                    obj.ID = Guid.Empty.ToString();
                }
                eQResult = ItemMasterS.Insert(obj, user_session.USER_ID);
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
        private void Dropdown_CreateEdit()
        {
            ViewBag.ITEM_SUB_GROUP_ID = new SelectList(ItemSubGroupS.GetAllActive(), "ID", "ITEM_SUB_GROUP_NAME");
            ViewBag.ITEM_CLASS_ID = new SelectList(ItemClassS.GetAllActive(), "ID", "CLASS_NAME");
            ViewBag.ITEM_CATEGORY_ID = new SelectList(ItemCategoryS.GetAllActive(), "ID", "CATEGORY_NAME");
            ViewBag.ITEM_TYPE_ID = new SelectList(ItemTypeS.GetAllActive(), "ID", "TYPE_NAME");
            ViewBag.ITEM_STATUS_ID = new SelectList(ItemStatusS.GetAllActive(), "ID", "STATUS_NAME");
            ViewBag.UNIT_CHILD_ID = new SelectList(UnitChildS.GetAllActive(), "ID", "UNIT_NAME");
        }
        public IActionResult Delete(string id)
        {
            EQResult eQResult = ItemMasterS.Delete(id);
            return Json(eQResult);
        }

        //API
        [HttpPost]
        public IActionResult GetItemDetailsForSalesBookingBySubGroupIDByItemName(string item_sub_group_id)
        {
            var obj = ItemMasterS.GetItemDetailsForSalesBookingBySubGroupIDByItemName(item_sub_group_id);
            return Json(obj);
        }
        [HttpPost]
        public IActionResult GetTableSetup(string item_sub_group_id)
        {
            var obj = ItemMasterS.GetGenerateTables(item_sub_group_id);
            return Json(obj);
        }
    }
}
