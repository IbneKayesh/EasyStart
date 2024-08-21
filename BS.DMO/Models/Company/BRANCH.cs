namespace BS.DMO.Models.Company
{
    public class BRANCH : BaseModel
    {
        public BRANCH()
        {
            ID = Guid.Empty.ToString();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "ID")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 1)]
        public string ID { get; set; }

        [Display(Name = "Branch Type Name")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        [Required(ErrorMessage = "{0} is required")]
        public string? BRANCH_TYPE_ID { get; set; }

        [Display(Name = "Business Name")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        [Required(ErrorMessage = "{0} is required")]
        public string? BUSINESS_ID { get; set; }


        [Display(Name = "Branch Name")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 3)]
        [Required(ErrorMessage = "{0} is required")]
        public string? BRANCH_NAME { get; set; }

        [Display(Name = "Short Name")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 2)]
        [Required(ErrorMessage = "{0} is required")]
        public string? SHORT_NAME { get; set; }

        [Display(Name = "Address")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 3)]
        [Required(ErrorMessage = "{0} is required")]
        public string? ADDRESS_INFO { get; set; }

        [Display(Name = "Contact Name")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 3)]
        [Required(ErrorMessage = "{0} is required")]
        public string? CONTACT_NAME { get; set; }

        [Display(Name = "Contact No")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 3)]
        [Required(ErrorMessage = "{0} is required")]
        public string? CONTACT_NO { get; set; }

        [Display(Name = "Email Address")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        [DataType(DataType.EmailAddress, ErrorMessage = "{0} is not valid email")]
        public string? EMAIL_ADDRESS { get; set; }

        [Display(Name = "Max Employees")]
        [Required(ErrorMessage = "{0} is required")]
        public int MAX_EMPLOYEE { get; set; }

        [Display(Name = "Max Total Salary")]
        [Required(ErrorMessage = "{0} is required")]
        public int MAX_SALARY { get; set; }
    }
}
