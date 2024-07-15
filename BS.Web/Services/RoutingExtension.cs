namespace BS.Web.Services
{
    internal static class RoutingExtension
    {
        public static IEndpointRouteBuilder AreaEndpointRouteBuilder(this IEndpointRouteBuilder endpointRoute)
        {
            endpointRoute.MapAreaControllerRoute(
                        name: "AreaSetup",
                        areaName: "Setup",
                        pattern: "Setup/{controller=Home}/{action=Index}/{id?}");

            return endpointRoute;

        }
    }
}
