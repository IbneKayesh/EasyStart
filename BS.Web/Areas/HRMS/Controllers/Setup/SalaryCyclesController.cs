namespace BS.Web.Areas.HRMS.Controllers.Setup
{
    [Area("HRMS")]
    public class SalaryCyclesController : BaseController
    {
        private readonly SalaryCyclesService SalaryCyclesS;
        public SalaryCyclesController(SalaryCyclesService salaryCyclesService)
        {
            SalaryCyclesS = salaryCyclesService;
        }
        public IActionResult Index()
        {
            var entityList = SalaryCyclesS.GetAll();
            return View(ViewPathFinder.ViewName(this.GetType(), "Index"), entityList);
        }
        public IActionResult Create()
        {
            return View(ViewPathFinder.ViewName(this.GetType(), "AddUpdate"), new SALARY_CYCLES());
        }
        [HttpPost]
        public IActionResult AddUpdate(SALARY_CYCLES obj)
        {
            EQResult eQResult = new EQResult();
            if (ModelState.IsValid)
            {
                eQResult = SalaryCyclesS.Insert(obj, user_session.USER_ID);
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
                var entity = SalaryCyclesS.GetById(id);
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
            EQResult eQResult = SalaryCyclesS.Delete(id);
            return Json(eQResult);
        }
    }
}
