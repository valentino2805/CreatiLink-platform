using CreatiLinkPlatform.API.Projects.Domain.Model.Aggregates;
using CreatiLinkPlatform.API.Projects.Interfaces.REST.Resources;

namespace CreatiLinkPlatform.API.Projects.Interfaces.REST.Transform;

public static class ProjectResourceFromEntityAssembler
{
    public static ProjectResource ToResourceFromEntity(Project entity)
    {
        return new ProjectResource(
            entity.Id,
            entity.ProfileId,
            entity.Title,
            entity.Image,
            entity.Likes,
            entity.Comments,
            entity.Description,
            entity.Technologies
        );
    }
}