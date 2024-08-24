using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace OnlineCateringProject.Models.Authentication
{
    public class AuthForAdmin:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if(context.HttpContext.Session.GetString("access_admin") == null) {
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                        {"Controller","AccessAdmin" },
                        {"Action","LoginAdmin" }
                    }                  
                    );
            }
            if(context.HttpContext.Session.GetString("userType") != "ADMIN")
            {
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                        {"Controller","HomeAdmin" },
                        {"Action","Index" }
                    }
                    );
            }
        }
    }
}
