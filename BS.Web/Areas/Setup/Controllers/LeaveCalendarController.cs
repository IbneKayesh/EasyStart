

namespace BS.Web.Areas.Setup.Controllers
{
    [Area("Setup")]
    public class LeaveCalendarController : BaseController
    {
        private readonly LeaveCalendarService leaveCalendarS;
        private readonly FinancialYearService financialYearS;
        private readonly LeaveTypeService leaveTypeS;
        public LeaveCalendarController(LeaveCalendarService _leaveCalendarService, FinancialYearService _financialYearService,
            LeaveTypeService _leaveTypeService)
        {
            leaveCalendarS = _leaveCalendarService;
            financialYearS = _financialYearService;
            leaveTypeS = _leaveTypeService;
        }
        public IActionResult Index(string id, string leaveType)
        {
            var entityList = leaveCalendarS.GetAll(id, leaveType);
            return View(entityList);
        }
        public IActionResult Create()
        {
            Dropdown_CreateEdit();
            return View("AddUpdate", new LEAVE_CALENDAR());
        }
        [HttpPost]
        public IActionResult AddUpdate(LEAVE_CALENDAR obj)
        {
            Dropdown_CreateEdit();
            EQResult eQResult = new EQResult();
            if (ModelState.IsValid)
            {
                eQResult = leaveCalendarS.Insert(obj, user_session.USER_ID);
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
            return View(obj);
        }
        public IActionResult Edit(string id)
        {
            Dropdown_CreateEdit();
            if (!string.IsNullOrWhiteSpace(id))
            {
                var entity = leaveCalendarS.GetById(id);
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
        private void Dropdown_CreateEdit()
        {
            ViewBag.FINANCIAL_YEAR_ID = new SelectList(financialYearS.GetAllActive(), "ID", "YEAR_NAME");
            ViewBag.LEAVE_TYPE_ID = new SelectList(leaveTypeS.GetAllActive(), "ID", "LEAVE_TYPE_NAME");
        }
        public IActionResult Delete(string id)
        {
            EQResult eQResult = leaveCalendarS.Delete(id);
            return Json(eQResult);
        }
    }
}
