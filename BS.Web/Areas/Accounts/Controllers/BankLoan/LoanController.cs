﻿namespace BS.Web.Areas.Accounts.Controllers.BankLoan
{
    [Area("Accounts")]
    public class LoanController : BaseController
    {
        private readonly LoanService loanS;
        private readonly BranchCostCenterService branchCostCenterS;
        public LoanController(LoanService _loanService, BranchCostCenterService _branchCostCenterService)
        {
            loanS = _loanService;
            branchCostCenterS = _branchCostCenterService;
        }
        public IActionResult Index()
        {
            var entityList = loanS.GetAll();
            //entityList.ForEach(e => e.ALLOW_DELETE = false);
            return View(ViewPathFinder.ViewName(this.GetType(), "Index"), entityList);
        }
        public IActionResult Create()
        {
            Dropdown_CreateEdit();
            var obj = loanS.NewLoanMaster();
            return View(ViewPathFinder.ViewName(this.GetType(), "AddUpdate"), obj);
        }
        [HttpPost]
        public IActionResult AddUpdate(NEW_BANK_LOAN_MASTER_VM obj)
        {
            Dropdown_CreateEdit();
            EQResult eQResult = new EQResult();
            var buttonClicked = Request.Form["submitButton"];
            if (buttonClicked == "Apply")
            {
                ModelState.Clear();
                decimal TotalAmount = (obj.BANK_LOAN_MASTER.INTEREST_RATE / 100) * obj.BANK_LOAN_MASTER.LOAN_AMOUNT;
                obj.BANK_LOAN_MASTER.TOTAL_AMOUNT = obj.BANK_LOAN_MASTER.LOAN_AMOUNT + TotalAmount;
                obj.BANK_LOAN_MASTER.DUE_AMOUNT = obj.BANK_LOAN_MASTER.LOAN_AMOUNT + TotalAmount;
                obj.BANK_LOAN_MASTER.FINE_AMOUNT = 0;
                if (obj.BANK_LOAN_MASTER.LOAN_AMOUNT < 1)
                {
                    ModelState.AddModelError("", "Enter correct loan amount");
                    obj.BANK_LOAN_SCHEDULE = new List<BANK_LOAN_SCHEDULE>();
                }
                if (obj.BANK_LOAN_MASTER.NO_OF_SCHEDULE < 1)
                {
                    ModelState.AddModelError("", "Enter correct number of schedule");
                    obj.BANK_LOAN_SCHEDULE = new List<BANK_LOAN_SCHEDULE>();
                }
                else
                {
                    DateTime date = obj.BANK_LOAN_MASTER.START_DATE;
                    decimal eachValue = obj.BANK_LOAN_MASTER.LOAN_AMOUNT / obj.BANK_LOAN_MASTER.NO_OF_SCHEDULE;
                    decimal eachIntrValue = (obj.BANK_LOAN_MASTER.INTEREST_RATE / 100) * eachValue;

                    List<BANK_LOAN_SCHEDULE> objList = new List<BANK_LOAN_SCHEDULE>();
                    for (int i = 1; i <= obj.BANK_LOAN_MASTER.NO_OF_SCHEDULE; i++)
                    {
                        BANK_LOAN_SCHEDULE bls = new BANK_LOAN_SCHEDULE();
                        bls.SCHEDULE_NO = i;
                        bls.LOAN_AMOUNT = Math.Round(eachValue, 6);
                        bls.INTEREST_AMOUNT = Math.Round(eachIntrValue, 6);
                        bls.TOTAL_AMOUNT = Math.Round(eachValue + eachIntrValue, 6);
                        bls.DUE_DATE = date.AddMonths(i);
                        objList.Add(bls);
                    }
                    obj.BANK_LOAN_SCHEDULE = objList;
                    obj.BANK_LOAN_MASTER.END_DATE = date.AddMonths(obj.BANK_LOAN_MASTER.NO_OF_SCHEDULE);
                }
                return View(ViewPathFinder.ViewName(this.GetType(), "AddUpdate"), obj);
            }
            else if (buttonClicked == "Save")
            {
                if (obj.BANK_LOAN_SCHEDULE == null || obj.BANK_LOAN_SCHEDULE.Count == 0)
                {
                    ModelState.Clear();
                    ModelState.AddModelError("", "Apply loan calculation");
                    obj.BANK_LOAN_SCHEDULE = new List<BANK_LOAN_SCHEDULE>();
                }
                else if (ModelState.IsValid)
                {
                    eQResult = loanS.Insert(obj, user_session.USER_ID);
                    TempData["msg"] = eQResult.messages;

                    if (eQResult.success && eQResult.rows > 0)
                    {
                        return RedirectToAction(nameof(Edit), new { id = obj.BANK_LOAN_MASTER.ID });
                    }
                }
                else
                {
                    var errors = ValidateModelData.GET_MODEL_ERRORS(ModelState);
                    ModelState.AddModelError("", errors);
                }
                return View(ViewPathFinder.ViewName(this.GetType(), "AddUpdate"), obj);
            }
            else
            {
                return View(ViewPathFinder.ViewName(this.GetType(), "AddUpdate"), obj);
            }
        }
        public IActionResult Edit(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var entity = loanS.GetById(id);
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
            ViewBag.BRANCH_COST_CENTER_ID = new SelectList(branchCostCenterS.GetCashNonCash(), "ID", "COST_CENTER_NAME");
        }

        public IActionResult Delete(string id)
        {
            EQResult eQResult = loanS.Delete(id, user_session.USER_ID);
            return Json(eQResult);
        }


        //Payment
        public IActionResult Payment(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var entity = loanS.GetByScheduleId(id);
                if (entity != null)
                {
                    return View(ViewPathFinder.ViewName(this.GetType(), "Payment"), entity);
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
        public IActionResult Payment(BANK_LOAN_SCHEDULE obj)
        {
            EQResult eQResult = new EQResult();
            //    if (ModelState.IsValid)
            //    {
            eQResult = loanS.InsertPayment(obj, user_session.USER_ID);
            TempData["msg"] = eQResult.messages;
            if (eQResult.success && eQResult.rows > 0)
            {
                return RedirectToAction(nameof(Edit), new { id = obj.BANK_LOAN_MASTER_ID });
            }
            //    }
            //    else
            //    {
            //var errors = ValidateModelData.GET_MODEL_ERRORS(ModelState);
            ModelState.AddModelError("", eQResult.messages);
            //}
            return View(ViewPathFinder.ViewName(this.GetType(), "Payment"), obj);
        }
    }
}
