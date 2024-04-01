using ControlBS.BusinessObjects;
using ControlBS.BusinessObjects.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ControlBS.WebApi.Utils.Auth{
    public class AuthorizeAttribute : Attribute , IAuthorizationFilter{
        public void OnAuthorization(AuthorizationFilterContext context){
            // skip authorization if action is didecrated with [AllowAnonymous] attribute
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if (allowAnonymous) return;

            //authorization
            var user = (Response<CTPERS>?)context.HttpContext.Items["User"];
            if (user == null){
                // not logged in or role not authorized
                context.Result = new JsonResult(new Response<CTPERS>(StatusCodes.Status401Unauthorized)){StatusCode = StatusCodes.Status401Unauthorized};
            }
        }
    }
}