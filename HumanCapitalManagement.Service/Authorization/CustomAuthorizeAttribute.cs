using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace HumanCapitalManagement.Service.Authorization;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Interface)]
public class CustomAuthorizeAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var ClientId = context.HttpContext.Items["clientId"];
        if (ClientId == null)
        {
            context.Result = new JsonResult(new { message = "You are not authorized to access this resource!" }) { StatusCode = StatusCodes.Status401Unauthorized };
        }
    }
}
