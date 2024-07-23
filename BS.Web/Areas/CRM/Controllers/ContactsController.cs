namespace BS.Web.Areas.CRM.Controllers
{
    [Area("CRM")]
    public class ContactsController : BaseController
    {
        private readonly ContactsService contactsS;
        private readonly EntityValueTextService entityValueTextS;
        public ContactsController(ContactsService _contactsService, EntityValueTextService _entityValueTextS)
        {
            contactsS = _contactsService;
            entityValueTextS = _entityValueTextS;
        }
        public IActionResult Index()
        {
            var entityList = contactsS.GetAll();
            return View(entityList);
        }
        public IActionResult Create()
        {
            Dropdown_CreateEdit();
            return View("AddUpdate", new CONTACTS());
        }
        [HttpPost]
        public IActionResult AddUpdate(CONTACTS obj)
        {
            Dropdown_CreateEdit();
            EQResult eQResult = new EQResult();
            if (ModelState.IsValid)
            {
                eQResult = contactsS.Insert(obj, user_session.USER_ID);
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
                var entity = contactsS.GetById(id);
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
            //ViewBag.COUNTRY_ID = new SelectList(countryInfoS.GetAllActive(), "ID", "COUNTRY_NAME");
            var entityValue = entityValueTextS.GetListByEntityID(EntityValueText.CONTACT_CATEGORY_ID);

            ViewBag.CONTACT_CATEGORY_ID = new SelectList(entityValue, "VALUE_ID", "TEXT_ID", entityValue.FirstOrDefault(x => x.IS_DEFAULT).VALUE_ID);
        }
        public IActionResult Delete(string id)
        {
            EQResult eQResult = contactsS.Delete(id);
            return Json(eQResult);
        }


        //API
    }
}
