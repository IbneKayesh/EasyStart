using BS.DMO.Models.Setup;

namespace BS.Web.Areas.Inventory.Controllers
{
    [Area("Inventory")]
    public class UnitChildController : BaseController
    {
        private readonly UnitChildService unitChildS;
        private readonly UnitMasterService unitMasterS;
        public UnitChildController(UnitChildService _unitChildService, UnitMasterService _unitMasterService)
        {
            unitChildS = _unitChildService;
            unitMasterS = _unitMasterService;
        }
        public IActionResult Index()
        {
            var entityList = unitChildS.GetAll();
            return View(entityList);
        }
        public IActionResult Create()
        {
            Dropdown_CreateEdit();
            return View("AddUpdate", new UNIT_CHILD());
        }
        [HttpPost]
        public IActionResult AddUpdate(UNIT_CHILD obj)
        {
            Dropdown_CreateEdit();
            EQResult eQResult = new EQResult();
            if (ModelState.IsValid)
            {
                eQResult = unitChildS.Insert(obj, user_session.USER_ID);
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
            Dropdown_CreateEdit();
            if (!string.IsNullOrWhiteSpace(id))
            {
                var entity = unitChildS.GetById(id);
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
            ViewBag.UNIT_MASTER_ID = new SelectList(unitMasterS.GetAllActive(), "ID", "UNIT_MASTER_NAME");
        }
        public IActionResult Delete(string id)
        {
            EQResult eQResult = unitChildS.Delete(id);
            return Json(eQResult);
        }
    }
}
