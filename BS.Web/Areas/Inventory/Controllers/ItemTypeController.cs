﻿namespace BS.Web.Areas.Inventory.Controllers
{
    [Area("Inventory")]
    public class ItemTypeController : BaseController
    {
        private readonly ItemTypeService productTypeS;
        public ItemTypeController(ItemTypeService _productTypeService)
        {
            productTypeS = _productTypeService;
        }
        public IActionResult Index()
        {
            var entityList = productTypeS.GetAll();
            return View(entityList);
        }
        public IActionResult Create()
        {
            return View("AddUpdate", new ITEM_TYPE());
        }
        [HttpPost]
        public IActionResult AddUpdate(ITEM_TYPE obj)
        {
            EQResult eQResult = new EQResult();
            if (ModelState.IsValid)
            {
                eQResult = productTypeS.Insert(obj, user_session.USER_ID);
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
                var entity = productTypeS.GetById(id);
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
            EQResult eQResult = productTypeS.Delete(id);
            return Json(eQResult);
        }
    }
}
