namespace BS.Web.Areas.HelpDesk.Controllers
{
    [Area("HelpDesk")]
    public class BoardsController : BaseController
    {
        private readonly BoardService boardS;
        private readonly EntityValueTextService entityValueTextS;
        private readonly WorkTaskService workTaskS;
        private readonly TaskStatusService taskStatusS;
        public BoardsController(BoardService _boardService, EntityValueTextService _entityValueTextS,
            WorkTaskService _workTaskService, TaskStatusService _taskStatusService)
        {
            boardS = _boardService;
            entityValueTextS = _entityValueTextS;
            workTaskS = _workTaskService;
            taskStatusS = _taskStatusService;
        }
        public IActionResult Index()
        {
            var entityList = boardS.GetAll();
            return View(entityList);
        }
        public IActionResult Create()
        {
            return View("AddUpdate", new BOARDS());
        }
        [HttpPost]
        public IActionResult AddUpdate(BOARDS obj)
        {
            EQResult eQResult = new EQResult();
            if (ModelState.IsValid)
            {
                eQResult = boardS.Insert(obj, user_session.USER_ID);
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
        public IActionResult Edit(string id, string copy)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var entity = boardS.GetById(id);
                if (entity != null)
                {
                    if (!string.IsNullOrWhiteSpace(copy))
                    {
                        //asign for new save
                        ModelState.Clear();
                        entity.ID = Guid.Empty.ToString();
                    }
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
            EQResult eQResult = boardS.Delete(id);
            return Json(eQResult);
        }


        //Board Group
        public IActionResult MyBoard(string board)
        {
            var entityList = boardS.GetBoardGroupWithChildByBoardID(board);
            return View(entityList);
        }
        public IActionResult AddEditBoardGroup(string id, string board)
        {
            BOARD_GROUP entityObject = boardS.GetBoardGroupByID(id, board);
            Dropdown_AddEditBoardGroup();
            return View("AddEditBoardGroup", entityObject);
        }

        [HttpPost]
        public IActionResult AddEditBoardGroup(BOARD_GROUP obj)
        {
            EQResult eQResult = new EQResult();
            if (ModelState.IsValid)
            {
                eQResult = boardS.InsertBoardGroup(obj, user_session.USER_ID);
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
            Dropdown_AddEditBoardGroup();
            return View(obj);
        }
        private void Dropdown_AddEditBoardGroup()
        {
            var entityValue = entityValueTextS.GetListByEntityID(EntityValueText.COLOR_CODE);
            ViewBag.BS_COLOR = new SelectList(entityValue, "VALUE_ID", "VALUE_NAME", entityValue.FirstOrDefault(x => x.IS_DEFAULT).VALUE_ID);
        }

        //Add Edit Work Task
        public IActionResult AddWorkTask(string id, string board, string bgid)
        {
            Dropdown_AddEditWorkTask(board);
            WORK_TASK obj = new WORK_TASK();
            if (!string.IsNullOrWhiteSpace(id))
            {
                obj.ID = id;
            }
            obj.BG_ID = bgid;
            obj.BOARD_ID = board;
            return View("AddEditWorkTask", obj);
        }
        [HttpPost]
        public IActionResult AddEditWorkTask(WORK_TASK obj)
        {
            EQResult eQResult = new EQResult();
            if (ModelState.IsValid)
            {
                eQResult = workTaskS.Insert(obj, user_session.USER_ID);
                TempData["msg"] = eQResult.messages;

                if (eQResult.success && eQResult.rows > 0)
                {
                    return RedirectToAction("MyBoard", new { board = obj.BOARD_ID});
                }
            }
            else
            {
                var errors = UtilityService.GET_MODEL_ERRORS(ModelState);
                ModelState.AddModelError("", errors);
            }
            //board id :: error
            Dropdown_AddEditWorkTask(obj.BG_ID);
            return View(obj);
        }
        public IActionResult EditWorkTask(string id, string board, string bgid, string copy)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var entity = workTaskS.GetById(id);
                if (entity != null)
                {
                    //Clear for Not Mapped board ID
                    ModelState.Clear();
                    entity.BOARD_ID = board;

                    if (!string.IsNullOrWhiteSpace(copy))
                    {
                        //asign for new save
                        entity.ID = Guid.Empty.ToString();
                    }
                    Dropdown_AddEditWorkTask(board);
                    return View("AddEditWorkTask", entity);
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
        private void Dropdown_AddEditWorkTask(string board)
        {
            ViewBag.BG_ID = new SelectList(boardS.GetBoardGroupByBoardID(board), "ID", "GROUP_NAME");
            ViewBag.STATUS_ID = new SelectList(taskStatusS.GetAllActive(), "ID", "STATUS_NAME");
            
            List<string> entityIds = new List<string>()
            {
                EntityValueText.WT_TYPE,
                EntityValueText.STATUS_ID,
                EntityValueText.PRIORITY_ID
            };
            var entityValue = entityValueTextS.GetListByEntityID(entityIds);
            var wt_type = entityValue.Where(x => x.ENTITY_ID == EntityValueText.WT_TYPE).ToList();
            var status_id = entityValue.Where(x => x.ENTITY_ID == EntityValueText.STATUS_ID).ToList();
            var priority_id = entityValue.Where(x => x.ENTITY_ID == EntityValueText.PRIORITY_ID).ToList();

            ViewBag.WT_TYPE = new SelectList(wt_type, "VALUE_ID", "VALUE_NAME", wt_type.FirstOrDefault(x => x.IS_DEFAULT).VALUE_ID);
            //ViewBag.STATUS_ID = new SelectList(status_id, "VALUE_ID", "VALUE_NAME", status_id.FirstOrDefault(x => x.IS_DEFAULT).VALUE_ID);
            ViewBag.PRIORITY_ID = new SelectList(priority_id, "VALUE_ID", "VALUE_NAME", priority_id.FirstOrDefault(x => x.IS_DEFAULT).VALUE_ID);
        }
    }
}