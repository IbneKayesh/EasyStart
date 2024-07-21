using BS.DMO.Models.Setup;
using BS.Infra.Services.Security;

namespace BS.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserLoginInfoService userLoginInfoService;
        public HomeController(ILogger<HomeController> logger,
            UserLoginInfoService _userLoginInfoServiceDI)
        {
            _logger = logger;
            userLoginInfoService = _userLoginInfoServiceDI;
        }

        public IActionResult Index()
        {
            //Login Log
            var sessionId = HttpContext.Session.Id;
            userLoginInfoService.AddLog("Dev", sessionId);


            string table_script = Services.Power.ModelToTable.GenerateCreateTableQuery<LEAVE_CALENDAR>();
            table_script = Services.Power.ClassObjectSanitizer.SetForSave<LEAVE_CALENDAR>();
            return View("Index",table_script);

            //return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
