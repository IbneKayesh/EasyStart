namespace BS.Web.Areas.SalesOrder.Controllers
{
    [Area("SalesOrder")]
    public class SalesBookingController : BaseController
    {
        private readonly SalesBookingService salesBookingS;
        private readonly SubSectionService subSectionS;
        private readonly EntityValueTextService entityValueTextS;
        public SalesBookingController(SalesBookingService _salesBookingService,
            SubSectionService _subSectionService,
            EntityValueTextService _entityValueTextService)
        {
            salesBookingS = _salesBookingService;
            subSectionS = _subSectionService;
            entityValueTextS = _entityValueTextService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            Dropdown_CreateEdit();
            var obj = salesBookingS.NewSalesBooking(user_session.USER_ID, user_session.USER_NAME);
            return View("AddUpdate", obj);
        }
        [HttpPost]
        public IActionResult AddUpdate(NEW_SB_VM obj)
        {
            Dropdown_CreateEdit();
            EQResult eQResult = new EQResult();
            if (ModelState.IsValid)
            {
                eQResult = salesBookingS.Insert(obj, user_session.USER_ID);
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
        private void Dropdown_CreateEdit()
        {
            ViewBag.SUB_SECTION_ID = new SelectList(subSectionS.GetAllSalesBooking(), "ID", "SUB_SECTION_NAME");
            var entityValue = entityValueTextS.GetListByEntityID(EntityValueText.BOOKING_SOURCE);

            ViewBag.TRN_SOURCE_ID = new SelectList(entityValue, "VALUE_ID", "TEXT_ID");
        }
    }
}
