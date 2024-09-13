namespace BS.Web.Areas.Setup.Controllers
{
    [Area("Setup")]
    public class SubSectionsTrnIdController : BaseController
    {
        private readonly SubSectionsTrnIdService subSectionsTrnIdS;
        private readonly SubSectionService subSectionS;
        public SubSectionsTrnIdController(SubSectionsTrnIdService _subSectionsTrnIdService, SubSectionService _subSectionService)
        {
            subSectionsTrnIdS = _subSectionsTrnIdService;
            subSectionS = _subSectionService;
        }
        public IActionResult Index()
        {
            var entityList = subSectionsTrnIdS.GetAll();
            return View(entityList);
        }
        public IActionResult Create()
        {
            Dropdown_CreateEdit();
            return View("AddUpdate", new SUB_SECTIONS_TRN_ID());
        }
        [HttpPost]
        public IActionResult AddUpdate(SUB_SECTIONS_TRN_ID obj)
        {
            Dropdown_CreateEdit();
            EQResult eQResult = new EQResult();
            if (ModelState.IsValid)
            {
                eQResult = subSectionsTrnIdS.Insert(obj, user_session.USER_ID);
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
        public IActionResult Edit(string trn_id, string sub_section_id)
        {
            Dropdown_CreateEdit();
            if (!string.IsNullOrWhiteSpace(trn_id) && !string.IsNullOrWhiteSpace(sub_section_id))
            {
                var entity = subSectionsTrnIdS.GetById(trn_id, sub_section_id);
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
            ViewBag.SUB_SECTION_ID = new SelectList(subSectionS.GetAllActive(), "ID", "SUB_SECTION_NAME");
            ViewBag.TRN_ID = new SelectList(TransactionID.GetAll());
        }
        [HttpGet]
        public IActionResult Delete(string trn_id, string sub_section_id)
        {
            EQResult eQResult = subSectionsTrnIdS.Delete(trn_id, sub_section_id);
            return Json(eQResult);
        }
    }
}
