using BS.DMO.Models.HelpDesk;
using BS.DMO.ViewModels.HelpDesk;

namespace BS.Web.Areas.HelpDesk.Controllers
{
    [Area("HelpDesk")]
    public class RoutineTaskController : BaseController
    {
        private readonly RoutineTaskService routineTaskS;
        public RoutineTaskController(RoutineTaskService _routineTaskService)
        {
            routineTaskS = _routineTaskService;
        }
        public IActionResult Index()
        {
            var entityList = routineTaskS.GetAll();
            return View(entityList);
        }
        public IActionResult Create()
        {
            DateTime dateTime = DateTime.Now;
            var obj = routineTaskS.CreateNew(dateTime);
            return View("AddUpdate", obj);
        }
        [HttpPost]
        public IActionResult AddUpdate(ROUTINE_TASK_CREATE_VM obj)
        {
            EQResult eQResult = new EQResult();
            foreach (var item in obj.ROUTINE_TASK_VM)
            {
                if (item.IS_DONE == null)
                {
                    item.IS_DONE = "false";
                }
                else
                {
                    item.IS_DONE = "true";
                }
            }
            ModelState.Clear();
            if (ModelState.IsValid)
            {
                eQResult = routineTaskS.Insert(obj, user_session.USER_ID);
                TempData["msg"] = eQResult.messages;

                if (eQResult.success && eQResult.rows > 0)
                {
                    return RedirectToAction(nameof(Create));
                }
            }
            else
            {
                var errors = ValidateModelData.GET_MODEL_ERRORS(ModelState);
                ModelState.AddModelError("", errors);
            }
            return View(obj);
        }
        public IActionResult Edit(string id, string copy)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var entity = routineTaskS.GetById(id);
                if (entity != null)
                {
                    if (!string.IsNullOrWhiteSpace(copy))
                    {
                        //asign for new save
                        ModelState.Clear();
                        entity.ID = Guid.Empty.ToString();
                    }
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
            EQResult eQResult = routineTaskS.Delete(id);
            return Json(eQResult);
        }
    }
}