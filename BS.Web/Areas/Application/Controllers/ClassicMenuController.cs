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
            //1 :: check session and load all menus from db, pick only 1st nodes
            //2 :: get from session and filter them
            //List<CLASSIC_MENU_VM> entityList = new List<CLASSIC_MENU_VM>();
            List<CLASSIC_MENU> entityList = new List<CLASSIC_MENU>();
            var session = HttpContext.Session;
            if (session.GetString("menu") == null)
            {
                //string adminRoleId = "c541f83e-9ea6-4bfe-9dd5-5efb3083ee83";
                //var sessionData = classicMenuS.GetAllByRoleId(adminRoleId);
                var sessionData = classicMenuS.GetAll();
                //HttpContext.Session.Set<List<CLASSIC_MENU_VM>>("menu", sessionData);
                HttpContext.Session.Set<List<CLASSIC_MENU>>("menu", sessionData);

                entityList = (from a in sessionData
                              where a.PARENT_NODE == "N"
                              orderby a.PAGE_ID
                              select a).ToList();
            }
            else
            {
                //var sessionData = session.GetObject<List<CLASSIC_MENU_VM>>("menu");
                var sessionData = session.GetObject<List<CLASSIC_MENU>>("menu");
                if (next != null)
                {
                    entityList = (from a in sessionData
                                  where a.PARENT_NODE == next
                                  orderby a.PAGE_ID
                                  select a).ToList();

                }
                else if (prev != null)
                {
                    entityList = (from a in sessionData
                                  join b in sessionData on a.PARENT_NODE equals b.PARENT_NODE
                                  where b.MENU_ID == prev
                                  orderby a.PAGE_ID
                                  select a).ToList();
                }
                else
                {
                    entityList = (from a in sessionData
                                  where a.PARENT_NODE == "N"
                                  orderby a.PAGE_ID
                                  select a).ToList();
                }
            }
            return View(entityList);
        }

    }
}
