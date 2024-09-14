namespace BS.DMO.Models.Setup
{
    public class HOLIDAY_TYPE : BaseModel
    {
        public HOLIDAY_TYPE()
        {
            ID = Guid.Empty.ToString();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "ID")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 1)]
        public string ID { get; set; }


        [Display(Name = "Holiday Type Name")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 3)]
        [Required(ErrorMessage = "{0} is required")]
        public string? HOLIDAY_TYPE_NAME { get; set; }

        [Display(Name = "Application Required")]
        [Required(ErrorMessage = "{0} is required")]
        public bool IS_APPLICATION_REQUIRED { get; set; } = false;
    }
}
