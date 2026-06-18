using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebUI.Controllers
{
    public abstract class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var username = HttpContext.Session.GetString("username");
            
            if (string.IsNullOrEmpty(username))
            {
                filterContext.Result = RedirectToAction("Login", "Account");
                return;
            }
            
            base.OnActionExecuting(filterContext);
        }
    }
}