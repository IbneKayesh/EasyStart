using BS.DMO.Models.Accounts.BankLoan;

namespace BS.DMO.ViewModels.Accounts.BankLoan
{
    public class NEW_BANK_LOAN_MASTER_VM
    {
        public BANK_LOAN_MASTER BANK_LOAN_MASTER { get; set; }

        public List<BANK_LOAN_SCHEDULE>? BANK_LOAN_SCHEDULE { get; set; }
    }
}
