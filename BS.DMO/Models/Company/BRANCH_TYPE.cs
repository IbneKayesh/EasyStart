namespace BS.DMO.Models.Company
{
    public class BRANCH_TYPE : BaseModel
    {
        public BRANCH_TYPE()
        {
            ID = Guid.Empty.ToString();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "ID")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 1)]
        public string ID { get; set; }

        [Display(Name = "Branch Type Name")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 3)]
        [Required(ErrorMessage = "{0} is required")]
        public string? BRANCH_TYPE_NAME { get; set; }

        [Display(Name = "Short Name")]
        [StringLength(10, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 2)]
        [Required(ErrorMessage = "{0} is required")]
        public string? SHORT_NAME { get; set; }
    }
}