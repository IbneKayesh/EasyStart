﻿namespace BS.DMO.Models.Accounts.BankLoan
{
    public class BANK_LOAN_SCHEDULE : BaseModel
    {
        public BANK_LOAN_SCHEDULE()
        {
            ID = Guid.Empty.ToString();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "ID")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 1)]
        public string ID { get; set; }

        [Display(Name = "Loan ID")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? BANK_LOAN_MASTER_ID { get; set; }

        [Display(Name = "Schedule No")]
        [Required(ErrorMessage = "{0} is required")]
        public int SCHEDULE_NO { get; set; }



        [Display(Name = "Loan Amount")]
        [Required(ErrorMessage = "{0} is required")]
        [Column(TypeName = "decimal(18, 6)")]
        public decimal LOAN_AMOUNT { get; set; } = 0;

        [Display(Name = "Interest Amount")]
        [Required(ErrorMessage = "{0} is required")]
        [Column(TypeName = "decimal(18, 6)")]
        public decimal INTEREST_AMOUNT{ get; set; } = 0;

        [Display(Name = "Total Amount")]
        [Required(ErrorMessage = "{0} is required")]
        [Column(TypeName = "decimal(18, 6)")]
        public decimal TOTAL_AMOUNT { get; set; } = 0;

        [Display(Name = "Due Date")]
        [Required(ErrorMessage = "{0} is required")]
        public DateTime DUE_DATE { get; set; }
    }
}
