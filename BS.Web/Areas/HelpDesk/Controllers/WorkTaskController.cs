using BS.DMO.Models.HelpDesk;
using Microsoft.AspNetCore.Mvc;

namespace BS.Web.Areas.HelpDesk.Controllers
{
    [Area("HelpDesk")]
    public class WorkTaskController : BaseController
    {
        private readonly WorkTaskService workTaskS;
        public WorkTaskController(WorkTaskService _workTaskService)
        {
            workTaskS = _workTaskService;
        }
        public IActionResult Index()
        {
            var entityList = workTaskS.GetAll();
            return View(entityList);
        }
       
        
       
        public IActionResult Delete(string id)
        {
            EQResult eQResult = workTaskS.Delete(id);
            return Json(eQResult);
        }
    }
}
//make SQL format with like and IN () key
//product warranty claim
//application menu and role menu

//product
//order booking detail
//contact documents
//product category picture