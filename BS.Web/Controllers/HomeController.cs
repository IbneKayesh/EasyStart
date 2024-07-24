using BS.DMO.Models.Inventory;

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
            string table_script = Services.Power.ModelToTable.GenerateCreateTableQuery<PRODUCT_STATUS>();
            //table_script = Services.Power.ClassObjectSanitizer.SetForSave<PRODUCT_CLASS>();
            //table_script = Services.Power.RazorSanitizer.Create<PRODUCT_CLASS>();
            //table_script = Services.Power.RazorSanitizer.Select<PRODUCT_CLASS>();

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
            userLoginInfoService.AddLog("Dev", sessionId);


            var UserSession = new USER_SESSION()
            {
                SESSION_ID = sessionId,
                USER_ID = "Dev User 1",
                USER_NAME = "Zakia",
                USER_EMAIL = "zakia@gmail.com"
            };
            HttpContext.Session.Set<USER_SESSION>(StaticKeys.SessionName, UserSession);

            TempData["msg"] = AlertifyJsService.Success(UserSession.USER_NAME + " logged in successfully");

            return RedirectToAction("Index");
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
