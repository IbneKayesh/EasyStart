using BS.DMO.Models.Inventory;

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
        }
        //Admin

        //Application
        public DbSet<CLASSIC_MENU> CLASSIC_MENU { get; set; }

        //Company
        public DbSet<BRANCH_TYPE> BRANCH_TYPE { get; set; }
        public DbSet<BUSINESS> BUSINESS { get; set; }
        public DbSet<BRANCH> BRANCH { get; set; }
        public DbSet<BRANCH_COST_CENTER> BRANCH_COST_CENTER { get; set; }
        public DbSet<DEPARTMENTS> DEPARTMENTS { get; set; }
        public DbSet<SECTIONS> SECTIONS { get; set; }
        public DbSet<SUB_SECTIONS> SUB_SECTIONS { get; set; }

        //CRM
        public DbSet<CONTACTS> CONTACTS { get; set; }
        public DbSet<CONTACT_ADDRESS> CONTACT_ADDRESS { get; set; }

        //Help desk
        public DbSet<TASK_NOTES> TASK_NOTES { get; set; }
        public DbSet<Abc>? Abc { get; set; }

        //HRMS
        //Inventory
        public DbSet<PRODUCT_BRAND> PRODUCT_BRAND { get; set; }
        //My Drive
        //PM

        //Sales Order
        public DbSet<SB_MASTER> SB_MASTER { get; set; }
        public DbSet<SB_CHILD> SB_CHILD { get; set; }

        //Security
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

        //Utility
        public DbSet<USER_LOGIN_INFO> USER_LOGIN_INFO { get; set; }

    }
}
