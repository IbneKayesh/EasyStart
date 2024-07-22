namespace BS.Web.Services
{
    internal static class RoutingExtension
    {
        public static IEndpointRouteBuilder AreaEndpointRouteBuilder(this IEndpointRouteBuilder endpointRoute)
        {
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
                        name: "AreaSalesOrder",
                        areaName: "SalesOrder",
                        pattern: "SalesOrder/{controller=Home}/{action=Index}/{id?}");

            endpointRoute.MapAreaControllerRoute(
                        name: "AreaSetup",
                        areaName: "Setup",
                        pattern: "Setup/{controller=Home}/{action=Index}/{id?}");

            return endpointRoute;

        }
    }
}
