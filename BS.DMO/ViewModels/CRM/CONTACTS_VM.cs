namespace BS.DMO.ViewModels.CRM
{
    public class CONTACTS_VM : CONTACTS
    {
        //[NotMapped]

        [Display(Name = "Category")]
        public string? CATEGORY_NAME { get; set; }

        [Display(Name = "Has Address")]
        public int? HAS_ADDRESS { get; set; }
    }
}
