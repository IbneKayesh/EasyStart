using BS.Infra.Services.Application;
using BS.Infra.Services.HelpDesk;

namespace BS.Web.Services
{
    internal static class ServiceExtension
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options
                .UseSqlServer(configuration.GetConnectionString(StaticKeys.ConnectionString))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            //Application
            services.AddScoped<ClassicMenuService>();
            
            //Utility
            services.AddTransient<UserLoginInfoService>();

            //Setup
            services.AddTransient<BankInfoService>();
            services.AddTransient<BankBranchService>();
            services.AddTransient<CountryInfoService>();
            services.AddTransient<CurrencyInfoService>();
            services.AddTransient<CurrencyConvRateService>();
            services.AddTransient<FinancialYearService>();
            services.AddTransient<LeaveTypeService>();
            services.AddTransient<LeaveCalendarService>();
            services.AddTransient<EntityValueTextService>();
            services.AddTransient<TrnAutoStepService>();
            services.AddTransient<SubSectionsTrnIdService>();

            //Company
            services.AddTransient<BusinessService>();
            services.AddTransient<BranchTypeService>();
            services.AddTransient<BranchService>();
            services.AddTransient<BranchCostCenterService>();
            services.AddTransient<DepartmentService>();
            services.AddTransient<SectionService>();
            services.AddTransient<SubSectionService>();

            //Help Desk
            services.AddTransient<TaskNotesService>();
            
            //SalesBooking
            services.AddTransient<SalesBookingService>();

            //services.AddScoped<IUnitOfWork, UnitOfWork>();
            //services.AddScoped<ITransactionNoBuilderRep, TransactionNoBuilderRep>();
            //services.Configure<APP_CONFIG>(configuration.GetSection("AppConfig"));
            return services;
        }
    }
}

//|:---------:|:------------------------------------------:|:----------------------------------------------------:|:------------------------------------:|
//| Parameter | Add Singleton                              | Add Scoped                                           | Add Transient                        |
//|:---------:|:------------------------------------------:|:----------------------------------------------------:|:------------------------------------:|
//| Instance  | Same for each request/ each user.          | One per request.                                     | Different for every time.            |
//| Disposed  | App shutdown                               | End of request                                       | End of request                       |
//| Used in   | When Singleton implementation is required. | Applications that have different behavior per user.  | Lightweight and stateless services.  |
//|:---------:|:------------------------------------------:|:----------------------------------------------------:|:------------------------------------:|