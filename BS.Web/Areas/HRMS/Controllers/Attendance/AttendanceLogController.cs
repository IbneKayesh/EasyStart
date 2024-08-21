namespace BS.Web.Areas.HRMS.Controllers.Attendance
{
    [Area("HRMS")]
    public class AttendanceLogController : BaseController
    {
        string emp_id = "fea490f7-a68b-4541-8c76-887f0a2054e9";
        private readonly AttendanceLogService attendanceLogS;
        public AttendanceLogController(AttendanceLogService _attendanceLogService)
        {
            attendanceLogS = _attendanceLogService;
        }
        public IActionResult Index()
        {
            var entityList = attendanceLogS.GetByEmpId(emp_id);
            return View(ViewPathFinder.ViewName(this.GetType(), "Index"), entityList);
        }
        public IActionResult Create()
        {
            DateTime date = DateTime.Now.AddDays(1);
            string tDate = date.ToString("dd-MMM-yyyy");
            string fDate = date.AddDays(-10).ToString("dd-MMM-yyyy");
            attendanceLogS.ProcessByLoginLog(fDate, tDate, emp_id, user_session.USER_ID);
            TempData["msg"] = NotifyService.Success($"{fDate} to {tDate} procceed");
            return RedirectToAction("Index");
        }
    }
}
