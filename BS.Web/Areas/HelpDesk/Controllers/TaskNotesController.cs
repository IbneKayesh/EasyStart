using Microsoft.AspNetCore.Mvc;

namespace BS.Web.Areas.HelpDesk.Controllers
{
    public class TaskNotesController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
//Sales Booking > Contact Autocomplete make generic, make SQL format with like and IN () key
//Open pages as tab instead of new window
//product warranty claim

//BUSINESS_LINE, SUB_SECTIONS_BUSINESS_LINE