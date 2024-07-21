namespace BS.Web.Areas.Company.Controllers
{
    [Area("Company")]
    public class BusinessController : BaseController
    {
        private readonly BusinessService businessS;
        public BusinessController(BusinessService _businessService)
        {
            businessS = _businessService;
        }
        public IActionResult Index()
        {
            @ViewData["PageNo"] = "311";
            var entityList = businessS.GetAll();
            return View(entityList);
        }
        public IActionResult Create()
        {
            @ViewData["PageNo"] = "312";
            return View("AddUpdate", new BUSINESS());
        }
        [HttpPost]
        public IActionResult AddUpdate(BUSINESS obj)
        {
            @ViewData["PageNo"] = "312";
            EQResult eQResult = new EQResult();
            if (ModelState.IsValid)
            {
                eQResult = businessS.Insert(obj, UserId);
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
            @ViewData["PageNo"] = "312";
            if (!string.IsNullOrWhiteSpace(id))
            {
                var entity = businessS.GetById(id);
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
            EQResult eQResult = businessS.Delete(id);
            return Json(eQResult);
        }
    }
}
