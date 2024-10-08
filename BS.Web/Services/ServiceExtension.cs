﻿namespace BS.Web.Services
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
            //Accounts
            services.AddTransient<LoanService>();

            //Admin
            
            //Application
            services.AddScoped<AppNotificationsService>();
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
            services.AddTransient<WorkTaskService>();
            services.AddTransient<TaskNotesService>();
            services.AddTransient<BoardService>();
            services.AddTransient<RoutineTaskService>();

            //Help Desk > Setup
            services.AddTransient<TaskStatusService>();
            services.AddTransient<RoutineNamesService>();

            //HRMS
            services.AddTransient<AttendanceLogService>();
            services.AddTransient<DesignationService>();
            services.AddTransient<WorkShiftService>();
            services.AddTransient<EmployeesService>();
            services.AddTransient<EmpWorkShiftService>();
            services.AddTransient<SalaryCyclesService>();
            //Inventory
            services.AddTransient<ItemAttributeService>();
            services.AddTransient<ItemAttributeValueService>();
            services.AddTransient<ItemGroupTypeService>();
            services.AddTransient<ItemGroupService>();
            services.AddTransient<ItemSubGroupService>();
            services.AddTransient<ItemCategoryService>();
            services.AddTransient<ItemTypeService>();
            services.AddTransient<ItemStatusService>();
            services.AddTransient<ItemClassService>();
            services.AddTransient<UnitChildService>();
            services.AddTransient<UnitMasterService>();
            services.AddTransient<ItemMasterService>();
            services.AddTransient<ProductsService>();
            //My Drive
            //PM
            //Sales Order
            services.AddTransient<SalesBookingService>();
            //Security
            services.AddTransient<SecurityRoleService>();
            //Setup
            services.AddTransient<BankInfoService>();
            services.AddTransient<BankBranchService>();
            services.AddTransient<CountryInfoService>();
            services.AddTransient<CurrencyInfoService>();
            services.AddTransient<CurrencyConvRateService>();
            services.AddTransient<FinancialYearService>();
            services.AddTransient<HolidayTypeService>();
            services.AddTransient<HolidayCalendarService>();
            services.AddTransient<EntityValueTextService>();
            services.AddTransient<TrnAutoStepService>();
            services.AddTransient<SubSectionsTrnIdService>();
            services.AddTransient<TrnLastNoListService>();

            //Transport
            services.AddTransient<DeliveryAgentService>();
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