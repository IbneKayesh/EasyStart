﻿using BS.Helper;

namespace BS.Web.Areas.Inventory.Controllers
{
    [Area("Inventory")]
    public class ItemGroupTypeController : BaseController
    {
        private readonly ItemGroupTypeService ItemGroupTypeS;
        public ItemGroupTypeController(ItemGroupTypeService itemGroupTypeService)
        {
            ItemGroupTypeS = itemGroupTypeService;
        }
        public IActionResult Index()
        {
            var entityList = ItemGroupTypeS.GetAll();
            return View(entityList);
        }
        public IActionResult Create()
        {
            return View("AddUpdate", new ITEM_GROUP_TYPE());
        }
        [HttpPost]
        public IActionResult AddUpdate(ITEM_GROUP_TYPE obj)
        {
            EQResult eQResult = new EQResult();
            if (ModelState.IsValid)
            {
                eQResult = ItemGroupTypeS.Insert(obj, user_session.USER_ID);
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
            return View(obj);
        }
        public IActionResult Edit(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var entity = ItemGroupTypeS.GetById(id);
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
            EQResult eQResult = ItemGroupTypeS.Delete(id);
            return Json(eQResult);
        }
    }
}
