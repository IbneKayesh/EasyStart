namespace BS.DMO.ViewModels.Setup
{
    public class EMP_LEAVE_BALANCE_VM : EMP_LEAVE_BALANCE
    {
        //[NotMapped]

        [Display(Name = "Year Name")]
        public string? YEAR_NAME { get; set; }


        [Display(Name = "Holiday Type Name")]
        public string? HOLIDAY_TYPE_NAME { get; set; }
    }
}
