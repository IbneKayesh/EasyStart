namespace BS.DMO.Models.Setup
{
    public class SUB_SECTIONS_TRN_ID : AuditTable
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Sub Section Id")]
        [Required(ErrorMessage = "{0} is required")]
        public string? SUB_SECTION_ID { get; set; }


        [Key, Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Transaction Id")]
        [Required(ErrorMessage = "{0} is required")]
        public string? TRN_ID { get; set; }



        //[NotMapped]
        //[Display(Name = "Section")]
        //public string? SECTION_NAME { get; set; }
    }
}
