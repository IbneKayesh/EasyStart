using BS.Infra.Services.Application;

namespace BS.Web.Areas.Application.Controllers
{
    [Area("Application")]
    public class ClassicMenuController : BaseController
    {
        private readonly ClassicMenuService classicMenuS;
        public ClassicMenuController(ClassicMenuService _classicMenuService)
        {
            classicMenuS = _classicMenuService;
        }
        public IActionResult Index(string prev, string next)
        {
            var entityList = classicMenuS.GetAll(prev, next);
            return View(entityList);
        }
    }
}
