using BS.DMO.StaticValues;

namespace BS.DMO.Models
{
    public class BaseModel
    {
        public BaseModel()
        {
            IS_ACTIVE = true;

            CREATE_USER = "BS1";
            CREATE_DATE = DateTime.Now;

            UPDATE_USER = "BS2";
            UPDATE_DATE = DateTime.Now;
            REVISE_NO = 0;
            //Data Edit Restrict for multiple Users

            DATE_DISPLAY_FORMAT = AppDateFormat.DATE_DISPLAY_FORMAT;
            ALLOW_ADD = true;
            ALLOW_EDIT = true;
            ALLOW_DELETE = true;
        }

        [Display(Name = "Status")]
        public bool IS_ACTIVE { get; set; }


        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string CREATE_USER { get; set; }
        public DateTime CREATE_DATE { get; set; }

        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string UPDATE_USER { get; set; }
        public DateTime UPDATE_DATE { get; set; }


        [Display(Name = "Revise No")]
        public int REVISE_NO { get; set; }

        [Timestamp]
        public byte[]? RowVersion { get; set; }

        [NotMapped]
        public string DATE_DISPLAY_FORMAT { get; set; }

        [NotMapped]
        public bool ALLOW_ADD { get; set; }

        [NotMapped]
        public bool ALLOW_EDIT { get; set; }

        [NotMapped]
        public bool ALLOW_DELETE { get; set; }
    }
}
