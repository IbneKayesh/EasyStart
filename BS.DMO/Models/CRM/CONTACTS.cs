namespace BS.DMO.Models.CRM
{
    public class CONTACTS
    {
        public CONTACTS()
        {
            ID = Guid.Empty.ToString();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "ID")]
        public string ID { get; set; }

        [Display(Name = "Name")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 3)]
        [Required(ErrorMessage = "{0} is required")]
        public string? CONTACT_NAME { get; set; }

        [Display(Name = "Contact")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 11)]
        [Required(ErrorMessage = "{0} is required")]
        public string? CONTACT_NO { get; set; }

        [Display(Name = "Email")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        [DataType(DataType.EmailAddress)]
        public string? EMAIL_ADDRESS { get; set; }

        [Display(Name = "Address")]
        [StringLength(250, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? OFFICE_ADDRESS { get; set; }

        [Display(Name = "Factory Address")]
        [StringLength(250, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? FACTORY_ADDRESS { get; set; }

        [Display(Name = "Balance Amount")]
        [Column(TypeName = "decimal(18, 6)")]
        public decimal BALANCE_AMOUNT { get; set; } = 0;
    }
}
