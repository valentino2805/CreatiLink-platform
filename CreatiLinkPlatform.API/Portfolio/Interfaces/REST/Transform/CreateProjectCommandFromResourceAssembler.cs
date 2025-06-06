using CreatiLinkPlatform.API.Projects.Domain.Model.Commands;
using CreatiLinkPlatform.API.Projects.Interfaces.REST.Resources;

namespace CreatiLinkPlatform.API.Projects.Interfaces.REST.Transform;

public static class CreateProjectCommandFromResourceAssembler
{
    public static CreateProjectCommand ToCommandFromResource(CreateProjectResource resource)
    {
        return new CreateProjectCommand(
            resource.ProfileId,
            resource.Title,
            resource.Image,
            resource.Description,
            resource.Technologies
        );
    }
}