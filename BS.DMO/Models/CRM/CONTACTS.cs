namespace BS.DMO.Models.CRM
{
    public class CONTACTS : BaseModel
    {
        //lat long
        public CONTACTS()
        {
            ID = Guid.Empty.ToString();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "ID")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 1)]
        public string ID { get; set; }

        [Display(Name = "Contact Code")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? CONTACT_CODE { get; set; }

        [Display(Name = "Contact Category Id")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        [Required(ErrorMessage = "{0} is required")]
        public string? CONTACT_CATEGORY_ID { get; set; }

        [Display(Name = "Contact Group Name")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        [Required(ErrorMessage = "{0} is required")]
        public string? CONTACT_GROUP { get; set; }

        [Display(Name = "Name")]
        [StringLength(100, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 3)]
        [Required(ErrorMessage = "{0} is required")]
        public string? CONTACT_NAME { get; set; }

        [Display(Name = "Contact Person")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 3)]
        [Required(ErrorMessage = "{0} is required")]
        public string? CONTACT_PERSON { get; set; }

        [Display(Name = "Contact No")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 3)]
        [Required(ErrorMessage = "{0} is required")]
        public string? CONTACT_NO { get; set; }

        [Display(Name = "Email")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        [DataType(DataType.EmailAddress)]
        public string? EMAIL_ADDRESS { get; set; }

        [Display(Name = "Office Address")]
        [StringLength(250, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? OFFICE_ADDRESS { get; set; }

        [Display(Name = "Factory Address")]
        [StringLength(250, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? FACTORY_ADDRESS { get; set; }

        [Display(Name = "Balance Amount")]
        [Column(TypeName = "decimal(18, 6)")]
        public decimal BALANCE_AMOUNT { get; set; } = 0;


        //Join
        [Display(Name = "Is Customer")]
        public bool IS_CUSTOMER { get; set; }

        [Display(Name = "Customer User Id")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? CUSTOMER_USER_ID { get; set; }

        [Display(Name = "Customer Date")]
        public DateTime? CUSTOMER_DATE { get; set; }


        [Display(Name = "Is Seller")]
        public bool IS_SELLER { get; set; }

        [Display(Name = "Seller User Id")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? SELLER_USER_ID { get; set; }

        [Display(Name = "Seller Date")]
        public DateTime? SELLER_DATE { get; set; }
    }
}
