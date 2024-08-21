namespace BS.DMO.Models.Application
{
    public class MENU_ROLE : BaseModel
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Menu Id")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        [Required(ErrorMessage = "{0} is required")]
        public string? MENU_ID { get; set; }


        [Key, Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Role Id")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        [Required(ErrorMessage = "{0} is required")]
        public string? ROLE_ID { get; set; }


        [Display(Name = "View")]
        public bool IS_SELECT { get; set; }

        [Display(Name = "Add")]
        public bool IS_INSERT { get; set; }

        [Display(Name = "Edit")]
        public bool IS_UPDATE { get; set; }

        [Display(Name = "Delete")]
        public bool IS_DELETE { get; set; }
    }
}
