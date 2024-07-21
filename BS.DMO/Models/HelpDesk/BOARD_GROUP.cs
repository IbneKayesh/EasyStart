namespace BS.DMO.Models.HelpDesk
{
    public class BOARD_GROUP
    {
        public string? ID { get; set; }
        public string? GROUP_NAME { get; set; }
        public string? BS_COLOR { get; set; }
        public int ORDER_BY { get; set; }
        public int LIMIT_ROWS { get; set; }

        [NotMapped]
        public string? BOARD_NAME { get; set; }

    }
}
