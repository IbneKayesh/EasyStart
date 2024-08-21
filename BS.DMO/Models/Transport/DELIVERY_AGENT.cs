namespace BS.DMO.Models.Transport
{
    public class DELIVERY_AGENT : BaseModel
    {
        public DELIVERY_AGENT()
        {
            ID = Guid.Empty.ToString();
            LOAD_CAPACITY = 0;
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "ID")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 1)]
        public string ID { get; set; }


        [Display(Name = "Agent Type")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        [Required(ErrorMessage = "{0} is required")]
        public string? DELIVERY_AGENT_TYPE_ID { get; set; }


        [Display(Name = "Agent No")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 3)]
        [Required(ErrorMessage = "{0} is required")]
        public string? AGENT_NO { get; set; }

        [Display(Name = "Agent Name")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 3)]
        [Required(ErrorMessage = "{0} is required")]
        public string? AGENT_NAME { get; set; }

        [Display(Name = "Contact No")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 11)]
        [Required(ErrorMessage = "{0} is required")]
        public string? AGENT_CONTACT { get; set; }

        [Display(Name = "Address")]
        [StringLength(250, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        [Required(ErrorMessage = "{0} is required")]
        public string? AGENT_ADDRESS { get; set; }

        [Display(Name = "Load Capacity")]
        [Required(ErrorMessage = "{0} is required")]
        public int LOAD_CAPACITY { get; set; }

        [Display(Name = "KPL")]
        [Column(TypeName = "decimal(18, 6)")]
        public decimal KPL { get; set; }
    }
}
