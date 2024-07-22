using BS.Infra.DbHelper;
using BS.Web.Services;

namespace BS.Web.Areas.Setup.Controllers
{
    [Area("Setup")]
    public class EntityValueTextController : BaseController
    {
        private readonly EntityValueTextService entityValueTextS;
        public EntityValueTextController(EntityValueTextService _entityValueTextService)
        {
            entityValueTextS = _entityValueTextService;
        }
        public IActionResult Index()
        {
            var entityList = entityValueTextS.GetAll();
            return View(entityList);
        }
        public IActionResult Create()
        {
            return View("AddUpdate", new ENTITY_VALUE_TEXT());
        }
        [HttpPost]
        public IActionResult AddUpdate(ENTITY_VALUE_TEXT obj)
        {
            EQResult eQResult = new EQResult();
            if (ModelState.IsValid)
            {
                eQResult = entityValueTextS.Insert(obj, user_session.USER_ID);
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
                var entity = entityValueTextS.GetById(id);
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
            EQResult eQResult = entityValueTextS.Delete(id);
            return Json(eQResult);
        }
    }
}
