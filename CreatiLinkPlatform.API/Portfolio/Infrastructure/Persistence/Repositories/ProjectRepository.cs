using Microsoft.EntityFrameworkCore;
using CreatiLinkPlatform.API.Projects.Domain.Model.Aggregates;
using CreatiLinkPlatform.API.Projects.Domain.Repositories;
using CreatiLinkPlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using CreatiLinkPlatform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace CreatiLinkPlatform.API.Projects.Infrastructure.Persistence.Repositories;

public class ProjectRepository(AppDbContext context) : BaseRepository<Project>(context), IProjectRepository
{
    public async Task<IEnumerable<Project>> FindByProfileIdAsync(int profileId) =>
        await Context.Set<Project>()
            .Where(p => p.ProfileId == profileId)
            .ToListAsync();

    public new async Task<Project?> FindByIdAsync(int id) =>
        await Context.Set<Project>()
            .Where(p => p.Id == id)
            .FirstOrDefaultAsync();

    public new async Task<IEnumerable<Project>> ListAsync() =>
        await Context.Set<Project>().ToListAsync();
}