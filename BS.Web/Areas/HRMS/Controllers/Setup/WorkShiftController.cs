namespace BS.Web.Areas.HRMS.Controllers.Setup
{
    [Area("HRMS")]
    public class WorkShiftController : BaseController
    {
        private readonly WorkShiftService workShiftS;
        public WorkShiftController(WorkShiftService _workShiftService)
        {
            workShiftS = _workShiftService;
        }
        public IActionResult Index()
        {
            var entityList = workShiftS.GetAll();
            return View(ViewPathFinder.ViewName(this.GetType(), "Index"), entityList);
            //return View("/Areas/HRMS/Views/Setup/WorkShift/Index.cshtml", entityList);
        }
        public IActionResult Create()
        {
            return View(ViewPathFinder.ViewName(this.GetType(), "AddUpdate"), new WORK_SHIFT());
            //return View("/Areas/HRMS/Views/Setup/WorkShift/AddUpdate.cshtml", new WORK_SHIFT());
        }
        [HttpPost]
        public IActionResult AddUpdate(WORK_SHIFT obj)
        {
            EQResult eQResult = new EQResult();
            if (ModelState.IsValid)
            {
                eQResult = workShiftS.Insert(obj, user_session.USER_ID);
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
                var entity = workShiftS.GetById(id);
                if (entity != null)
                {
                    return View(ViewPathFinder.ViewName(this.GetType(), "AddUpdate"), entity);
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
            EQResult eQResult = workShiftS.Delete(id);
            return Json(eQResult);
        }
    }
}
