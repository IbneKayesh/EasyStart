using BS.DMO.Models.HelpDesk;
using Microsoft.AspNetCore.Mvc;

namespace BS.Web.Areas.HelpDesk.Controllers
{
    [Area("HelpDesk")]
    public class WorkTaskController : BaseController
    {
        private readonly WorkTaskService workTaskS;
        public WorkTaskController(WorkTaskService _workTaskService)
        {
           workTaskS = _workTaskService;
        }
        public IActionResult Index()
        {
            var entityList = workTaskS.GetAll();
            return View(entityList);
        }
        public IActionResult Create()
        {
            return View("AddUpdate", new WORK_TASK());
        }
        [HttpPost]
        public IActionResult AddUpdate(WORK_TASK obj)
        {
            EQResult eQResult = new EQResult();
            if (ModelState.IsValid)
            {
                eQResult = workTaskS.Insert(obj, user_session.USER_ID);
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
                var entity = workTaskS.GetById(id);
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
            EQResult eQResult = workTaskS.Delete(id);
            return Json(eQResult);
        }
    }
}
//make SQL format with like and IN () key
//product warranty claim
//application menu and role menu

//product
//order booking detail
//contact documents
//product category picture