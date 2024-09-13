using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace BS.Web.Areas.HRMS.Controllers.Employee
{
    [Area("HRMS")]
    public class EmployeeWorkShiftController : BaseController
    {
        private readonly EmpWorkShiftService EmpWorkShiftS;
        private readonly WorkShiftService WorkShiftS;
        public EmployeeWorkShiftController(EmpWorkShiftService empWorkShiftService, WorkShiftService workShiftS)
        {
            EmpWorkShiftS = empWorkShiftService;
            WorkShiftS = workShiftS;
        }
        public IActionResult Index(string empId)
        {
            var entityList = EmpWorkShiftS.GetByEmpID(empId);
            return View(ViewPathFinder.ViewName(this.GetType(), "Index"), entityList);
        }
        public IActionResult Create(string empId)
        {
            Dropdown_CreateEdit();
            var obj = new EMP_WORK_SHIFT();
            obj.EMP_ID = empId;
            return View(ViewPathFinder.ViewName(this.GetType(), "AddUpdate"), obj);
        }
        [HttpPost]
        public IActionResult AddUpdate(EMP_WORK_SHIFT obj)
        {
            EQResult eQResult = new EQResult();
            if (ModelState.IsValid)
            {
                eQResult = EmpWorkShiftS.Insert(obj, user_session.USER_ID);
                if (eQResult.success && eQResult.rows > 0)
                {
                    TempData["msg"] = eQResult.messages;
                    return RedirectToAction(nameof(Index), new { empId = obj.EMP_ID });
                }
                else
                {
                    ModelState.AddModelError("", eQResult.messages);
                }
            }
            else
            {
                var errors = ValidateModelData.GET_MODEL_ERRORS(ModelState);
                ModelState.AddModelError("", errors);
            }
            Dropdown_CreateEdit();
            return View(ViewPathFinder.ViewName(this.GetType(), "AddUpdate"), obj);
        }
        public IActionResult Edit(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var entity = EmpWorkShiftS.GetById(id);
                if (entity != null)
                {
                    Dropdown_CreateEdit();
                    return View(ViewPathFinder.ViewName(this.GetType(), "AddUpdate"), entity);
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

        private void Dropdown_CreateEdit()
        {
            ViewBag.WORK_SHIFT_ID = new SelectList(WorkShiftS.GetAllActive(), "ID", "SHIFT_NAME");
        }

        public IActionResult Delete(string id)
        {
            EQResult eQResult = EmpWorkShiftS.Delete(id);
            return Json(eQResult);
        }
    }
}