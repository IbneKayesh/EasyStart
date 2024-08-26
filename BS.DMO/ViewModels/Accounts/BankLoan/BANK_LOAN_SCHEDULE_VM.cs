using BS.DMO.Models.Accounts.BankLoan;

namespace BS.DMO.ViewModels.Accounts.BankLoan
{
    public class BANK_LOAN_SCHEDULE_VM : BANK_LOAN_SCHEDULE
    {
        //[NotMapped]
        [Display(Name = "Is Paid")]
        public bool IS_PAID { get; set; }

        [Display(Name = "Is Fine")]
        public bool IS_FINE { get; set; }
    }
}
