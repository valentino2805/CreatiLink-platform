namespace CreatiLinkPlatform.API.Projects.Domain.Model.Commands;

public record CreateProjectCommand(
    int ProfileId,
    string Title,
    string Image,
    string Description,
    List<string> Technologies
);