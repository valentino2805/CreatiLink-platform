namespace CreatiLinkPlatform.API.Projects.Interfaces.REST.Resources;

public record ProjectResource(
    int Id,
    int ProfileId,
    string Title,
    string Image,
    string Likes,
    string Comments,
    string Description,
    List<string> Technologies
);
