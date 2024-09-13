using BS.DMO.Models.Setup;

namespace BS.Web.Areas.Setup.Controllers
{
    [Area("Setup")]
    public class TransactionAutoStepController : BaseController
    {
        private readonly TrnAutoStepService trnAutoStepS;
        public TransactionAutoStepController(TrnAutoStepService _trnAutoStepService)
        {
            trnAutoStepS = _trnAutoStepService;
        }
        public IActionResult Index()
        {
            var entityList = trnAutoStepS.GetAll();
            return View(entityList);
        }
        public IActionResult Create()
        {
            Dropdown_CreateEdit();
            return View("AddUpdate", new TRN_AUTO_STEP());
        }
        [HttpPost]
        public IActionResult AddUpdate(TRN_AUTO_STEP obj)
        {
            Dropdown_CreateEdit();
            EQResult eQResult = new EQResult();
            if (ModelState.IsValid)
            {
                eQResult = trnAutoStepS.Insert(obj, user_session.USER_ID);
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
                var entity = trnAutoStepS.GetById(id);
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
            EQResult eQResult = trnAutoStepS.Delete(id);
            return Json(eQResult);
        }
    }
}
