namespace BS.Web.Areas.Company.Controllers
{
    [Area("Company")]
    public class SubSectionController : BaseController
    {
        private readonly SubSectionService subSectionS;
        private readonly SectionService sectionS;
        public SubSectionController(SubSectionService _subSectionService, SectionService _sectionService)
        {
            subSectionS = _subSectionService;
            sectionS = _sectionService;
        }
        public IActionResult Index()
        {
            var entityList = subSectionS.GetAll();
            return View(entityList);
        }
        public IActionResult Create()
        {
            Dropdown_CreateEdit();
            return View("AddUpdate", new SUB_SECTIONS());
        }
        [HttpPost]
        public IActionResult AddUpdate(SUB_SECTIONS obj)
        {
            Dropdown_CreateEdit();
            EQResult eQResult = new EQResult();
            if (ModelState.IsValid)
            {
                eQResult = subSectionS.Insert(obj, UserId);
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
            Dropdown_CreateEdit();
            if (!string.IsNullOrWhiteSpace(id))
            {
                var entity = sectionS.GetById(id);
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
            ViewBag.SECTION_ID = new SelectList(sectionS.GetAllActive(), "ID", "SECTION_NAME");
        }
        public IActionResult Delete(string id)
        {
            EQResult eQResult = subSectionS.Delete(id);
            return Json(eQResult);
        }
    }
}
