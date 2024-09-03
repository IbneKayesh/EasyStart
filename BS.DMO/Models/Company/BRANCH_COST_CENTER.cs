namespace BS.DMO.Models.Company
{
    public class BRANCH_COST_CENTER : BaseModel
    {
        public BRANCH_COST_CENTER()
        {
            ID = Guid.Empty.ToString();
            OPEN_DATE =  DateTime.Now;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "ID")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 1)]
        public string ID { get; set; }

        [Display(Name = "Branch Name")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        [Required(ErrorMessage = "{0} is required")]
        public string? BRANCH_ID { get; set; }

        [Display(Name = "Bank Branch")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        [Required(ErrorMessage = "{0} is required")]
        public string? BANK_BRANCH_ID { get; set; }


        [Display(Name = "Cost Center Name")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 3)]
        [Required(ErrorMessage = "{0} is required")]
        public string? COST_CENTER_NAME { get; set; }


        [Display(Name = "Max Balanace Limit")]
        [Required(ErrorMessage = "{0} is required")]
        [Column(TypeName = "decimal(18, 4)")]
        [Range(minimum: 0.01d, double.MaxValue, ErrorMessage = "{0} length is {2} between {1}")]
        public decimal MAX_BALANCE_LIMIT { get; set; }


        [Display(Name = "Open Date")]
        [Required(ErrorMessage = "{0} is required")]
        public DateTime OPEN_DATE { get; set; }
    }
}
