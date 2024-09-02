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

            ViewBag.CONTACT_CATEGORY_ID = new SelectList(entityValue, "VALUE_ID", "VALUE_NAME", entityValue.FirstOrDefault(x => x.IS_DEFAULT).VALUE_ID);
        }
        public IActionResult Delete(string id)
        {
            EQResult eQResult = contactsS.Delete(id);
            return Json(eQResult);
        }


        //Contact Address
        public IActionResult ContactAddress(string contactId)
        {
            var entityList = contactsS.GetAll_ContactAddress(contactId);
            return View(entityList);
        }
        public IActionResult CreateContactAddress(string contactId)
        {
            var obj = new CONTACT_ADDRESS();
            obj.CONTACT_ID = contactId;
            return View("AddUpdateContactAddress", obj);
        }
        public IActionResult EditContactAddress(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var entity = contactsS.GetById_ContactAddress(id);
                if (entity != null)
                {
                    return View("AddUpdateContactAddress", entity);
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

        [HttpPost]
        public IActionResult AddUpdateContactAddress(CONTACT_ADDRESS obj)
        {
            EQResult eQResult = new EQResult();
            if (ModelState.IsValid)
            {
                eQResult = contactsS.Insert_ContactAddress(obj, user_session.USER_ID);
                TempData["msg"] = eQResult.messages;

                if (eQResult.success && eQResult.rows > 0)
                {
                    return RedirectToAction(nameof(ContactAddress), new { contactId = obj.CONTACT_ID });
                }
            }
            else
            {
                var errors = UtilityService.GET_MODEL_ERRORS(ModelState);
                ModelState.AddModelError("", errors);
            }
            return View(obj);
        }
        public IActionResult DeleteContactAddress(string id)
        {
            EQResult eQResult = contactsS.Delete_ContactAddress(id);
            return Json(eQResult);
        }


        //API
        //[HttpPost]
        //public IActionResult FindCustomer(string search_term)
        //{
        //    var obj = contactsS.GetByName(search_term.ToLower());
        //    return Json(obj);
        //}
        public IActionResult ShowCustomer()
        {
            var obj = contactsS.GetAllCustomer();
            return Json(obj);
        }
        [HttpPost]
        public IActionResult FindCustomerBillTo(string contactId)
        {
            var obj = contactsS.GetAllCustomerGroupById(contactId);
            return Json(obj);
        }
        public IActionResult FindCustomerDefaultAddress(string customerId)
        {
            var obj = contactsS.GetById_ContactDefaultAddress(customerId);
            return Json(obj);
        }
        public IActionResult FindCustomerAddress(string customerId)
        {
            var obj = contactsS.GetAll_ContactAddress(customerId);
            return Json(obj);
        }
    }
}
