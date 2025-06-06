using CreatiLinkPlatform.API.IAM.Domain.Model.Commands;
using CreatiLinkPlatform.API.IAM.Interfaces.REST.Resources;

namespace CreatiLinkPlatform.API.IAM.Interfaces.REST.Transform;

public static class SignUpCommandFromResourceAssembler
{
    public static SignUpCommand ToCommandFromResource(SignUpResource resource)
    {
        return new SignUpCommand(resource.Email, resource.Password, resource.Role);
    }
}