namespace BS.DMO.Models
{
    public class PostingModel : BaseModel
    {
        //Posting
        [Display(Name = "Is Posted")]
        public bool IS_POSTED { get; set; }

        [Display(Name = "Posted User Id")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? POSTED_USER_ID { get; set; }

        [Display(Name = "Posted Date")]
        public DateTime? POSTED_DATE { get; set; }

        [Display(Name = "Posted Note")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? POSTED_NOTE { get; set; }

        //Approve
        [Display(Name = "Is Approve")]
        public bool IS_APPROVE { get; set; }

        [Display(Name = "Approve User Id")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? APPROVE_USER_ID { get; set; }

        [Display(Name = "Approve Date")]
        public DateTime? APPROVE_DATE { get; set; }

        [Display(Name = "Approve Note")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? APPROVE_NOTE { get; set; }
    }
}
