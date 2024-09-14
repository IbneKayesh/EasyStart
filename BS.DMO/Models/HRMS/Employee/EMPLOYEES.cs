using BS.DMO.ViewModels.Setup;

namespace BS.DMO.Models.HRMS.Employee
{
    public class EMPLOYEES : BaseModel
    {
        public EMPLOYEES()
        {
            ID = Guid.Empty.ToString();
            BIRTH_DATE = DateTime.Now;
            JOIN_DATE = DateTime.Now;
            CONFIRM_DATE = DateTime.Now.AddMonths(6);

            //not mapped
            EMP_ADDRESS = new List<EMP_ADDRESS>();
            EMP_EXPERIENCE = new List<EMP_EXPERIENCE>();
            EMP_EDU = new List<EMP_EDU>();
            EMP_DESIG_VM = new List<EMP_DESIG_VM>();
            EMP_SALARY_CYCLES_VM = new List<EMP_SALARY_CYCLES_VM>();
            EMP_LEAVE_BALANCE_VM = new List<EMP_LEAVE_BALANCE_VM>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "ID")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 1)]
        public string ID { get; set; }

        [Display(Name = "Employee No")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 3)]
        [Required(ErrorMessage = "{0} is required")]
        public string? EMP_NO { get; set; }

        [Display(Name = "Employee Full Name")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 3)]
        [Required(ErrorMessage = "{0} is required")]
        public string? EMP_NAME { get; set; }

        [Display(Name = "Employee Photo")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? EMP_PHOTO { get; set; }

        [Display(Name = "Card 1")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? CARD_NO1 { get; set; }

        [Display(Name = "Card 2")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? CARD_NO2 { get; set; }

        [Display(Name = "Contact No")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? CONTACT_NO { get; set; }

        [Display(Name = "Email")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? EMAIL_ADDRESS { get; set; }

        [Display(Name = "Birthdate")]
        [Required(ErrorMessage = "{0} is required")]
        public DateTime BIRTH_DATE { get; set; }

        [Display(Name = "Gender")]
        [StringLength(15, ErrorMessage = "{0} length is between {2} and {1}", MinimumLength = 0)]
        [Required(ErrorMessage = "{0} is required")]
        public string? GENDER_ID { get; set; }

        [Display(Name = "Marital Status")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(15, ErrorMessage = "{0} length is between {2} and {1}", MinimumLength = 0)]
        public string? MARITAL_STATUS { get; set; }

        [Display(Name = "Spouse Name")]
        [StringLength(100, ErrorMessage = "{0} length is between {2} and {1}", MinimumLength = 3)]
        public string? SPOUSE_NAME { get; set; }

        [Display(Name = "Blood Group")]
        [StringLength(100, ErrorMessage = "{0} length is between {2} and {1}", MinimumLength = 0)]
        public string? BLOOD_GROUP { get; set; }

        [Display(Name = "National ID")]
        [StringLength(100, ErrorMessage = "{0} length is between {2} and {1}", MinimumLength = 0)]
        public string? NATIONAL_IDNO { get; set; }

        [Display(Name = "Passport Number")]
        [StringLength(100, ErrorMessage = "{0} length is between {2} and {1}", MinimumLength = 0)]
        public string? PASSPORT_NO { get; set; }

        [Display(Name = "TIN")]
        [StringLength(100, ErrorMessage = "{0} length is between {2} and {1}", MinimumLength = 0)]
        public string? TIN_NO { get; set; }

        [Display(Name = "Nationality")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? NATIONALITY { get; set; }

        [Display(Name = "Father Name")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(50, ErrorMessage = "{0} length is between {2} and {1}", MinimumLength = 3)]
        public string? FATHER_NAME { get; set; }

        [Display(Name = "Mother Name")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(50, ErrorMessage = "{0} length is between {2} and {1}", MinimumLength = 3)]
        public string? MOTHER_NAME { get; set; }

        [Display(Name = "Parents Contact")]
        [StringLength(30, ErrorMessage = "{0} length is between {2} and {1}", MinimumLength = 0)]
        public string? PARENTS_CONACT { get; set; }

        [Display(Name = "Reference Person")]
        [StringLength(30, ErrorMessage = "{0} length is between {2} and {1}", MinimumLength = 0)]
        public string? REFERENCE_PERSON { get; set; }



        //office information
        [Display(Name = "Join Date")]
        [Required(ErrorMessage = "{0} is required")]
        public DateTime JOIN_DATE { get; set; }

        [Display(Name = "Confirm Date")]
        [Required(ErrorMessage = "{0} is required")]
        public DateTime CONFIRM_DATE { get; set; }


        //non mapped
        [NotMapped]
        public List<EMP_ADDRESS>? EMP_ADDRESS { get; set; }

        [NotMapped]
        public List<EMP_EXPERIENCE>? EMP_EXPERIENCE { get; set; }

        [NotMapped]
        public List<EMP_EDU>? EMP_EDU { get; set; }

        [NotMapped]
        public List<EMP_DESIG_VM>? EMP_DESIG_VM { get; set; }

        [NotMapped]
        public List<EMP_SALARY_CYCLES_VM>? EMP_SALARY_CYCLES_VM { get; set; }

        [NotMapped]
        public List<EMP_LEAVE_BALANCE_VM>? EMP_LEAVE_BALANCE_VM { get; set; }
    }
}
