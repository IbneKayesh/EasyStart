namespace BS.DMO.Models.Setup
{
    public class ENTITY_VALUE_TEXT: BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Value ID")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 2)]
        public string? VALUE_ID { get; set; }


        [Display(Name = "Value Name")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 2)]
        public string? VALUE_NAME { get; set; }


        [Display(Name = "Entity")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength =0)]
        [Column(TypeName = "nvarchar(50)")]
        public string? ENTITY_ID { get; set; }


        [Display(Name = "Entity Description")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? ENTITY_DESCRIPTION { get; set; }

        [Display(Name = "Is Default")]
        [Required(ErrorMessage = "{0} is required")]
        public bool IS_DEFAULT { get; set; }
    }
}
