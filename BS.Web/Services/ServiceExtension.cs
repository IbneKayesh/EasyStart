using BS.Infra.Services.Inventory;

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
            //Admin
            
            //Application
            services.AddScoped<ClassicMenuService>();

            //Company
            services.AddTransient<BusinessService>();
            services.AddTransient<BranchTypeService>();
            services.AddTransient<BranchService>();
            services.AddTransient<BranchCostCenterService>();
            services.AddTransient<DepartmentService>();
            services.AddTransient<SectionService>();
            services.AddTransient<SubSectionService>();
            services.AddTransient<BusinessLineService>();
            services.AddTransient<SubSectionsBusinessLineService>();

            //CRM
            services.AddTransient<ContactsService>();

            //Help Desk
            services.AddTransient<TaskNotesService>();
            //HRMS
            //Inventory
            services.AddTransient<ProductBrandService>();
            services.AddTransient<ProductCategoryService>();
            services.AddTransient<ProductClassService>();
            services.AddTransient<ProductGroupService>();
            services.AddTransient<ProductSourceService>();
            services.AddTransient<UnitChildService>();
            services.AddTransient<UnitMasterService>();
            services.AddTransient<SegmentNameTypeService>();
            services.AddTransient<ProductTypeService>();
            services.AddTransient<ProductStatusService>();
            services.AddTransient<ProductSegmentService>();
            //My Drive
            //PM
            //Sales Order
            services.AddTransient<SalesBookingService>();
            //Security
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

            //Utility
            services.AddTransient<UserLoginInfoService>();

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