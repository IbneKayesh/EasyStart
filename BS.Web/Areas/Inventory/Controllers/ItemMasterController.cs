using BS.DMO.ViewModels.Inventory;

namespace BS.Web.Areas.Inventory.Controllers
{
    [Area("Inventory")]
    public class ItemMasterController : BaseController
    {
        private readonly ProductsService productsS;
        private readonly BusinessLineService businessLineS;
        private readonly ItemTypeService productTypeS;
        private readonly ItemClassService productClassS;
        private readonly ItemCategoryService productCategoryS;
        private readonly UnitChildService unitChildS;
        public ItemMasterController(ProductsService _productsService,
            BusinessLineService _businessLineService,
            ItemTypeService _productTypeService,
            ItemClassService _productClassService,
            ItemCategoryService _productCategoryService,
            UnitChildService _unitChildService)
        {
            productsS = _productsService;
            businessLineS = _businessLineService;
            productTypeS = _productTypeService;
            productClassS = _productClassService;
            productCategoryS = _productCategoryService;
            unitChildS = _unitChildService;
        }
        public IActionResult Index(PRODUCTS_IDX obj)
        {
            var entityList = productsS.GetAll(obj);
            obj.PRODUCTS_VM = entityList;
            return View(obj);
        }
        public IActionResult Create()
        {
            Dropdown_CreateEdit();
            return View("AddUpdate", new PRODUCTS());
        }
        public IActionResult Edit(string id)
        {
            Dropdown_CreateEdit();
            if (!string.IsNullOrWhiteSpace(id))
            {
                var entity = productsS.GetById(id);
                if (entity != null)
                {
                    return View("AddUpdate", entity);
                }
                else
                {
                    TempData["msg"] = NotifyService.NotFound();
                }
            }
            return View("AddUpdate", new PRODUCTS());
        }
        [HttpPost]
        public IActionResult AddUpdate(PRODUCTS obj)
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
                eQResult = productsS.Insert(obj, user_session.USER_ID);
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
            ViewBag.BUSINESS_LINE_ID = new SelectList(businessLineS.GetAllActive(), "ID", "BUSINESS_LINE_NAME");
            ViewBag.PRODUCT_TYPE_ID = new SelectList(productTypeS.GetAllActive(), "ID", "TYPE_NAME");
            ViewBag.PRODUCT_CLASS_ID = new SelectList(productClassS.GetAllActive(), "ID", "CLASS_NAME");
            ViewBag.PRODUCT_CATEGORY_ID = new SelectList(productCategoryS.GetAllActive(), "ID", "CATEGORY_NAME");
            ViewBag.UNIT_CHILD_ID = new SelectList(unitChildS.GetAllActive(), "ID", "UNIT_NAME");
        }
        public IActionResult Delete(string id)
        {
            EQResult eQResult = productsS.Delete(id);
            return Json(eQResult);
        }

        //API
        [HttpPost]
        public IActionResult FindProductsForSalesBooking(string search_term)
        {
            var obj = productsS.GetProductsForSalesBookingByName(search_term.ToLower());
            return Json(obj);
        }
        [HttpPost]
        public IActionResult FindProductsForSalesBooking2(string productName)
        {
            var obj = productsS.GetProductsForSalesBookingByName(productName.ToLower());
            return Json(obj);
        }
    }
}
