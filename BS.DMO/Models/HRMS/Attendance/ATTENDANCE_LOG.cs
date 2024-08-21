using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.DMO.Models.HRMS.Attendance
{
    public class ATTENDANCE_LOG : BaseModel
    {
        public ATTENDANCE_LOG()
        {
            ID = Guid.Empty.ToString();
            WORK_SHIFT_ID = Guid.Empty.ToString();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "ID")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 1)]
        public string ID { get; set; }

        [Display(Name = "Employee No")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        [Required(ErrorMessage = "{0} is required")]
        public string? EMP_ID { get; set; }

        [Display(Name = "Attendance Date")]
        public DateTime ATTEN_DATE { get; set; }


        [Display(Name = "Attendance Type")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        [Required(ErrorMessage = "{0} is required")]
        public string? ATTEN_TYPE_ID { get; set; }

        [Display(Name = "Working Shift")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? WORK_SHIFT_ID { get; set; }


        [Display(Name = "In Time")]
        public DateTime? IN_TIME { get; set; }

        [Display(Name = "Out Time")]
        public DateTime? OUT_TIME { get; set; }

        [Display(Name = "Total Time")]
        [Column(TypeName = "decimal(18, 4)")]
        [Range(minimum: 0, double.MaxValue, ErrorMessage = "{0} length is {2} between {1}")]
        public decimal? TOTAL_TIME { get; set; }

        [Display(Name = "Over Time")]
        [Column(TypeName = "decimal(18, 4)")]
        [Range(minimum: 0, double.MaxValue, ErrorMessage = "{0} length is {2} between {1}")]
        public decimal? OVER_TIME { get; set; }

        [Display(Name = "Net Time")]
        [Column(TypeName = "decimal(18, 4)")]
        [Range(minimum: 0, double.MaxValue, ErrorMessage = "{0} length is {2} between {1}")]
        public decimal? NET_TIME { get; set; }

        [Display(Name = "Note")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? ATTEN_NOTE { get; set; }
    }
}