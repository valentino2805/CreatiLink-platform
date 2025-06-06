using CreatiLinkPlatform.API.Shared.Domain.Repositories;

using CreatiLinkPlatform.API.Profile.Domain.Model.Aggregates;
using CreatiLinkPlatform.API.Profile.Domain.Repositories;
using CreatiLinkPlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using CreatiLinkPlatform.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;
using CreatiLinkPlatform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace CreatiLinkPlatform.API.Profile.Infrastructure.Repositories;

public class ProfileRepository(AppDbContext context) 
    : BaseRepository<Domain.Model.Aggregates.Profile>(context), IProfileRepository
{
    public async Task<Domain.Model.Aggregates.Profile?> FindByIdAsync(int id) =>
        await Context.Set<Domain.Model.Aggregates.Profile>().FirstOrDefaultAsync(p => p.Id == id);

    public async Task<IEnumerable<Domain.Model.Aggregates.Profile>> FindByNameAsync(string name) =>
        await Context.Set<Domain.Model.Aggregates.Profile>()
            .Where(p => p.Name.Contains(name))
            .ToListAsync();
    
    public new async Task<IEnumerable<Domain.Model.Aggregates.Profile>> ListAsync() =>
        await Context.Set<Domain.Model.Aggregates.Profile>().ToListAsync();
    
    public async Task<Domain.Model.Aggregates.Profile?> FindByUserIdAsync(int userId)
    {
        return await Context.Set<Domain.Model.Aggregates.Profile>()
            .FirstOrDefaultAsync(p => p.UserId == userId);
    }




}