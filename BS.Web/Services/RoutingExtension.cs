namespace BS.Web.Services
{
    internal static class RoutingExtension
    {
        public static IEndpointRouteBuilder AreaEndpointRouteBuilder(this IEndpointRouteBuilder endpointRoute)
        {
            endpointRoute.MapAreaControllerRoute(
                        name: "AreaAccounts",
                        areaName: "Accounts",
                        pattern: "Accounts/{controller=Home}/{action=Index}/{id?}");

            endpointRoute.MapAreaControllerRoute(
                        name: "AreaAdmin",
                        areaName: "Admin",
                        pattern: "Admin/{controller=Home}/{action=Index}/{id?}");

            endpointRoute.MapAreaControllerRoute(
                        name: "AreaApplication",
                        areaName: "Application",
                        pattern: "Application/{controller=Home}/{action=Index}/{id?}");

            endpointRoute.MapAreaControllerRoute(
                        name: "AreaCompany",
                        areaName: "Company",
                        pattern: "Company/{controller=Home}/{action=Index}/{id?}");

            endpointRoute.MapAreaControllerRoute(
                        name: "AreaCRM",
                        areaName: "CRM",
                        pattern: "CRM/{controller=Home}/{action=Index}/{id?}");

            endpointRoute.MapAreaControllerRoute(
                        name: "AreaHelpDesk",
                        areaName: "HelpDesk",
                        pattern: "HelpDesk/{controller=Home}/{action=Index}/{id?}");

            endpointRoute.MapAreaControllerRoute(
                        name: "AreaHRMS",
                        areaName: "HRMS",
                        pattern: "HRMS/{controller=Home}/{action=Index}/{id?}");

            endpointRoute.MapAreaControllerRoute(
                        name: "AreaInventory",
                        areaName: "Inventory",
                        pattern: "Inventory/{controller=Home}/{action=Index}/{id?}");

            endpointRoute.MapAreaControllerRoute(
                        name: "AreaSalesOrder",
                        areaName: "SalesOrder",
                        pattern: "SalesOrder/{controller=Home}/{action=Index}/{id?}");

            endpointRoute.MapAreaControllerRoute(
                        name: "AreaSecurity",
                        areaName: "Security",
                        pattern: "Security/{controller=Home}/{action=Index}/{id?}");

            endpointRoute.MapAreaControllerRoute(
                        name: "AreaSetup",
                        areaName: "Setup",
                        pattern: "Setup/{controller=Home}/{action=Index}/{id?}");

            endpointRoute.MapAreaControllerRoute(
                        name: "AreaTransport",
                        areaName: "Transport",
                        pattern: "Transport/{controller=Home}/{action=Index}/{id?}");

            return endpointRoute;

        }
    }
}
