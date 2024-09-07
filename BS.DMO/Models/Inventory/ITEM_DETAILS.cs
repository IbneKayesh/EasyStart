namespace BS.DMO.Models.Inventory
{
    public class ITEM_DETAILS
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "ID")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 1)]
        public string? ID { get; set; }

        [Display(Name = "Item Master Id")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        [Required(ErrorMessage = "{0} is required")]
        public string? ITEM_MASTER_ID { get; set; }

        [Display(Name = "Code")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? ITEM_CODE { get; set; }

        [Display(Name = "Barcode")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? BAR_CODE { get; set; }

        [Display(Name = "Description")]
        [StringLength(300, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? ITEM_DESC { get; set; }

        // Attribute 15
        [Display(Name = "Attrbute 1")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? ATTRIBUTE_VALUE1 { get; set; }

        [Display(Name = "Attrbute 2")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? ATTRIBUTE_VALUE2 { get; set; }

        [Display(Name = "Attrbute 3")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? ATTRIBUTE_VALUE3 { get; set; }

        [Display(Name = "Attrbute 4")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? ATTRIBUTE_VALUE4 { get; set; }

        [Display(Name = "Attrbute 5")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? ATTRIBUTE_VALUE5 { get; set; }

        [Display(Name = "Attrbute 6")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? ATTRIBUTE_VALUE6 { get; set; }

        [Display(Name = "Attrbute 7")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? ATTRIBUTE_VALUE7 { get; set; }

        [Display(Name = "Attrbute 8")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? ATTRIBUTE_VALUE8 { get; set; }

        [Display(Name = "Attrbute 9")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? ATTRIBUTE_VALUE9 { get; set; }

        [Display(Name = "Attrbute 10")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? ATTRIBUTE_VALUE10 { get; set; }

        [Display(Name = "Attrbute 11")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? ATTRIBUTE_VALUE11 { get; set; }

        [Display(Name = "Attrbute 12")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? ATTRIBUTE_VALUE12 { get; set; }

        [Display(Name = "Attrbute 13")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? ATTRIBUTE_VALUE13 { get; set; }

        [Display(Name = "Attrbute 14")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? ATTRIBUTE_VALUE14 { get; set; }

        [Display(Name = "Attrbute 15")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? ATTRIBUTE_VALUE15 { get; set; }
    }
}
