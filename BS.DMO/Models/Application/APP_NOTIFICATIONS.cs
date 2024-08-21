namespace BS.DMO.Models.Application
{
    public class APP_NOTIFICATIONS : BaseModel
    {
        public APP_NOTIFICATIONS()
        {
            ID = Guid.NewGuid().ToString();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "ID")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 1)]
        public string ID { get; set; }

        [Display(Name = "Title")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? TITLE_TEXT { get; set; }


        [Display(Name = "Body")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? BODY_TEXT { get; set; }


        [Display(Name = "Url")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? NAV_URL { get; set; }


        [Display(Name = "To User")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? TO_USER { get; set; }

        [Display(Name = "To User Group")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? TO_USER_GROUP { get; set; }


        [Display(Name = "From User")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? FROM_USER { get; set; }


        [Display(Name = "Priority Level")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? PRIORITY_LEVEL { get; set; }


        [Display(Name = "Read")]
        public bool? IS_READ { get; set; }


        [Display(Name = "Read Time")]
        public DateTime? READ_TIME { get; set; }

    }
}
