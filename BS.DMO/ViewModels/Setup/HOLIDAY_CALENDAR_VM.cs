namespace BS.DMO.ViewModels.Setup
{
    public class HOLIDAY_CALENDAR_VM : HOLIDAY_CALENDAR
    {
        //[NotMapped]

        [Display(Name = "Year Name")]
        public string? YEAR_NAME { get; set; }


        [Display(Name = "Holiday Type Name")]
        public string? HOLIDAY_TYPE_NAME { get; set; }
    }
}
