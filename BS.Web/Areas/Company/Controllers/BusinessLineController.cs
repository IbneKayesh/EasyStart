namespace BS.Web.Areas.Company.Controllers
{
    [Area("Company")]
    public class BusinessLineController : BaseController
    {
        private readonly BusinessLineService businessLineS;
        public BusinessLineController(BusinessLineService _businessLineService)
        {
            businessLineS = _businessLineService;
        }
        public IActionResult Index()
        {
            var entityList = businessLineS.GetAll();
            return View(entityList);
        }
        public IActionResult Create()
        {
            return View("AddUpdate", new BUSINESS_LINE());
        }
        [HttpPost]
        public IActionResult AddUpdate(BUSINESS_LINE obj)
        {
            EQResult eQResult = new EQResult();
            if (ModelState.IsValid)
            {
                eQResult = businessLineS.Insert(obj, user_session.USER_ID);
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
                var entity = businessLineS.GetById(id);
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
            EQResult eQResult = businessLineS.Delete(id);
            return Json(eQResult);
        }
    }
}
