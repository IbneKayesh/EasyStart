namespace BS.DMO.ViewModels.Setup
{
    public class LEAVE_CALENDAR_VM : LEAVE_CALENDAR
    {
        //[NotMapped]

        [Display(Name = "Year Name")]
        public string? YEAR_NAME { get; set; }


        [Display(Name = "Leave Type Name")]
        public string? LEAVE_TYPE_NAME { get; set; }
    }
}
