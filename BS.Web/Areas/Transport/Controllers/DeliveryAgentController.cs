using BS.DMO.Models.Setup;
using BS.DMO.StaticValues;
using BS.Infra.Services.Setup;

namespace BS.Web.Areas.Transport.Controllers
{
    [Area("Transport")]
    public class DeliveryAgentController : BaseController
    {
        private readonly DeliveryAgentService deliveryAgentS;
        private readonly EntityValueTextService entityValueTextS;
        public DeliveryAgentController(DeliveryAgentService _deliveryAgentService, EntityValueTextService _entityValueTextService)
        {
            deliveryAgentS = _deliveryAgentService;
            entityValueTextS = _entityValueTextService;
        }
        public IActionResult Index()
        {
            var entityList = deliveryAgentS.GetAll();
            return View(entityList);
        }
        public IActionResult Create()
        {
            Dropdown_CreateEdit();
            return View("AddUpdate", new DELIVERY_AGENT());
        }
        [HttpPost]
        public IActionResult AddUpdate(DELIVERY_AGENT obj)
        {
            EQResult eQResult = new EQResult();
            if (ModelState.IsValid)
            {
                eQResult = deliveryAgentS.Insert(obj, user_session.USER_ID);
                TempData["msg"] = eQResult.messages;

                if (eQResult.success && eQResult.rows > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                var errors = ValidateModelData.GET_MODEL_ERRORS(ModelState);
                ModelState.AddModelError("", errors);
            }
            Dropdown_CreateEdit();
            return View(obj);
        }
        public IActionResult Edit(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var entity = deliveryAgentS.GetById(id);
                if (entity != null)
                {
                    Dropdown_CreateEdit();
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
        private void Dropdown_CreateEdit()
        {
            List<string> entityIds = new List<string>()
            {
                EntityValueText.DELIVERY_AGENT_TYPE_ID,
            };
            var entityValue = entityValueTextS.GetListByEntityID(entityIds);
            var delivery_agent = entityValue.Where(x => x.ENTITY_ID == EntityValueText.DELIVERY_AGENT_TYPE_ID).ToList();
            ViewBag.DELIVERY_AGENT_TYPE_ID = new SelectList(delivery_agent, "VALUE_ID", "VALUE_NAME", delivery_agent.FirstOrDefault(x => x.IS_DEFAULT).VALUE_ID);
        }
        public IActionResult Delete(string id)
        {
            EQResult eQResult = deliveryAgentS.Delete(id);
            return Json(eQResult);
        }
    }
}
