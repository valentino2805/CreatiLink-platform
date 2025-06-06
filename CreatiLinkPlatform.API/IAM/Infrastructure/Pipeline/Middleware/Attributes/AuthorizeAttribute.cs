using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using CreatiLinkPlatform.API.IAM.Domain.Model.Aggregates;

namespace CreatiLinkPlatform.API.IAM.Infrastructure.Pipeline.Middleware.Attributes;


[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter

{

    public void OnAuthorization(AuthorizationFilterContext context)
    {
       
        var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
       
        if (allowAnonymous)
        {
            Console.WriteLine("Skipping authorization");
            return;
        }
      
        var users = (Users?)context.HttpContext.Items["Users"];
        // If the user is not authenticated, return 401 Unauthorized
        if (users is null) context.Result = new UnauthorizedResult();
    }
}