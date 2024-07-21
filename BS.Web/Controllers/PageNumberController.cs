namespace BS.Web.Controllers
{
    public class PageNumberController : Controller
    {
        public IActionResult Index()
        {
            List<PageNumberInfo> pageNumberInfos =
            [
                new PageNumberInfo
                {
                    AreaID =2, Area = "Setup", ControllerID=1, Controller= "CountryInfo", ActionID = 1, Action ="Index", PageID = "2-1-1", PageInfo="Index - Country Information"
                },
                new PageNumberInfo
                {
                    AreaID =2, Area = "Setup", ControllerID=1, Controller= "CountryInfo", ActionID = 2, Action ="Create", PageID = "2-1-2", PageInfo="Create/Edit - Country Information"
                },
                new PageNumberInfo
                {
                    AreaID =2, Area = "Setup", ControllerID=2, Controller= "BankInfo", ActionID = 1, Action ="Index", PageID = "2-2-1", PageInfo="Index - Bank Information"
                },
                new PageNumberInfo
                {
                    AreaID =2, Area = "Setup", ControllerID=2, Controller= "BankInfo", ActionID = 2, Action ="Create", PageID = "2-2-2", PageInfo="Create/Edit - Bank Information"
                },
                new PageNumberInfo
                {
                    AreaID =2, Area = "Setup", ControllerID=3, Controller= "BankBranch", ActionID = 1, Action ="Index", PageID = "2-3-1", PageInfo="Index - Bank Branch Information"
                },
                new PageNumberInfo
                {
                    AreaID =2, Area = "Setup", ControllerID=3, Controller= "BankBranch", ActionID = 2, Action ="Create", PageID = "2-3-2", PageInfo="Create/Edit - Bank Branch Information"
                },
                new PageNumberInfo
                {
                    AreaID =2, Area = "Setup", ControllerID=4, Controller= "FinancialYear", ActionID = 1, Action ="Index", PageID = "2-4-1", PageInfo="Index - Financial Year Information"
                },
                new PageNumberInfo
                {
                    AreaID =2, Area = "Setup", ControllerID=4, Controller= "FinancialYear", ActionID = 2, Action ="Create", PageID = "2-4-2", PageInfo="Create/Edit - Financial Year Information"
                },
                new PageNumberInfo
                {
                    AreaID =2, Area = "Setup", ControllerID=5, Controller= "CurrencyInfo", ActionID = 1, Action ="Index", PageID = "2-5-1", PageInfo="Index - Currency Information"
                },
                new PageNumberInfo
                {
                    AreaID =2, Area = "Setup", ControllerID=5, Controller= "CurrencyInfo", ActionID = 2, Action ="Create", PageID = "2-5-2", PageInfo="Create/Edit - Currency Information"
                },
                new PageNumberInfo
                {
                    AreaID =2, Area = "Setup", ControllerID=6, Controller= "CurrencyConvRate", ActionID = 1, Action ="Index", PageID = "2-6-1", PageInfo="Index - Currency Conv Rate"
                },
                new PageNumberInfo
                {
                    AreaID =2, Area = "Setup", ControllerID=6, Controller= "CurrencyConvRate", ActionID = 2, Action ="Create", PageID = "2-6-2", PageInfo="Create/Edit - Currency Conv Rate"
                },
                new PageNumberInfo
                {
                    AreaID =2, Area = "Setup", ControllerID=7, Controller= "LeaveType", ActionID = 1, Action ="Index", PageID = "2-7-1", PageInfo="Index - Leave Type"
                },
                new PageNumberInfo
                {
                    AreaID =2, Area = "Setup", ControllerID=7, Controller= "LeaveType", ActionID = 2, Action ="Create", PageID = "2-7-2", PageInfo="Create/Edit - Leave Type"
                },
                new PageNumberInfo
                {
                    AreaID =2, Area = "Setup", ControllerID=8, Controller= "LeaveCalendar", ActionID = 1, Action ="Index", PageID = "2-8-1", PageInfo="Index - Leave Calendar"
                },
                new PageNumberInfo
                {
                    AreaID =2, Area = "Setup", ControllerID=8, Controller= "LeaveCalendar", ActionID = 2, Action ="Create", PageID = "2-8-2", PageInfo="Create/Edit - Leave Calendar"
                },
            ];
            return View(pageNumberInfos.OrderByDescending(x=>x.ControllerID).ThenByDescending(t=>t.AreaID));
        }
    }
    public class PageNumberInfo()
    {
        public int AreaID { get; set; }
        public string Area { get; set; }

        public int ControllerID { get; set; }
        public string Controller { get; set; }

        public int ActionID { get; set; }
        public string Action { get; set; }

        public string PageID { get; set; }
        public string PageInfo { get; set; }
    }
}
