using CreatiLinkPlatform.API.Projects.Domain.Model.Aggregates;
using CreatiLinkPlatform.API.Shared.Domain.Repositories;

namespace CreatiLinkPlatform.API.Projects.Domain.Repositories;

public interface IProjectRepository : IBaseRepository<Project>
{
    Task<IEnumerable<Project>> FindByProfileIdAsync(int profileId);
    Task<Project?> FindByIdAsync(int projectId); 
}