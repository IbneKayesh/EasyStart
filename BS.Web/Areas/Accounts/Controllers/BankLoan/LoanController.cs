using BS.DMO.Models.Accounts.BankLoan;
using BS.Infra.Services.Company;
using Microsoft.VisualBasic;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BS.Web.Areas.Accounts.Controllers.BankLoan
{
    [Area("Accounts")]
    public class LoanController : BaseController
    {
        private readonly LoanService loanS;
        private readonly BranchCostCenterService branchCostCenterS;

        private readonly EmployeesService employeesS;
        private readonly DesignationService designationS;
        public LoanController(LoanService _loanService, BranchCostCenterService _branchCostCenterService, EmployeesService _employeesService, DesignationService _designationService)
        {
            loanS = _loanService;
            branchCostCenterS = _branchCostCenterService;
            employeesS = _employeesService;
            designationS = _designationService;
        }
        public IActionResult Index()
        {
            var entityList = employeesS.GetAll();
            return View(ViewPathFinder.ViewName(this.GetType(), "Index"), entityList);
        }
        public IActionResult Create()
        {
            Dropdown_CreateEdit();
            return View(ViewPathFinder.ViewName(this.GetType(), "AddUpdate"), new BANK_LOAN_MASTER());
        }
        [HttpPost]
        public IActionResult AddUpdate(BANK_LOAN_MASTER obj)
        {
            Dropdown_CreateEdit();
            EQResult eQResult = new EQResult();
            var buttonClicked = Request.Form["submitButton"];
            if (buttonClicked == "Apply")
            {
                ModelState.Clear();
                decimal TotalAmount = (obj.INTEREST_RATE / 100) * obj.LOAN_AMOUNT;
                obj.TOTAL_AMOUNT = obj.LOAN_AMOUNT + TotalAmount;
                if (obj.LOAN_AMOUNT < 1)
                {
                    ModelState.AddModelError("", "Enter correct loan amount");
                }
                if (obj.NO_OF_SCHEDULE < 1)
                {
                    ModelState.AddModelError("", "Enter correct number of schedule");
                }
                else
                {
                    DateTime date = obj.START_DATE;
                    decimal eachValue = obj.LOAN_AMOUNT / obj.NO_OF_SCHEDULE;
                    decimal eachIntrValue = (obj.INTEREST_RATE / 100) * eachValue;

                    List<BANK_LOAN_SCHEDULE> objList = new List<BANK_LOAN_SCHEDULE>();
                    for (int i = 1; i <= obj.NO_OF_SCHEDULE; i++)
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
                }
                return View(ViewPathFinder.ViewName(this.GetType(), "AddUpdate"), obj);
            }
            else if (buttonClicked == "Save")
            {
                if (ModelState.IsValid)
                {
                    eQResult = loanS.Insert(obj, user_session.USER_ID);
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
            ViewBag.BRANCH_COST_CENTER_ID = new SelectList(branchCostCenterS.GetAllActive(), "ID", "COST_CENTER_NAME");
        }

        public IActionResult Delete(string id)
        {
            EQResult eQResult = employeesS.Delete(id, user_session.USER_ID);
            return Json(eQResult);
        }
    }
}
