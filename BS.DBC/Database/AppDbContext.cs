using BS.DMO.Models.Accounts.BankLoan;

namespace BS.DBC.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Other configurations or mappings go here
            modelBuilder.Entity<SUB_SECTIONS_TRN_ID>().HasKey(sf => new { sf.SUB_SECTION_ID, sf.TRN_ID });
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SUB_SECTIONS_BUSINESS_LINE>().HasKey(sf => new { sf.SUB_SECTION_ID, sf.BUSINESS_LINE_ID });
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MENU_ROLE>().HasKey(sf => new { sf.MENU_ID, sf.ROLE_ID });
            base.OnModelCreating(modelBuilder);
        }
        //Accounts
        public DbSet<BANK_LOAN_SCHEDULE> BANK_LOAN_SCHEDULE { get; set; }
        public DbSet<BANK_LOAN_PAYMENTS> BANK_LOAN_PAYMENTS { get; set; }
        public DbSet<BANK_LOAN_MASTER> BANK_LOAN_MASTER { get; set; }
        public DbSet<BANK_LOAN_FINES> BANK_LOAN_FINES { get; set; }

        //Admin

        //Application
        public DbSet<APP_NOTIFICATIONS> APP_NOTIFICATIONS { get; set; }
        public DbSet<CLASSIC_MENU> CLASSIC_MENU { get; set; }

        //Company
        public DbSet<BRANCH_TYPE> BRANCH_TYPE { get; set; }
        public DbSet<BUSINESS> BUSINESS { get; set; }
        public DbSet<BRANCH> BRANCH { get; set; }
        public DbSet<BRANCH_COST_CENTER> BRANCH_COST_CENTER { get; set; }
        public DbSet<DEPARTMENTS> DEPARTMENTS { get; set; }
        public DbSet<SECTIONS> SECTIONS { get; set; }
        public DbSet<SUB_SECTIONS> SUB_SECTIONS { get; set; }
        public DbSet<BUSINESS_LINE> BUSINESS_LINE { get; set; }
        public DbSet<SUB_SECTIONS_BUSINESS_LINE> SUB_SECTIONS_BUSINESS_LINE { get; set; }

        //CRM
        public DbSet<CONTACTS> CONTACTS { get; set; }
        public DbSet<CONTACT_ADDRESS> CONTACT_ADDRESS { get; set; }

        //Help desk
        public DbSet<TASK_STATUS> TASK_STATUS { get; set; }
        public DbSet<BOARDS> BOARDS { get; set; }
        public DbSet<BOARD_GROUP> BOARD_GROUP { get; set; }
        public DbSet<TASK_NOTES> TASK_NOTES { get; set; }
        public DbSet<WORK_TASK> WORK_TASK { get; set; }
        public DbSet<ROUTINE_NAMES> ROUTINE_NAMES { get; set; }
        public DbSet<ROUTINE_TASK> ROUTINE_TASK { get; set; }
        public DbSet<Abc>? Abc { get; set; }

        //HRMS
        public DbSet<ATTENDANCE_LOG> ATTENDANCE_LOG { get; set; }
        public DbSet<WORK_SHIFT> WORK_SHIFT { get; set; }
        public DbSet<DESIGNATION> DESIGNATION { get; set; }
        public DbSet<EMPLOYEES> EMPLOYEES { get; set; }
        //Inventory
        public DbSet<PRODUCT_BRAND> PRODUCT_BRAND { get; set; }
        public DbSet<PRODUCT_CATEGORY> PRODUCT_CATEGORY { get; set; }
        public DbSet<PRODUCT_CLASS> PRODUCT_CLASS { get; set; }
        public DbSet<PRODUCT_GROUP> PRODUCT_GROUP { get; set; }
        public DbSet<PRODUCT_SOURCE> PRODUCT_SOURCE { get; set; }
        public DbSet<UNIT_CHILD> UNIT_CHILD { get; set; }
        public DbSet<UNIT_MASTER> UNIT_MASTER { get; set; }
        public DbSet<PRODUCT_SEGMENTS> PRODUCT_SEGMENTS { get; set; }
        public DbSet<SEGMENT_NAME_TYPE> SEGMENT_NAME_TYPE { get; set; }
        public DbSet<PRODUCT_TYPE> PRODUCT_TYPE { get; set; }
        public DbSet<PRODUCT_STATUS> PRODUCT_STATUS { get; set; }
        public DbSet<PRODUCTS> PRODUCTS { get; set; }
        //My Drive
        //PM

        //Sales Order
        public DbSet<SB_MASTER> SB_MASTER { get; set; }
        public DbSet<SB_CHILD> SB_CHILD { get; set; }

        //Security
        public DbSet<SECURITY_ROLE> SECURITY_ROLE { get; set; }
        public DbSet<MENU_ROLE> MENU_ROLE { get; set; }
        //Setup
        public DbSet<BANK_INFO> BANK_INFO { get; set; }
        public DbSet<BANK_BRANCH> BANK_BRANCH { get; set; }
        public DbSet<COUNTRY_INFO> COUNTRY_INFO { get; set; }
        public DbSet<CURRENCY_INFO> CURRENCY_INFO { get; set; }
        public DbSet<CURRENCY_CONV_RATE> CURRENCY_CONV_RATE { get; set; }
        public DbSet<FINANCIAL_YEAR> FINANCIAL_YEAR { get; set; }
        public DbSet<LEAVE_TYPE> LEAVE_TYPE { get; set; }
        public DbSet<LEAVE_CALENDAR> LEAVE_CALENDAR { get; set; }
        public DbSet<ENTITY_VALUE_TEXT> ENTITY_VALUE_TEXT { get; set; }
        public DbSet<TRN_AUTO_STEP> TRN_AUTO_STEP { get; set; }
        public DbSet<SUB_SECTIONS_TRN_ID> SUB_SECTIONS_TRN_ID { get; set; }
        public DbSet<TRN_LAST_NO_LIST> TRN_LAST_NO_LIST { get; set; }
        
        //Transport
        public DbSet<DELIVERY_AGENT> DELIVERY_AGENT { get; set; }

        //Utility
        public DbSet<USER_LOGIN_INFO> USER_LOGIN_INFO { get; set; }

    }
}
