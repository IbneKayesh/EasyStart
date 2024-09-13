using BS.DMO.Models.Application;
using BS.DMO.Models.Security;
using BS.DMO.Models.Setup.Security;

namespace BS.Web.Areas.Security.Controllers
{
    [Area("Security")]
    public class SecurityRoleController : BaseController
    {
        private readonly SecurityRoleService securityRoleS;
        private readonly ClassicMenuService classicMenuS;
        public SecurityRoleController(SecurityRoleService _securityRoleService, ClassicMenuService _classicMenuService)
        {
            securityRoleS = _securityRoleService;
            classicMenuS = _classicMenuService;
        }
        public IActionResult Index()
        {
            var entityList = securityRoleS.GetAll();
            return View(entityList);
        }
        public IActionResult Create()
        {
            return View("AddUpdate", new SECURITY_ROLE());
        }
        [HttpPost]
        public IActionResult AddUpdate(SECURITY_ROLE obj)
        {
            EQResult eQResult = new EQResult();
            if (ModelState.IsValid)
            {
                eQResult = securityRoleS.Insert(obj, user_session.USER_ID);
                TempData["msg"] = eQResult.messages;

                if (eQResult.success && eQResult.rows > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                var errors = ValidateModelData.GET_MODEL_ERRORS(ModelState);
                ModelState.AddModelError("", errors);
            }
            return View(obj);
        }
        public IActionResult Edit(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var entity = securityRoleS.GetById(id);
                if (entity != null)
                {
                    return View("AddUpdate", entity);
                }
                else
                {
                    TempData["msg"] = NotifyService.NotFound();
                }
            }
            else
            {
                TempData["msg"] = NotifyService.Error("Invalid ID, Parameter is required");
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(string id)
        {
            EQResult eQResult = securityRoleS.Delete(id);
            return Json(eQResult);
        }



        //Add to Role


        public IActionResult MenuItems()
        {
            List<CLASSIC_MENU> entityList = classicMenuS.GetAll();
            return View(entityList);
        }
        public IActionResult AddToRole(string id, string roleId)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                if (!string.IsNullOrWhiteSpace(roleId))
                {
                    var entity = classicMenuS.GetMenuRoleByMenuIdRoleId(id, roleId);
                    if (entity != null)
                    {
                        Dropdown_CreateEdit("");
                        return View("AddToRole", entity);
                    }
                }
                Dropdown_CreateEdit(id);
                var obj = new MENU_ROLE();
                obj.MENU_ID = id;
                obj.ROLE_ID = roleId;
                return View("AddToRole", obj);
            }
            else
            {
                TempData["msg"] = NotifyService.Error("Invalid ID, Parameter is required");
            }
            return RedirectToAction(nameof(MenuItems));
        }

        [HttpPost]
        public IActionResult AddToRole(MENU_ROLE obj)
        {
            Dropdown_CreateEdit(obj.MENU_ID);
            EQResult eQResult = new EQResult();
            if (ModelState.IsValid)
            {
                eQResult = classicMenuS.InsertMenuRole(obj, user_session.USER_ID);
                TempData["msg"] = eQResult.messages;

                if (eQResult.success && eQResult.rows > 0)
                {
                    return RedirectToAction(nameof(MenuItems)); ;
                }
                else
                {
                    return RedirectToAction(nameof(MenuItems));
                }
            }
            else
            {
                var errors = ValidateModelData.GET_MODEL_ERRORS(ModelState);
                ModelState.AddModelError("", errors);
                return View(obj);
            }
        }
        private void Dropdown_CreateEdit(string menuId)
        {
            if (!string.IsNullOrWhiteSpace(menuId))
            {
                ViewBag.ROLE_ID = new SelectList(securityRoleS.GetAllActiveWithoutMenuId(menuId), "ID", "ROLE_NAME");
            }
            else
            {
                ViewBag.ROLE_ID = new SelectList(securityRoleS.GetAllActive(), "ID", "ROLE_NAME");
            }
        }

        public IActionResult MenuRoles(string id)
        {
            List<MENU_ROLE> entityList = classicMenuS.GetRoleByMenuId(id);
            return View(entityList);
        }
    }
}
