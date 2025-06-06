using CreatiLinkPlatform.API.Profile.Domain.Model.Aggregates;
using CreatiLinkPlatform.API.Profile.Interfaces.REST.Resources;

namespace CreatiLinkPlatform.API.Profile.Interfaces.REST.Transform;

public static class ProfileResourceFromEntityAssembler
{
    public static ProfileResource ToResource(Domain.Model.Aggregates.Profile entity)
    {
        return new ProfileResource(
            entity.Id,
            entity.Name,
            entity.Location,
            entity.Bio,
            entity.Image,
            entity.Icon,
            entity.Experience,
            new SocialLinks(
                entity.Social.Instagram,
                entity.Social.Facebook,
                entity.Social.X
            ),
            entity.UserId // 👈 AÑADIDO AQUÍ
        );
    }
}
