namespace CreatiLinkPlatform.API.Profile.Interfaces.REST.Resources;
using CreatiLinkPlatform.API.Profile.Domain.Model.Aggregates;

public record CreateProfileResource(
    int UserId,
    string Name,
    string Location,
    string Bio,
    string Image,
    string Icon,
    List<string> Experience,
    SocialLinks Social
);