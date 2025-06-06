namespace CreatiLinkPlatform.API.Projects.Interfaces.REST.Resources;

public record CreateProjectResource(
    int ProfileId,
    string Title,
    string Image,
    string Description,
    List<string> Technologies
);