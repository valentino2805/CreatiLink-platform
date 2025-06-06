namespace CreatiLinkPlatform.API.Projects.Interfaces.REST.Resources;

public record UpdateProjectResource(
    int Id,
    string Title,
    string Image,
    string Description,
    List<string> Technologies
);