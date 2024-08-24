using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using OnlineCateringProject.Models;

namespace OnlineCateringProject.Models.Authentication
{
    public class AuthForAccess : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Session.GetString("access_client") != null)
            {
                OnlineCateringContext db = new();
                var a = db.LoginMasters.FirstOrDefault(x=>x.Name == context.HttpContext.Session.GetString("access_client"));
                if (a != null && (a.UserType == "Customer" || a.UserType == "Caterer"))
                {
                    context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                        {"Controller","Login" },
                        {"Action","Login" }
                    }
                    );
                }
            }
        }
    }
}
