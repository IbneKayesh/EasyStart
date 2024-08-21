namespace BS.Web.Areas.Application.Controllers
{
    [Area("Application")]
    public class NotificationsController : BaseController
    {
        private readonly AppNotificationsService appNotificationsS;
        public NotificationsController(AppNotificationsService _appNotificationsService)
        {
            appNotificationsS = _appNotificationsService;
        }
        public IActionResult Index()
        {
            var entityList = appNotificationsS.GetAll();
            return View(entityList);
        }
    }
}
