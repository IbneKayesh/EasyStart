using BS.DMO.Models.SalesOrder;

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
            string table_script = string.Empty;
            table_script = Services.Power.ModelToTable.GenerateCreateTableQuery<EMP_EDU>();
            ////table_script = Services.Power.ModelToTable.GenerateSelect<EMPLOYEES>();
            //table_script = Services.Power.ClassObjectSanitizer.SetForSave<EMPLOYEES>();
            //table_script = Services.Power.RazorSanitizer.Create<EMPLOYEES>();
            //table_script = Services.Power.RazorSanitizer.Select<EMPLOYEES>();

            return View("Index", table_script);
        }

        public IActionResult LoginLog()
        {
            var entityList = userLoginInfoService.GetAll();
            return View(entityList);
        }



        public IActionResult Login()
        {
            //Login Log
            var sessionId = HttpContext.Session.Id;
            userLoginInfoService.AddLog("fea490f7-a68b-4541-8c76-887f0a2054e9", sessionId);


            var UserSession = new USER_SESSION()
            {
                SESSION_ID = sessionId,
                USER_ID = "Dev User 1",
                USER_NAME = "Zakia",
                USER_EMAIL = "zakia@gmail.com"
            };
            HttpContext.Session.Set<USER_SESSION>(StaticKeys.SessionName, UserSession);

            TempData["msg"] = AlertifyJsService.Success(UserSession.USER_NAME + " logged in successfully");

            //return RedirectToAction("Index");
            return RedirectToAction("Index", "ClassicMenu", new { area = "Application", next = "75" });
        }
        public IActionResult Privacy()
        {
            ViewData["PageName"] = "Privacy";
            return View("Privacy");
        }
        public IActionResult Page1()
        {
            ViewData["PageName"] = "Page 1";
            return View("Privacy");
        }
        public IActionResult Page2()
        {
            ViewData["PageName"] = "Page 2";
            return View("Privacy");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
