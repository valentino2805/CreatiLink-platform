using CreatiLinkPlatform.API.IAM.Domain.Model.Commands;
using CreatiLinkPlatform.API.IAM.Interfaces.REST.Resources;

namespace CreatiLinkPlatform.API.IAM.Interfaces.REST.Transform;

public static class SignInCommandFromResourceAssembler
{
    public static SignInCommand ToCommandFromResource(SignInResource resource)
    {
        return new SignInCommand(resource.Email, resource.Password);
    }
}