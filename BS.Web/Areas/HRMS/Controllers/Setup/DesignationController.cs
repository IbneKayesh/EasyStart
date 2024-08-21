namespace BS.Web.Areas.HRMS.Controllers.Setup
{
    [Area("HRMS")]
    public class DesignationController : BaseController
    {
        private readonly DesignationService designationS;
        public DesignationController(DesignationService _designationService)
        {
            designationS = _designationService;
        }
        public IActionResult Index()
        {
            var entityList = designationS.GetAll();
            return View(ViewPathFinder.ViewName(this.GetType(), "Index"), entityList);
        }
        public IActionResult Create()
        {
            Dropdown_CreateEdit();
            return View(ViewPathFinder.ViewName(this.GetType(), "AddUpdate"), new DESIGNATION());
        }
        [HttpPost]
        public IActionResult AddUpdate(DESIGNATION obj)
        {
            EQResult eQResult = new EQResult();
            if (ModelState.IsValid)
            {
                eQResult = designationS.Insert(obj, user_session.USER_ID);
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
                var entity = designationS.GetById(id);
                if (entity != null)
                {
                    Dropdown_CreateEdit();
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

        private void Dropdown_CreateEdit()
        {
            ViewBag.PARENT_ID = designationS.GetAllActive();
        }

        public IActionResult Delete(string id)
        {
            EQResult eQResult = designationS.Delete(id);
            return Json(eQResult);
        }
    }
}
