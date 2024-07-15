using BS.Infra.DbHelper;

namespace BS.Web.Areas.Setup.Controllers
{
    [Area("Setup")]
    public class BankBranchController : BaseController
    {
        private readonly BankBranchService bankBranchService;
        public BankBranchController(BankBranchService _bankBranchService)
        {
            bankBranchService = _bankBranchService;
        }
        public IActionResult Index()
        {
            var entityList = bankBranchService.GetAll();
            return View(entityList);
        }
        public IActionResult Create()
        {
            return View("AddUpdate", new BANK_BRANCH());
        }
        [HttpPost]
        public IActionResult AddUpdate(BANK_BRANCH obj)
        {
            if (ModelState.IsValid)
            {
                EQResult eQResult = bankBranchService.Insert(obj, UserId);
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
                var entity = bankBranchService.GetById(id);
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
                EQResult eQResult = bankBranchService.Delete(id);
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
