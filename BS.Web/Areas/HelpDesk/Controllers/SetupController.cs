using BS.Infra.Services.HelpDesk.Setup;

namespace BS.Web.Areas.HelpDesk.Controllers
{
    [Area("HelpDesk")]
    public class SetupController : BaseController
    {
        private readonly TaskStatusService taskStatusS;
        public SetupController(TaskStatusService _taskStatusService)
        {
            taskStatusS = _taskStatusService;
        }
        public IActionResult TaskStatus()
        {
            var entityList = taskStatusS.GetAll();
            return View(entityList);
        }
        public IActionResult CreateTaskStatus()
        {
            return View("CreateTaskStatus", new TASK_STATUS());
        }
        [HttpPost]
        public IActionResult CreateTaskStatus(TASK_STATUS obj)
        {
            EQResult eQResult = new EQResult();
            if (ModelState.IsValid)
            {
                eQResult = taskStatusS.Insert(obj, user_session.USER_ID);
                TempData["msg"] = eQResult.messages;

                if (eQResult.success && eQResult.rows > 0)
                {
                    return RedirectToAction(nameof(TaskStatus));
                }
            }
            else
            {
                var errors = UtilityService.GET_MODEL_ERRORS(ModelState);
                ModelState.AddModelError("", errors);
            }
            return View(obj);
        }
        public IActionResult EditTaskStatus(string id, string copy)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var entity = taskStatusS.GetById(id);
                if (entity != null)
                {
                    return View("CreateTaskStatus", entity);
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
            return RedirectToAction(nameof(TaskStatus));
        }
        public IActionResult DeleteTaskStatus(string id)
        {
            EQResult eQResult = taskStatusS.Delete(id);
            return Json(eQResult);
        }
    }
}