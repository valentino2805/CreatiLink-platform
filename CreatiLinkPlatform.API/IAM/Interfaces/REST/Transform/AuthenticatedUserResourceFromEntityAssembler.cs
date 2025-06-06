using CreatiLinkPlatform.API.IAM.Domain.Model.Aggregates;
using CreatiLinkPlatform.API.IAM.Interfaces.REST.Resources;

namespace CreatiLinkPlatform.API.IAM.Interfaces.REST.Transform;


public static class AuthenticatedUserResourceFromEntityAssembler
{
    public static AuthenticatedUserResource ToResourceFromEntity(Users users, string token)
    {
        return new AuthenticatedUserResource(users.Id, users.Email, token, users.Role);
    }
}
