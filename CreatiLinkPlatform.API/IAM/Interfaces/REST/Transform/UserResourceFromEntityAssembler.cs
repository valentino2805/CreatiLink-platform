using CreatiLinkPlatform.API.IAM.Domain.Model.Aggregates;
using CreatiLinkPlatform.API.IAM.Interfaces.REST.Resources;

namespace CreatiLinkPlatform.API.IAM.Interfaces.REST.Transform;

public static class UserResourceFromEntityAssembler
{
    public static UserResource ToResourceFromEntity(Users users)
    {
        return new UserResource(users.Id, users.Email);
    }
}