namespace BS.DMO.Models.SalesOrder
{
    public class SB_MASTER : BaseModel
    {
        //sales booking
        public SB_MASTER()
        {
            ID = Guid.Empty.ToString();
            TRN_DATE = DateTime.Now;
            LAST_SDD_DATE = DateTime.Now.AddDays(2);
            LAST_MFG_DATE = DateTime.Now.AddDays(5);
            LAST_DELIVERY_DATE = DateTime.Now.AddDays(7);
            IS_POSTED = false;
            TRN_VALID_DAYS = 90;
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "ID")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 1)]
        public string ID { get; set; }

        //Trn
        [Display(Name = "Trn Source Id")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        [Required(ErrorMessage = "{0} is required")]
        public string? TRN_SOURCE_ID { get; set; }

        [Display(Name = "Type Id")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        [Required(ErrorMessage = "{0} is required")]
        public string? TRN_TYPE_ID { get; set; }

        [Display(Name = "Trn Id")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        [Required(ErrorMessage = "{0} is required")]
        public string? TRN_ID { get; set; }

        [Display(Name = "Trn No")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        [Required(ErrorMessage = "{0} is required")]
        public string? TRN_NO { get; set; }

        [Display(Name = "Ref Trn Id")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? REF_TRN_ID { get; set; } = "";

        [Display(Name = "Trn Date")]
        [Required(ErrorMessage = "{0} is required")]
        public DateTime TRN_DATE { get; set; }

        [Display(Name = "Note")]
        [StringLength(250, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? TRN_NOTE { get; set; }


        //Office
        [Display(Name = "From Sub Section Id")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        [Required(ErrorMessage = "{0} is required")]
        public string? FROM_SUB_SECTION_ID { get; set; }

        [Display(Name = "To Sub Section Id")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        [Required(ErrorMessage = "{0} is required")]
        public string? TO_SUB_SECTION_ID { get; set; }

        [Display(Name = "User Id")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        [Required(ErrorMessage = "{0} is required")]
        public string? FROM_USER_ID { get; set; }

        [Display(Name = "Assign User Id")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        [Required(ErrorMessage = "{0} is required")]
        public string? TO_USER_ID { get; set; }

        [Display(Name = "Trn Document")]
        [StringLength(100, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? TRN_DOCUMENT { get; set; }


        //Customer
        [Display(Name = "Customer Id")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        [Required(ErrorMessage = "{0} is required")]
        public string? CONTACT_ID { get; set; }

        [Display(Name = "Customer Bill To Id")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        [Required(ErrorMessage = "{0} is required")]
        public string? CONTACT_ADDRESS_ID { get; set; }

        [Display(Name = "Delivery Address Id")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        [Required(ErrorMessage = "{0} is required")]
        public string? DELIVERY_ADDRESS_ID { get; set; }

        [Display(Name = "Customer Ref No")]
        [StringLength(100, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? CUSTOMER_REF_NO { get; set; }

        [Display(Name = "Customer Document")]
        [StringLength(100, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? CUSTOMER_DOCUMENT { get; set; }

        [Display(Name = "Shipping Mode")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        [Required(ErrorMessage = "{0} is required")]
        public string? SHIPPING_MODE_ID { get; set; }

        [Display(Name = "Shipping Type")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        [Required(ErrorMessage = "{0} is required")]
        public string? SHIPPING_TYPE_ID { get; set; }

        [Display(Name = "Partial Delivery")]
        public bool ALLOW_PARTIAL_DELIVERY { get; set; }

        [Display(Name = "Sample")]
        public bool REQUIRED_SAMPLE { get; set; }

        [Display(Name = "Contact Note")]
        [StringLength(250, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? CONTACT_NOTE { get; set; }



        //Process
        [Display(Name = "Sample Delivery Date")]
        [Required(ErrorMessage = "{0} is required")]
        public DateTime LAST_SDD_DATE { get; set; }

        [Display(Name = "Last MFG Date")]
        [Required(ErrorMessage = "{0} is required")]
        public DateTime LAST_MFG_DATE { get; set; }

        [Display(Name = "Last Delivery Date")]
        [Required(ErrorMessage = "{0} is required")]
        public DateTime LAST_DELIVERY_DATE { get; set; }

        [Display(Name = "SDD Days")]
        public int SDD { get; set; } = 0;

        [Display(Name = "MFG Days")]
        public int FGD { get; set; } = 0;

        [Display(Name = "Delivery Days")]
        public int LDD { get; set; } = 0;

        //Trn Amount
        [Display(Name = "Valid Days")]
        public int TRN_VALID_DAYS { get; set; }

        [Display(Name = "Amount")]
        [Required(ErrorMessage = "{0} is required")]
        [Column(TypeName = "decimal(18, 6)")]
        public decimal TRN_AMOUNT { get; set; } = 0;

        [Display(Name = "Charge")]
        [Column(TypeName = "decimal(18, 6)")]
        public decimal CHARGE_AMOUNT { get; set; } = 0; //+

        [Display(Name = "Discount Amount")]
        [Column(TypeName = "decimal(18, 6)")]
        public decimal DISCOUNT_AMOUNT { get; set; } = 0; //-

        [Display(Name = "Additional Discount")]
        [Column(TypeName = "decimal(18, 6)")]
        public decimal ADD_DISCOUNT_AMOUNT { get; set; } = 0; //-

        [Display(Name = "VAT Amount")]
        [Column(TypeName = "decimal(18, 6)")]
        public decimal VAT_AMOUNT { get; set; } = 0; //+

        [Display(Name = "Net Amount")]
        [Required(ErrorMessage = "{0} is required")]
        [Column(TypeName = "decimal(18, 6)")]
        public decimal NET_AMOUNT { get; set; } = 0;


        //Payment
        [Display(Name = "Advanced Payment Amount")]
        [Column(TypeName = "decimal(18, 6)")]
        public decimal ADVANCED_PAYMENT_AMOUNT { get; set; } = 0; //-

        [Display(Name = "Paid Amount")]
        [Column(TypeName = "decimal(18, 6)")]
        public decimal PAID_AMOUNT { get; set; } = 0; //-

        [Display(Name = "Due Amount")]
        [Required(ErrorMessage = "{0} is required")]
        [Column(TypeName = "decimal(18, 6)")]
        public decimal DUE_AMOUNT { get; set; } = 0; 

        [Display(Name = "Payment Mode")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        [Required(ErrorMessage = "{0} is required")]
        public string? PAYMENT_MODE { get; set; }

        [Display(Name = "Payment Method")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        [Required(ErrorMessage = "{0} is required")]
        public string? PAYMENT_METHOD { get; set; }

        [Display(Name = "Is Paid")]
        public bool IS_PAID { get; set; }

        //Cancel
        [Display(Name = "Is Cancelled")]
        public bool IS_CANCELLED { get; set; }

        [Display(Name = "Cancelled User Id")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? CANCELLED_USER_ID { get; set; }

        [Display(Name = "Cancelled Date")]
        public DateTime? CANCELLED_DATE { get; set; }

        [Display(Name = "Cancelled Note")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? CANCELLED_NOTE { get; set; }

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


        //Next Step
        [Display(Name = "Next Step")]
        public bool NEXT_STEP { get; set; }
    }
}
