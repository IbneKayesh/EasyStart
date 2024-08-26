using BS.DMO.Models.Accounts.BankLoan;

namespace BS.DMO.ViewModels.Accounts.BankLoan
{
    public class BANK_LOAN_MASTER_VM : BANK_LOAN_MASTER
    {
        //[NotMapped]
        [Display(Name = "Cost Center Name")]
        public string? COST_CENTER_NAME { get; set; }
    }
}
