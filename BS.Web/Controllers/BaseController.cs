namespace BS.Web.Controllers
{
    public class BaseController : Controller
    {
        public USER_SESSION user_session;
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var session = context.HttpContext.Session;

            if (session.GetString(StaticKeys.SessionName) == null)
            {
                string nextUrl = context.HttpContext.Request.Path + context.HttpContext.Request.QueryString;
                context.Result = new RedirectToActionResult("Login", "Home", new { area = "", next_url = nextUrl });
                return;
            }
            else
            {
                user_session = session.GetObject<USER_SESSION>(StaticKeys.SessionName);
                //context.HttpContext.Items["_user_id"] = user.USER_ID;
            }

            base.OnActionExecuting(context);
        }
    }
}
