namespace CreatiLinkPlatform.API.Projects.Domain.Model.Commands;

public record UpdateProjectCommand(
    int Id,
    int ProjectId,
    string Title,
    string Image,
    string Description,
    List<string> Technologies
);