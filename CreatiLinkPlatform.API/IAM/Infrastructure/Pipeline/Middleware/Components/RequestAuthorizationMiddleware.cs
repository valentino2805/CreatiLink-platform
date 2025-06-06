using CreatiLinkPlatform.API.IAM.Application.Internal.OutboundServices;
using CreatiLinkPlatform.API.IAM.Domain.Model.Queries;
using CreatiLinkPlatform.API.IAM.Domain.Services;
using CreatiLinkPlatform.API.IAM.Infrastructure.Pipeline.Middleware.Attributes;

namespace CreatiLinkPlatform.API.IAM.Infrastructure.Pipeline.Middleware.Components;
/// <summary>
/// Request authorization middleware 
/// </summary>
/// <param name="next">
/// <see cref="RequestDelegate"/> Next middleware in pipeline
/// </param>
public class RequestAuthorizationMiddleware(RequestDelegate next)
{
    
    public async Task InvokeAsync(
        HttpContext context,
        IUserQueryService userQueryService,
        ITokenService tokenService)
    {
        Console.WriteLine("Entering InvokeAsync");
        var allowAnonymous = context.Request.HttpContext.GetEndpoint()!
            .Metadata
            .Any(m => m.GetType() == typeof(AllowAnonymousAttribute));
        Console.WriteLine($"AllowAnonymous: {allowAnonymous}");
        if (allowAnonymous)
        {
            Console.WriteLine("Skipping authorization");
            await next(context);
            return;
        }
        Console.WriteLine("Entering authorization");
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token is null) throw new Exception("Null of invalid token");
        
        var userId = await tokenService.ValidateToken(token);
        
        if (userId is null) throw new Exception("Invalid token");
        
        var getUserByIdQuery = new GetUserByIdQuery(userId.Value);

        var users = await userQueryService.Handle(getUserByIdQuery);
        Console.WriteLine("Successfully authorized. Updating context...");
        context.Items["Users"] = users;
        Console.WriteLine("Continuing to next middleware in pipeline");
        await next(context);
    }
}