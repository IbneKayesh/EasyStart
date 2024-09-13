namespace BS.Web.Areas.SalesOrder.Controllers
{
    [Area("SalesOrder")]
    public class SalesBookingController : BaseController
    {
        private readonly SalesBookingService salesBookingS;
        private readonly SubSectionService subSectionS;
        private readonly EntityValueTextService entityValueTextS;
        private readonly ContactsService contactsS;
        private readonly ItemSubGroupService ItemSubGroupS;
        public SalesBookingController(SalesBookingService _salesBookingService,
            SubSectionService _subSectionService,
            EntityValueTextService _entityValueTextService, ContactsService _contactsService,
            ItemSubGroupService itemSubGroupService)
        {
            salesBookingS = _salesBookingService;
            subSectionS = _subSectionService;
            entityValueTextS = _entityValueTextService;
            contactsS = _contactsService;
            ItemSubGroupS = itemSubGroupService;
        }

        public IActionResult Index()
        {
            var objList = salesBookingS.GetAllUnposted();
            return View(objList);
        }
        public IActionResult Create()
        {
            Dropdown_CreateEdit();
            var obj = salesBookingS.NewSalesBooking(user_session.USER_ID, user_session.USER_NAME);
            return View("AddUpdate", obj);
        }
        public IActionResult Edit(string id)
        {
            Dropdown_CreateEdit();
            if (!string.IsNullOrWhiteSpace(id))
            {
                var entity = salesBookingS.GetById(id);
                if (entity != null)
                {
                    return View("AddUpdate", entity);
                }
                else
                {
                    TempData["msg"] = NotifyService.NotFound();
                }
            }
            return RedirectToAction("Create");
        }
        [HttpPost]
        public IActionResult AddUpdate(NEW_SB_VM obj)
        {
            Dropdown_CreateEdit();
            EQResult eQResult = new EQResult();
            if (ModelState.IsValid)
            {
                eQResult = salesBookingS.Insert(obj, user_session.USER_ID);
                if (eQResult.success)
                {
                    if (eQResult.success && eQResult.rows > 0)
                    {
                        TempData["msg"] = eQResult.messages;
                        return RedirectToAction(nameof(Index));
                    }
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
            return View(obj);
        }
        private void Dropdown_CreateEdit()
        {
            var sub_section_id = new SelectList(subSectionS.GetAllByTrnID(TransactionID.SB), "ID", "SUB_SECTION_NAME");
            ViewBag.FROM_SUB_SECTION_ID = sub_section_id;
            ViewBag.TO_SUB_SECTION_ID = sub_section_id;
            ViewBag.CONTACT_ID = new SelectList(contactsS.GetAllActive(), "ID", "CONTACT_NAME");

            List<string> entityIds = new List<string>()
            {
                EntityValueText.BOOKING_SOURCE,
                EntityValueText.SB_TRN_TYPE_ID,
                EntityValueText.SHIPPING_MODE_ID,
                EntityValueText.SHIPPING_TYPE_ID,
                EntityValueText.PAYMENT_MODE,
                EntityValueText.PAYMENT_METHOD
            };
            var entityValue = entityValueTextS.GetListByEntityID(entityIds);

            //var entityValue = entityValueTextS.GetAll();
            var booking_source = entityValue.Where(x => x.ENTITY_ID == EntityValueText.BOOKING_SOURCE).ToList();
            var sb_trn_type_id = entityValue.Where(x => x.ENTITY_ID == EntityValueText.SB_TRN_TYPE_ID).ToList();
            var shipping_mode_id = entityValue.Where(x => x.ENTITY_ID == EntityValueText.SHIPPING_MODE_ID).ToList();
            var shipping_type_id = entityValue.Where(x => x.ENTITY_ID == EntityValueText.SHIPPING_TYPE_ID).ToList();
            var payment_mode = entityValue.Where(x => x.ENTITY_ID == EntityValueText.PAYMENT_MODE).ToList();
            var payment_method = entityValue.Where(x => x.ENTITY_ID == EntityValueText.PAYMENT_METHOD).ToList();


            ViewBag.TRN_SOURCE_ID = new SelectList(booking_source, "VALUE_ID", "VALUE_NAME", booking_source.FirstOrDefault(x => x.IS_DEFAULT).VALUE_ID);
            ViewBag.TRN_TYPE_ID = new SelectList(sb_trn_type_id, "VALUE_ID", "VALUE_NAME", sb_trn_type_id.FirstOrDefault(x => x.IS_DEFAULT).VALUE_ID);
            ViewBag.SHIPPING_MODE_ID = new SelectList(shipping_mode_id, "VALUE_ID", "VALUE_NAME", shipping_mode_id.FirstOrDefault(x => x.IS_DEFAULT).VALUE_ID);
            ViewBag.SHIPPING_TYPE_ID = new SelectList(shipping_type_id, "VALUE_ID", "VALUE_NAME", shipping_type_id.FirstOrDefault(x => x.IS_DEFAULT).VALUE_ID);
            ViewBag.PAYMENT_MODE = new SelectList(payment_mode, "VALUE_ID", "VALUE_NAME", payment_mode.FirstOrDefault(x => x.IS_DEFAULT).VALUE_ID);
            ViewBag.PAYMENT_METHOD = new SelectList(payment_method, "VALUE_ID", "VALUE_NAME", payment_method.FirstOrDefault(x => x.IS_DEFAULT).VALUE_ID);
            ViewBag.ITEM_SUB_GROUP_ID = new SelectList(ItemSubGroupS.GetForSales(), "ID", "ITEM_SUB_GROUP_NAME");
        }
        public IActionResult Delete(string id)
        {
            EQResult eQResult = salesBookingS.Delete(id);
            return Json(eQResult);
        }
    }
}
