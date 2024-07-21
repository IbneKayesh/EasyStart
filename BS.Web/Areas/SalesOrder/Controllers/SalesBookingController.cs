using BS.DMO.ViewModels.SalesOrder;

namespace BS.Web.Areas.SalesOrder.Controllers
{
    [Area("SalesOrder")]
    public class SalesBookingController : BaseController
    {
        private readonly SalesBookingService salesBookingS;
        public SalesBookingController(SalesBookingService _salesBookingService)
        {
            salesBookingS = _salesBookingService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            //@ViewData["PageNo"] = "312";
            //Dropdown_CreateEdit();
            var obj = salesBookingS.NewSalesBooking();
            return View("AddUpdate", obj);
        }
        [HttpPost]
        public IActionResult AddUpdate(NEW_SB_VM obj)
        {
            //@ViewData["PageNo"] = "312";
            //Dropdown_CreateEdit();
            EQResult eQResult = new EQResult();
            if (ModelState.IsValid)
            {
                eQResult = salesBookingS.Insert(obj, UserId);
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
    }
}
