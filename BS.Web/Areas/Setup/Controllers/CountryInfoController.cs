using BS.Infra.DbHelper;
using BS.Web.Services;

namespace BS.Web.Areas.Setup.Controllers
{
    [Area("Setup")]
    public class CountryInfoController : BaseController
    {
        private readonly CountryInfoService countryInfoS;
        public CountryInfoController(CountryInfoService _countryInfoService)
        {
            countryInfoS = _countryInfoService;
        }
        public IActionResult Index()
        {
            @ViewData["PageNo"] = "211";
            var entityList = countryInfoS.GetAll();
            return View(entityList);
        }
        public IActionResult Create()
        {
            @ViewData["PageNo"] = "212";
            return View("AddUpdate", new COUNTRY_INFO());
        }
        [HttpPost]
        public IActionResult AddUpdate(COUNTRY_INFO obj)
        {
            @ViewData["PageNo"] = "212";
            EQResult eQResult = new EQResult();
            if (ModelState.IsValid)
            {
                eQResult = countryInfoS.Insert(obj, UserId);
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
            @ViewData["PageNo"] = "212";
            if (!string.IsNullOrWhiteSpace(id))
            {
                var entity = countryInfoS.GetById(id);
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
            EQResult eQResult = countryInfoS.Delete(id);
            return Json(eQResult);
        }
    }
}
