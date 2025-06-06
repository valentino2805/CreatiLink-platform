using CreatiLinkPlatform.API.Projects.Domain.Model.Commands;
using CreatiLinkPlatform.API.Projects.Interfaces.REST.Resources;

namespace CreatiLinkPlatform.API.Projects.Interfaces.REST.Transform;

public static class UpdateProjectCommandFromResourceAssembler
{
    public static UpdateProjectCommand ToCommandFromResource(int projectId, UpdateProjectResource resource)
    {
        return new UpdateProjectCommand(
            resource.Id,
            projectId,
            resource.Title,
            resource.Image,
            resource.Description,
            resource.Technologies
        );
    }
}