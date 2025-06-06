using CreatiLinkPlatform.API.Projects.Domain.Model.Aggregates;
using CreatiLinkPlatform.API.Projects.Domain.Model.Queries;

namespace CreatiLinkPlatform.API.Projects.Domain.Services;

public interface IProjectQueryService
{
    Task<IEnumerable<Project>> Handle(GetAllProjectsQuery query);
    Task<Project?> Handle(GetProjectByIdQuery query);
    Task<IEnumerable<Project>> Handle(GetProjectsByProfileIdQuery query);
}