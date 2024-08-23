namespace BS.DMO.Models.Accounts.BankLoan
{
    public class BANK_LOAN_MASTER : BaseModel
    {
        public BANK_LOAN_MASTER()
        {
            ID = Guid.Empty.ToString();
            START_DATE = DateTime.Now;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "ID")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 1)]
        public string ID { get; set; }

        [Display(Name = "Branch Type Name")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        [Required(ErrorMessage = "{0} is required")]
        public string? BRANCH_COST_CENTER_ID { get; set; }


        [Display(Name = "Trn No")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        [Required(ErrorMessage = "{0} is required")]
        public string? TRN_NO { get; set; }

        [Display(Name = "Ref Trn Id")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? REF_TRN_ID { get; set; } = "";

        [Display(Name = "Start Date")]
        [Required(ErrorMessage = "{0} is required")]
        public DateTime START_DATE { get; set; }

        [Display(Name = "End Date")]
        [Required(ErrorMessage = "{0} is required")]
        public DateTime END_DATE { get; set; }

        [Display(Name = "Note")]
        [StringLength(250, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? TRN_NOTE { get; set; }

        [Display(Name = "Loan Amount")]
        [Required(ErrorMessage = "{0} is required")]
        [Column(TypeName = "decimal(18, 6)")]
        public decimal LOAN_AMOUNT { get; set; } = 0;

        [Display(Name = "Interest Rate")]
        [Required(ErrorMessage = "{0} is required")]
        [Column(TypeName = "decimal(18, 6)")]
        public decimal INTEREST_RATE { get; set; } = 0;

        [Display(Name = "Total Amount")]
        [Required(ErrorMessage = "{0} is required")]
        [Column(TypeName = "decimal(18, 6)")]
        public decimal TOTAL_AMOUNT { get; set; } = 0;

        [Display(Name = "No of Schedules")]
        [Required(ErrorMessage = "{0} is required")]
        public int NO_OF_SCHEDULE { get; set; }

        [Display(Name = "Due Amount")]
        [Required(ErrorMessage = "{0} is required")]
        [Column(TypeName = "decimal(18, 6)")]
        public decimal DUE_AMOUNT { get; set; } = 0;


        [NotMapped]
        public List<BANK_LOAN_SCHEDULE>? BANK_LOAN_SCHEDULE = new List<BANK_LOAN_SCHEDULE>();
    }
}
