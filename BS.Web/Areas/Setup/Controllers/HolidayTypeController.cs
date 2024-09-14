namespace BS.Web.Areas.Setup.Controllers
{
    [Area("Setup")]
    public class HolidayTypeController : BaseController
    {
        private readonly HolidayTypeService HolidayTypeS;
        public HolidayTypeController(HolidayTypeService holidayTypeService)
        {
            HolidayTypeS= holidayTypeService;
        }
        public IActionResult Index()
        {
            var entityList = HolidayTypeS.GetAll();
            return View(entityList);
        }
        public IActionResult Create()
        {
            return View("AddUpdate", new HOLIDAY_TYPE());
        }
        [HttpPost]
        public IActionResult AddUpdate(HOLIDAY_TYPE obj)
        {
            EQResult eQResult = new EQResult();
            if (ModelState.IsValid)
            {
                eQResult = HolidayTypeS.Insert(obj, user_session.USER_ID);
                if (eQResult.success && eQResult.rows > 0)
                {
                    TempData["msg"] = eQResult.messages;
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", eQResult.messages);
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
                var entity = HolidayTypeS.GetById(id);
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
            EQResult eQResult = HolidayTypeS.Delete(id);
            return Json(eQResult);
        }
    }
}
