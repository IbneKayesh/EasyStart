using BS.Infra.DbHelper;
using BS.Web.Services;

namespace BS.Web.Areas.Setup.Controllers
{
    [Area("Setup")]
    public class LeaveTypeController : BaseController
    {
        private readonly LeaveTypeService leaveTypeS;
        public LeaveTypeController(LeaveTypeService _leaveTypeService)
        {
            leaveTypeS = _leaveTypeService;
        }
        public IActionResult Index()
        {
            var entityList = leaveTypeS.GetAll();
            return View(entityList);
        }
        public IActionResult Create()
        {
            return View("AddUpdate", new LEAVE_TYPE());
        }
        [HttpPost]
        public IActionResult AddUpdate(LEAVE_TYPE obj)
        {
            EQResult eQResult = new EQResult();
            if (ModelState.IsValid)
            {
                eQResult = leaveTypeS.Insert(obj, user_session.USER_ID);
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
                var entity = leaveTypeS.GetById(id);
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
            EQResult eQResult = leaveTypeS.Delete(id);
            return Json(eQResult);
        }
    }
}
