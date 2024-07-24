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

//PRODUCT_SOURCE, PRODUCT_STATUS, PRODUCT_TYPE, UNIT_MASTER, UNIT_CHILD, SEGMENT_NAME_TYPE, BUSINESS_LINE, SUB_SECTIONS_BUSINESS_LINE