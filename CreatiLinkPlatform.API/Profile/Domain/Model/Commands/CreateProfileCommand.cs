namespace CreatiLinkPlatform.API.Profile.Domain.Model.Commands;

public record CreateProfileCommand(
    int UserId,
    string Name,
    string Location,
    string Bio,
    string Image,
    string Icon,
    List<string> Experience,
    SocialLinks Social
);
