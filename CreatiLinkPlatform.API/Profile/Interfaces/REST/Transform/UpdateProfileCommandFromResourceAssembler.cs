using CreatiLinkPlatform.API.Profile.Domain.Model.Commands;
using CreatiLinkPlatform.API.Profile.Interfaces.REST.Resources;

namespace CreatiLinkPlatform.API.Profile.Interfaces.REST.Transform;

public static class UpdateProfileCommandFromResourceAssembler
{
    public static UpdateProfileCommand ToCommand(int id, UpdateProfileResource resource)
    {
        return new UpdateProfileCommand(
            id,
            resource.Name,
            resource.Location,
            resource.Bio,
            resource.Image,
            resource.Icon,
            resource.Experience,
            new SocialLinks(
                resource.Social.Instagram,
                resource.Social.Facebook,
                resource.Social.X
            )
        );
    }
}