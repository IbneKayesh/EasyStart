using Microsoft.AspNetCore.Mvc.Filters;

namespace BS.Web.Services
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AppAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string[] _roles;

        public AppAuthorizeAttribute(string roles)
        {
            _roles = roles.Split(',').Select(r => r.Trim()).ToArray();
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //ap user session key
            //var user = context.HttpContext.Session.Get<USERS>("ap_usk");
            //if (user == null || !_roles.Any(requiredRole => user.USER_ROLE == requiredRole))
            //{
            //    context.Result = new RedirectToActionResult("Login", "Home", new { area = "" });
            //}
        }
    }
}
