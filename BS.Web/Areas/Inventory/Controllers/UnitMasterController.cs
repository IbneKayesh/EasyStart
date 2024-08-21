namespace BS.Web.Areas.Inventory.Controllers
{
    [Area("Inventory")]
    public class UnitMasterController : BaseController
    {
        private readonly UnitMasterService unitMasterS;
        public UnitMasterController(UnitMasterService _unitMasterService)
        {
            unitMasterS = _unitMasterService;
        }
        public IActionResult Index()
        {
            var entityList = unitMasterS.GetAll();
            return View(entityList);
        }
        public IActionResult Create()
        {
            return View("AddUpdate", new UNIT_MASTER());
        }
        [HttpPost]
        public IActionResult AddUpdate(UNIT_MASTER obj)
        {
            EQResult eQResult = new EQResult();
            if (ModelState.IsValid)
            {
                eQResult = unitMasterS.Insert(obj, user_session.USER_ID);
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
                var entity = unitMasterS.GetById(id);
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
            EQResult eQResult = unitMasterS.Delete(id);
            return Json(eQResult);
        }
    }
}
