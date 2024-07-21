namespace BS.DMO.ViewModels.Company
{
    public class BRANCH_COST_CENTER_VM: BRANCH_COST_CENTER
    {
        //[NotMapped]

        [Display(Name = "Branch Name")]
        public string? BRANCH_NAME { get; set; }


        [Display(Name = "Bank Branch Name")]
        public string? BANK_BRANCH_NAME { get; set; }
    }
}
