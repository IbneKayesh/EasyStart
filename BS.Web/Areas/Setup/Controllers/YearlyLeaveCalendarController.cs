namespace BS.Web.Areas.Setup.Controllers
{
    [Area("Setup")]
    public class YearlyLeaveCalendarController : BaseController
    {
        private readonly HolidayCalendarService leaveCalendarS;
        private readonly FinancialYearService financialYearS;
        private readonly HolidayTypeService HolidayTypeS;
        public YearlyLeaveCalendarController(HolidayCalendarService _leaveCalendarService, FinancialYearService _financialYearService,
            HolidayTypeService holidayTypeService)
        {
            leaveCalendarS = _leaveCalendarService;
            financialYearS = _financialYearService;
            HolidayTypeS = holidayTypeService;
        }
        public IActionResult Index(string id, string leaveType)
        {
            var entityList = leaveCalendarS.GetAllYearlyLeaveCalendar(id, leaveType);
            return View(entityList);
        }
        public IActionResult Create()
        {
            Dropdown_CreateEdit();
            return View("AddUpdate", new YEARLY_LEAVE_CALENDAR());
        }
        [HttpPost]
        public IActionResult AddUpdate(YEARLY_LEAVE_CALENDAR obj)
        {
            Dropdown_CreateEdit();
            EQResult eQResult = new EQResult();
            if (ModelState.IsValid)
            {
                eQResult = leaveCalendarS.InsertYearlyLeaveCalendar(obj, user_session.USER_ID);
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
                var entity = leaveCalendarS.GetYearlyLeaveCalendarById(id);
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
            ViewBag.HOLIDAY_TYPE_ID = new SelectList(HolidayTypeS.GetAllActiveRequireApplication(), "ID", "HOLIDAY_TYPE_NAME");
        }
        public IActionResult Delete(string id)
        {
            EQResult eQResult = leaveCalendarS.DeleteYearlyLeaveCalendar(id);
            return Json(eQResult);
        }
        public IActionResult ProcessEmployeeYearlyLeaveCalendar(string fyID)
        {
            EQResult eQResult = leaveCalendarS.ProcessEmployeeYearlyLeaveCalendar(fyID, user_session.USER_ID);
            //return Json(eQResult);
            TempData["msg"] = NotifyService.Success(eQResult.rows + " Rows procceed");
            return RedirectToAction(nameof(Index));
        }
    }
}
