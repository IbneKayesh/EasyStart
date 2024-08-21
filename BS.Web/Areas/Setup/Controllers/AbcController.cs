using Microsoft.AspNetCore.Mvc;

namespace BS.Web.Areas.Setup.Controllers
{
    public class AbcController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
