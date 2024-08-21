namespace BS.DMO.Models.HelpDesk
{
    public class BOARDS : BaseModel
    {
        public BOARDS()
        {
            ID = Guid.Empty.ToString();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "ID")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 1)]
        public string ID { get; set; }

        [Display(Name = "Project")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        //[Required(ErrorMessage = "{0} is required")]
        public string? PROJECT_ID { get; set; }


        [Display(Name = "Board Name")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 3)]
        [Required(ErrorMessage = "{0} is required")]
        public string? BOARD_NAME { get; set; }
    }
}
