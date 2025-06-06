using CreatiLinkPlatform.API.Projects.Domain.Model.Aggregates;
using CreatiLinkPlatform.API.Projects.Domain.Model.Queries;
using CreatiLinkPlatform.API.Projects.Domain.Repositories;
using CreatiLinkPlatform.API.Projects.Domain.Services;

namespace CreatiLinkPlatform.API.Projects.Application.Internal.QueryServices.acl;

public class ProjectQueryService(IProjectRepository projectRepository) : IProjectQueryService
{
    public async Task<Project?> Handle(GetProjectByIdQuery query)
    {
        return await projectRepository.FindByIdAsync(query.ProjectId);
    }

    public async Task<IEnumerable<Project>> Handle(GetAllProjectsQuery query)
    {
        return await projectRepository.ListAsync();
    }

    public async Task<IEnumerable<Project>> Handle(GetProjectsByProfileIdQuery query)
    {
        return await projectRepository.FindByProfileIdAsync(query.ProfileId);
    }
}