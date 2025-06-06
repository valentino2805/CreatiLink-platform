using CreatiLinkPlatform.API.Projects.Domain.Model.Aggregates;
using CreatiLinkPlatform.API.Projects.Domain.Model.Commands;

namespace CreatiLinkPlatform.API.Projects.Domain.Services;

public interface IProjectCommandService
{
    Task<Project?> Handle(CreateProjectCommand command);
    Task<Project?> Handle(UpdateProjectCommand command);
    Task<bool> Handle(DeleteProjectCommand command);
}