namespace BS.Web.Areas.HRMS.Controllers.Employee
{
    [Area("HRMS")]
    public class EmployeesController : BaseController
    {
        private readonly EmployeesService employeesS;
        private readonly DesignationService designationS;
        private readonly SubSectionService subSectionS;
        private readonly SalaryCyclesService salaryCyclesS;
        public EmployeesController(EmployeesService _employeesService, 
            DesignationService _designationService,
            SubSectionService subSectionS,
            SalaryCyclesService salaryCyclesS)
        {
            employeesS = _employeesService;
            designationS = _designationService;
            this.subSectionS = subSectionS;
            this.salaryCyclesS = salaryCyclesS;
        }
        public IActionResult Index()
        {
            var entityList = employeesS.GetAll();
            return View(ViewPathFinder.ViewName(this.GetType(), "Index"), entityList);
        }
        public IActionResult Create()
        {
            Dropdown_CreateEdit();
            return View(ViewPathFinder.ViewName(this.GetType(), "AddUpdate"), new EMPLOYEES());
        }
        [HttpPost]
        public IActionResult AddUpdate(EMPLOYEES obj)
        {
            EQResult eQResult = new EQResult();
            if (ModelState.IsValid)
            {
                eQResult = employeesS.Insert(obj, user_session.USER_ID);
                if (eQResult.success && eQResult.rows > 0)
                {
                    TempData["msg"] = eQResult.messages;
                    return RedirectToAction(nameof(Index));
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
                var entity = employeesS.GetById(id);
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
            ViewBag.GENDER_ID = new SelectList(CommonData.GetGender());
            ViewBag.MARITAL_STATUS = new SelectList(CommonData.GetMaritalStatus());
            ViewBag.BLOOD_GROUP = new SelectList(CommonData.GetBloodGroup());
            ViewBag.NATIONALITY = new SelectList(CommonData.GetNationality());
        }

        public IActionResult Delete(string id)
        {
            EQResult eQResult = employeesS.Delete(id, user_session.USER_ID);
            return Json(eQResult);
        }


        public IActionResult EditAddress(string empId, string addrId)
        {
            var obj = new EMP_ADDRESS();
            obj.EMP_ID = empId;

            if (!string.IsNullOrEmpty(addrId))
            {
                var entity = employeesS.GetAddressByID(addrId);
                if (entity != null)
                {
                    obj = entity;
                }
            }
            return View(ViewPathFinder.ViewName(GetType(), "EditAddress"), obj);
        }
        [HttpPost]
        public IActionResult EditAddress(EMP_ADDRESS obj)
        {
            EQResult eQResult = new EQResult();
            if (ModelState.IsValid)
            {
                eQResult = employeesS.InsertAddress(obj, user_session.USER_ID);
                if (eQResult.success && eQResult.rows > 0)
                {
                    TempData["msg"] = eQResult.messages;
                }
                else
                {
                    TempData["msg"] = eQResult.messages;
                }
            }
            else
            {
                var errors = ValidateModelData.GET_MODEL_ERRORS(ModelState);
                TempData["msg"] = NotifyService.Error(errors);
            }
            return RedirectToAction(nameof(Edit), new { id = obj.EMP_ID });
        }

        public IActionResult EditExperience(string empId, string expId)
        {
            var obj = new EMP_EXPERIENCE();
            obj.EMP_ID = empId;

            if (!string.IsNullOrEmpty(expId))
            {
                var entity = employeesS.GetExperienceByID(expId);
                if (entity != null)
                {
                    obj = entity;
                }
            }
            return View(ViewPathFinder.ViewName(GetType(), "EditExperience"), obj);
        }
        [HttpPost]
        public IActionResult EditExperience(EMP_EXPERIENCE obj)
        {
            EQResult eQResult = new EQResult();
            if (ModelState.IsValid)
            {
                eQResult = employeesS.InsertExperience(obj, user_session.USER_ID);
                if (eQResult.success && eQResult.rows > 0)
                {
                    TempData["msg"] = eQResult.messages;
                }
                else
                {
                    TempData["msg"] = eQResult.messages;
                }
            }
            else
            {
                var errors = ValidateModelData.GET_MODEL_ERRORS(ModelState);
                TempData["msg"] = NotifyService.Error(errors);
            }
            return RedirectToAction(nameof(Edit), new { id = obj.EMP_ID });
        }

        public IActionResult EditEdu(string empId, string eduId)
        {
            var obj = new EMP_EDU();
            obj.EMP_ID = empId;

            if (!string.IsNullOrEmpty(eduId))
            {
                var entity = employeesS.GetEduByID(eduId);
                if (entity != null)
                {
                    obj = entity;
                }
            }
            return View(ViewPathFinder.ViewName(GetType(), "EditEdu"), obj);
        }
        [HttpPost]
        public IActionResult EditEdu(EMP_EDU obj)
        {
            EQResult eQResult = new EQResult();
            if (ModelState.IsValid)
            {
                eQResult = employeesS.InsertEdu(obj, user_session.USER_ID);
                if (eQResult.success && eQResult.rows > 0)
                {
                    TempData["msg"] = eQResult.messages;
                }
                else
                {
                    TempData["msg"] = eQResult.messages;
                }
            }
            else
            {
                var errors = ValidateModelData.GET_MODEL_ERRORS(ModelState);
                TempData["msg"] = NotifyService.Error(errors);
            }
            return RedirectToAction(nameof(Edit), new { id = obj.EMP_ID });
        }

        public IActionResult EditDesignation(string empId, string desigId)
        {
            EditDesignation();
            var obj = new EMP_DESIG();
            obj.EMP_ID = empId;

            if (!string.IsNullOrEmpty(desigId))
            {
                var entity = employeesS.GetDesignationByID(desigId);
                if (entity != null)
                {
                    obj = entity;
                }
            }
            return View(ViewPathFinder.ViewName(GetType(), "EditDesignation"), obj);
        }
        [HttpPost]
        public IActionResult EditDesignation(EMP_DESIG obj)
        {
            EQResult eQResult = new EQResult();
            if (ModelState.IsValid)
            {
                eQResult = employeesS.InsertDesignation(obj, user_session.USER_ID);
                if (eQResult.success && eQResult.rows > 0)
                {
                    TempData["msg"] = eQResult.messages;
                }
                else
                {
                    TempData["msg"] = eQResult.messages;
                }
            }
            else
            {
                var errors = ValidateModelData.GET_MODEL_ERRORS(ModelState);
                TempData["msg"] = NotifyService.Error(errors);
            }
            return RedirectToAction(nameof(Edit), new { id = obj.EMP_ID });
        }
        private void EditDesignation()
        {
            ViewBag.DESIG_ID = new SelectList(designationS.GetAllActive(), "ID", "DESIGNATION_NAME");
            ViewBag.SUB_SECTION_ID = new SelectList(subSectionS.GetAllActiveForDropDown(), "ID", "SUB_SECTION_NAME");
        }

        public IActionResult EditSalaryCycles(string empId, string cycleId)
        {
            EditSalaryCycles();
            var obj = new EMP_SALARY_CYCLES();
            obj.EMP_ID = empId;

            if (!string.IsNullOrEmpty(cycleId))
            {
                var entity = employeesS.GetSalaryCyclesByID(cycleId);
                if (entity != null)
                {
                    obj = entity;
                }
            }
            return View(ViewPathFinder.ViewName(GetType(), "EditSalaryCycles"), obj);
        }
        [HttpPost]
        public IActionResult EditSalaryCycles(EMP_SALARY_CYCLES obj)
        {
            EQResult eQResult = new EQResult();
            if (ModelState.IsValid)
            {
                eQResult = employeesS.InsertSalaryCycles(obj, user_session.USER_ID);
                if (eQResult.success && eQResult.rows > 0)
                {
                    TempData["msg"] = eQResult.messages;
                }
                else
                {
                    TempData["msg"] = eQResult.messages;
                }
            }
            else
            {
                var errors = ValidateModelData.GET_MODEL_ERRORS(ModelState);
                TempData["msg"] = NotifyService.Error(errors);
            }
            return RedirectToAction(nameof(Edit), new { id = obj.EMP_ID });
        }
        private void EditSalaryCycles()
        {
            ViewBag.SALARY_CYCLES_ID = new SelectList(salaryCyclesS.GetAllActive(), "ID", "CYCLE_NAME");
        }

        //API
        [HttpPost]
        public IActionResult FindEmployee(string search_term)
        {
            var obj = employeesS.GetByName(search_term.ToLower());
            return Json(obj);
        }
    }
}
