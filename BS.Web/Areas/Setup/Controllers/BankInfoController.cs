using BS.Infra.DbHelper;

namespace BS.Web.Areas.Setup.Controllers
{
    [Area("Setup")]
    public class BankInfoController : BaseController
    {
        private readonly BankInfoService bankInfoService;
        public BankInfoController(BankInfoService _bankInfoService)
        {
            bankInfoService = _bankInfoService;
        }
        public IActionResult Index()
        {
            var entityList = bankInfoService.GetAll();
            return View(entityList);
        }
        public IActionResult Create()
        {
            return View("AddUpdate", new BANK_INFO());
        }
        [HttpPost]
        public IActionResult AddUpdate(BANK_INFO obj)
        {
            if (ModelState.IsValid)
            {
                EQResult eQResult = bankInfoService.Insert(obj, UserId);
                TempData["msg"] = eQResult.Messages;

                if (eQResult.Success && eQResult.Rows > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(obj);
        }
        public IActionResult Edit(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var entity = bankInfoService.GetById(id);
                if (entity != null)
                {
                    return View("AddUpdate", entity);
                }
                else
                {
                    TempData["msg"] = NotifyServices.NotFound();
                }
            }
            else
            {
                TempData["msg"] = NotifyServices.Error("Invalid ID, Parameter is required");
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                EQResult eQResult = bankInfoService.Delete(id);
                TempData["msg"] = eQResult.Messages;
            }
            else
            {
                TempData["msg"] = NotifyServices.Error("Invalid ID, Parameter is required");
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
