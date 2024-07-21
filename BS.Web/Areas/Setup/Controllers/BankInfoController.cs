using BS.Infra.DbHelper;
using BS.Web.Services;

namespace BS.Web.Areas.Setup.Controllers
{
    [Area("Setup")]
    public class BankInfoController : BaseController
    {
        private readonly BankInfoService bankInfoS;
        public BankInfoController(BankInfoService _bankInfoService)
        {
            bankInfoS = _bankInfoService;
        }
        public IActionResult Index()
        {
            @ViewData["PageNo"] = "221";
            var entityList = bankInfoS.GetAll();
            return View(entityList);
        }
        public IActionResult Create()
        {
            @ViewData["PageNo"] = "222";
            return View("AddUpdate", new BANK_INFO());
        }
        [HttpPost]
        public IActionResult AddUpdate(BANK_INFO obj)
        {
            @ViewData["PageNo"] = "222";
            EQResult eQResult = new EQResult();
            if (ModelState.IsValid)
            {
                eQResult = bankInfoS.Insert(obj, UserId);
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
            @ViewData["PageNo"] = "222";
            if (!string.IsNullOrWhiteSpace(id))
            {
                var entity = bankInfoS.GetById(id);
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
            EQResult eQResult = bankInfoS.Delete(id);
            return Json(eQResult);
        }
    }
}
