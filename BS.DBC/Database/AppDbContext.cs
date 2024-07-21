namespace BS.DBC.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        //Utility
        public DbSet<USER_LOGIN_INFO> USER_LOGIN_INFO { get; set; }

        //Setup
        public DbSet<BANK_INFO> BANK_INFO { get; set; }
        public DbSet<BANK_BRANCH> BANK_BRANCH { get; set; }
        public DbSet<COUNTRY_INFO> COUNTRY_INFO { get; set; }
        public DbSet<CURRENCY_INFO> CURRENCY_INFO { get; set; }
        public DbSet<CURRENCY_CONV_RATE> CURRENCY_CONV_RATE { get; set; }
        public DbSet<FINANCIAL_YEAR> FINANCIAL_YEAR { get; set; }
        public DbSet<LEAVE_TYPE> LEAVE_TYPE { get; set; }
        public DbSet<LEAVE_CALENDAR> LEAVE_CALENDAR { get; set; }


        public DbSet<Abc>? Abc { get; set; }

    }
}
