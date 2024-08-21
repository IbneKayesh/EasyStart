namespace BS.DMO.ViewModels.Setup
{
    public class BANK_BRANCH_VM : BANK_BRANCH
    {
        //[NotMapped]
        [Display(Name = "Bank Name")]
        public string? BANK_NAME { get; set; }
    }
}
