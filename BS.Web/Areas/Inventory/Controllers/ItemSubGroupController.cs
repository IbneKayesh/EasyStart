using BS.DMO.Models.Inventory;
using BS.Infra.Services.Inventory;

namespace BS.Web.Areas.Inventory.Controllers
{
    [Area("Inventory")]
    public class ItemSubGroupController : BaseController
    {
        private readonly ItemSubGroupService ItemSubGroupS;
        private readonly ItemGroupService ItemGroupS;
        private readonly ItemAttributeService ItemAttributeS;
        public ItemSubGroupController(
            ItemSubGroupService itemSubGroupService,
            ItemGroupService itemGroupService,
            ItemAttributeService itemAttributeService)
        {
            ItemSubGroupS = itemSubGroupService;
            ItemGroupS = itemGroupService;
            ItemAttributeS = itemAttributeService;
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

        //Item Structure Setup
        public IActionResult SetupList(string item_sub_group_id)
        {
            var entityList = ItemSubGroupS.GetSetupListByItemSubGroupId(item_sub_group_id);
            return View("SetupList", entityList);
        }
        public IActionResult CreateSetup(string item_sub_group_id)
        {
            var obj = new ITEM_SETUP();
            obj.ITEM_SUB_GROUP_ID = item_sub_group_id;
            Dropdown_CreateSetup();
            return View("CreateSetup", obj);
        }
        [HttpPost]
        public IActionResult CreateSetup(ITEM_SETUP obj)
        {
            EQResult eQResult = new EQResult();
            if (ModelState.IsValid)
            {
                eQResult = ItemSubGroupS.CreateSetup(obj, user_session.USER_ID);
                TempData["msg"] = eQResult.messages;

                if (eQResult.success && eQResult.rows > 0)
                {
                    return RedirectToAction(nameof(SetupList), new { item_sub_group_id = obj.ITEM_SUB_GROUP_ID });
                }
            }
            else
            {
                var errors = UtilityService.GET_MODEL_ERRORS(ModelState);
                ModelState.AddModelError("", errors);
            }
            return View(obj);
        }
        private void Dropdown_CreateSetup()
        {
            ViewBag.ITEM_ATTRIBUTE_ID = new SelectList(ItemAttributeS.GetAllActive(), "ID", "ITEM_ATTRIBUTE_NAME");
            ViewBag.ITEM_ATTRIBUTE_VALUE_ID = new SelectList(ItemMasterAttributeValue.GetAll());
        }
    }
}
