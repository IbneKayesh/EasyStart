using BS.DMO.Models.Setup;

namespace BS.Web.Areas.Setup.Controllers
{
    [Area("Setup")]
    public class SubSectionsTrnIdController : BaseController
    {
        private readonly SubSectionsTrnIdService subSectionsTrnIdS;
        public SubSectionsTrnIdController(SubSectionsTrnIdService _subSectionsTrnIdService)
        {
            subSectionsTrnIdS = _subSectionsTrnIdService;
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
                var entity = subSectionsTrnIdS.GetById(id);
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
            ViewBag.TRN_ID = new SelectList(TransactionID.GetAll());
        }
        public IActionResult Delete(string id)
        {
            EQResult eQResult = subSectionsTrnIdS.Delete(id);
            return Json(eQResult);
        }
    }
}
