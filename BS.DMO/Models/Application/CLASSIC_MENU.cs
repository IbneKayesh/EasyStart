namespace BS.DMO.Models.Application
{
    public class CLASSIC_MENU : BaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        [Display(Name = "ID")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 1)]
        public string? MENU_ID { get; set; }


        [Display(Name = "Menu Name")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 3)]
        public string? MENU_NAME { get; set; }


        [Display(Name = "Area")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 3)]
        public string? MENU_AREA { get; set; }


        [Display(Name = "Controller")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 3)]
        public string? MENU_CONTROLLER { get; set; }

        
        [Display(Name = "Action")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 3)]
        public string? MENU_ACTION { get; set; }

        
        [Display(Name = "Icon")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 3)]
        public string? MENU_ICON { get; set; }

        [Display(Name = "Note")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? MENU_NOTE { get; set; }

        [Display(Name = "Color")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? MENU_COLOR { get; set; }

        [Display(Name = "Page ID")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? PAGE_ID { get; set; }



        [Display(Name = "Parent Node ID")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 1)]
        public string? PARENT_NODE { get; set; } = "#";

        [Display(Name = "Child Node ID")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 1)]
        public string? CHILD_NODE { get; set; } = "#";
    }
}
