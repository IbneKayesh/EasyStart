namespace BS.DMO.Models.Accounts.BankLoan
{
    public class BANK_LOAN_FINES : BaseModel
    {
        public BANK_LOAN_FINES()
        {
            ID = Guid.Empty.ToString();
            FINE_DATE = DateTime.Now;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "ID")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 1)]
        public string ID { get; set; }

        [Display(Name = "Schedule Id")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? BANK_LOAN_SCHEDULE_ID { get; set; }

        [Display(Name = "Payment Info")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? PAYMENT_INFO { get; set; }

        [Display(Name = "Fine Amount")]
        [Required(ErrorMessage = "{0} is required")]
        [Column(TypeName = "decimal(18, 6)")]
        public decimal FINE_AMOUNT { get; set; } = 0;

        [Display(Name = "Fine Date")]
        [Required(ErrorMessage = "{0} is required")]
        public DateTime FINE_DATE { get; set; }
    }
}
