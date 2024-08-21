namespace BS.DMO.Models.Setup
{
    public class BANK_BRANCH : BaseModel
    {
        public BANK_BRANCH()
        {
            ID = Guid.Empty.ToString();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "ID")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 1)]
        public string ID { get; set; }

        [Display(Name = "Bank Id")]
        [StringLength(50, ErrorMessage = "{0} max length is 50")]
        [Required(ErrorMessage = "{0} is required")]
        public string? BANK_ID { get; set; }



        [Display(Name = "Branch Name")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 2)]
        [Required(ErrorMessage = "{0} is required")]
        public string? BRANCH_NAME { get; set; }

        [Display(Name = "Branch Short Name")]
        [StringLength(50, ErrorMessage = "{0} max length is 50")]
        public string? SHORT_NAME { get; set; }

        [Display(Name = "Branch Address")]
        [StringLength(150, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 2)]
        [Required(ErrorMessage = "{0} is required")]
        public string? BRANCH_ADDRESS { get; set; }


        [Display(Name = "Route No")]
        [StringLength(50, ErrorMessage = "{0} max length is 50")]
        public string? ROUTE_NO { get; set; }


        [Display(Name = "Swift Code")]
        [StringLength(50, ErrorMessage = "{0} max length is 50")]
        public string? SWIFT_CODE { get; set; }

    }
}
