

namespace BS.DMO.ViewModels.Company
{
    public class BRANCH_VM : BRANCH
    {
        //[NotMapped]
        [Display(Name = "Branch Type Name")]
        public string? BRANCH_TYPE_NAME { get; set; }


        [Display(Name = "Business Name")]
        public string? BUSINESS_NAME { get; set; }
    }
}
