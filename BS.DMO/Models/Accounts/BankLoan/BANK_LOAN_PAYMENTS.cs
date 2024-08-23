namespace BS.DMO.Models.Accounts.BankLoan
{
    public class BANK_LOAN_PAYMENTS : BaseModel
    {
        public BANK_LOAN_PAYMENTS()
        {
            ID = Guid.Empty.ToString();
            PAY_DATE = DateTime.Now;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "ID")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 1)]
        public string ID { get; set; }

        [Display(Name = "Trn Id")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? MASTER_ID { get; set; }

        [Display(Name = "Payment Info")]
        [Required(ErrorMessage = "{0} is required")]
        public string? PAYMENT_INFO { get; set; }

        [Display(Name = "Payment Amount")]
        [Required(ErrorMessage = "{0} is required")]
        [Column(TypeName = "decimal(18, 6)")]
        public decimal PAY_AMOUNT { get; set; } = 0;

        [Display(Name = "Payment Date")]
        [Required(ErrorMessage = "{0} is required")]
        public DateTime PAY_DATE { get; set; }


        [NotMapped]
        [Display(Name = "Is Fine")]
        public bool IS_FINE { get; set; }
    }
}
