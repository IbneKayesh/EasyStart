namespace BS.DMO.Models.CRM
{
    public class CONTACT_ADDRESS : BaseModel
    {
        public CONTACT_ADDRESS()
        {
            ID = Guid.Empty.ToString();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "ID")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 1)]
        public string ID { get; set; }

        [Display(Name = "Contact Id")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 1)]
        public string? CONTACT_ID { get; set; }



        [Display(Name = "Contact Person")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 3)]
        [Required(ErrorMessage = "{0} is required")]
        public string? CONTACT_PERSON { get; set; }

        [Display(Name = "Contact No")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 1)]
        [Required(ErrorMessage = "{0} is required")]
        public string? CONTACT_NO { get; set; }

        [Display(Name = "Email")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        [DataType(DataType.EmailAddress)]
        public string? EMAIL_ADDRESS { get; set; }

        [Display(Name = "Office Address")]
        [StringLength(250, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 2)]
        public string? OFFICE_ADDRESS { get; set; }

        [Display(Name = "Is Default")]
        public bool IS_DEFAULT { get; set; }
    }
}