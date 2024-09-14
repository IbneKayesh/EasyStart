namespace BS.DMO.ViewModels.Setup
{
    public class LEAVE_CALENDAR_VM : YEARLY_LEAVE_CALENDAR
    {
        //[NotMapped]

        [Display(Name = "Year Name")]
        public string? YEAR_NAME { get; set; }


        [Display(Name = "Holiday Type Name")]
        public string? HOLIDAY_TYPE_NAME { get; set; }
    }
}
