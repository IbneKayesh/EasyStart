namespace BS.Web.Areas.Inventory.Controllers
{
    [Area("Inventory")]
    public class SegmentNameTypeController : BaseController
    {
        private readonly SegmentNameTypeService segmentNameTypeS;
        public SegmentNameTypeController(SegmentNameTypeService _segmentNameTypeService)
        {
            segmentNameTypeS = _segmentNameTypeService;
        }
        public IActionResult Index()
        {
            var entityList = segmentNameTypeS.GetAll();
            return View(entityList);
        }
        public IActionResult Create()
        {
            return View("AddUpdate", new SEGMENT_NAME_TYPE());
        }
        [HttpPost]
        public IActionResult AddUpdate(SEGMENT_NAME_TYPE obj)
        {
            EQResult eQResult = new EQResult();
            if (ModelState.IsValid)
            {
                eQResult = segmentNameTypeS.Insert(obj, user_session.USER_ID);
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
                var entity = segmentNameTypeS.GetById(id);
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
            EQResult eQResult = segmentNameTypeS.Delete(id);
            return Json(eQResult);
        }
    }
}
