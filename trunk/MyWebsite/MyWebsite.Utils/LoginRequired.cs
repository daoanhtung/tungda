using System;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using ActionFilterAttribute = System.Web.Mvc.ActionFilterAttribute;

namespace MyWebsite.Utils
{
    public class LoginRequiredAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContextBase context = filterContext.HttpContext;

            if (context.Session[Constants.UserNameSessionKey] == null)
            {
                context.Response.Redirect(Common.GetAppStr("LoginUrl") + "?returnUrl=" + context.Request.Url.AbsolutePath);
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
