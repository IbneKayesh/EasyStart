using BS.DMO.ViewModels.Company;

namespace BS.Web.Areas.Company.Controllers
{
    [Area("Company")]
    public class SubSectionController : BaseController
    {
        private readonly SubSectionService subSectionS;
        private readonly SectionService sectionS;
        private readonly SubSectionsBusinessLineService subSectionsBusinessLineS;
        private readonly BusinessLineService businessLineS;
        public SubSectionController(SubSectionService _subSectionService,
            SectionService _sectionService,
            SubSectionsBusinessLineService _subSectionsBusinessLineService,
            BusinessLineService _businessLineService)
        {
            subSectionS = _subSectionService;
            sectionS = _sectionService;
            subSectionsBusinessLineS = _subSectionsBusinessLineService;
            businessLineS = _businessLineService;
        }
        public IActionResult Index()
        {
            var entityList = subSectionS.GetAll();
            return View(entityList);
        }
        public IActionResult Create()
        {
            Dropdown_CreateEdit();
            return View("AddUpdate", new SUB_SECTIONS());
        }
        [HttpPost]
        public IActionResult AddUpdate(SUB_SECTIONS obj)
        {
            Dropdown_CreateEdit();
            EQResult eQResult = new EQResult();
            if (ModelState.IsValid)
            {
                eQResult = subSectionS.Insert(obj, user_session.USER_ID);
                TempData["msg"] = eQResult.messages;

                if (eQResult.success && eQResult.rows > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                var errors = UtilityService.GET_MODEL_ERRORS(ModelState);
                ModelState.AddModelError("", errors);
            }
            return View(obj);
        }
        public IActionResult Edit(string id)
        {
            Dropdown_CreateEdit();
            if (!string.IsNullOrWhiteSpace(id))
            {
                var entity = sectionS.GetById(id);
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
        private void Dropdown_CreateEdit()
        {
            ViewBag.SECTION_ID = new SelectList(sectionS.GetAllActive(), "ID", "SECTION_NAME");
        }
        public IActionResult Delete(string id)
        {
            EQResult eQResult = subSectionS.Delete(id);
            return Json(eQResult);
        }



        //Add remove business line
        public IActionResult CreateBusinesLine(string id)
        {
            Dropdown_CreateEditBusinesLine();

            var obj = new SUB_SECTIONS_BUSINESS_LINE();
            obj.SUB_SECTION_ID = id;
            return View("AddUpdateBusinesLine", obj);
        }
        [HttpPost]
        public IActionResult CreateBusinesLine(SUB_SECTIONS_BUSINESS_LINE obj)
        {
            Dropdown_CreateEditBusinesLine();

            EQResult eQResult = new EQResult();
            if (ModelState.IsValid)
            {
                eQResult = subSectionsBusinessLineS.Insert(obj, user_session.USER_ID);
                TempData["msg"] = eQResult.messages;

                if (eQResult.success && eQResult.rows > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                var errors = UtilityService.GET_MODEL_ERRORS(ModelState);
                ModelState.AddModelError("", errors);
            }
            return View("AddUpdateBusinesLine", obj);
        }
        private void Dropdown_CreateEditBusinesLine()
        {
            ViewBag.BUSINESS_LINE_ID = new SelectList(businessLineS.GetAllActive(), "ID", "BUSINESS_LINE_NAME");
        }



        public IActionResult IndexBusinesLine(string id)
        {
            var entityList = subSectionsBusinessLineS.GetAllBySubSectionID(id);
            return View(entityList);
        }

        public IActionResult DeleteBusinesLine(string id, string business_line_id)
        {
            EQResult eQResult = subSectionsBusinessLineS.Delete(id, business_line_id);
            return Json(eQResult);
        }
    }
}
