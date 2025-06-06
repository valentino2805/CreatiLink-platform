namespace CreatiLinkPlatform.API.Profile.Domain.Model.Commands;

public record UpdateProfileCommand(
    
    int Id,
    string Name,
    string Location,
    string Bio,
    string Image,
    string Icon,
    List<string> Experience,
    SocialLinks Social
);

public record SocialLinks(
    string Instagram,
    string Facebook,
    string X
);
