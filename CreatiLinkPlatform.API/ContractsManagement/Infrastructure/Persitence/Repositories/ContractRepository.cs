using CreatiLinkPlatform.ContractsManagement.Domain.Repositories;
using CreatiLinkPlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using CreatiLinkPlatform.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using CreatiLinkPlatform.ContractsManagement.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace CreatiLinkPlatform.ContractsManagement.Infrastructure.Persitence.Repositories;

public class ContractRepository(AppDbContext context) : BaseRepository<Domain.Model.Aggregates.Contract>(context), IContractRepository
{
    public async Task<IEnumerable<Contract>> FindContractsByUserIdAsync(int userId) =>
        await Context.Set<Contract>()
            .Include(c => c.ClientUser)
            .Include(c => c.DesignerProfile)
            .Where(c => c.ClientUserId == userId)
            .ToListAsync();

    public async Task<Contract?> FindContractByIdAsync(int contractId) =>
        await Context.Set<Contract>()
            .Include(c => c.ClientUser)
            .Include(c => c.DesignerProfile)
            .FirstOrDefaultAsync(c => c.Id == contractId);

    public async Task<IEnumerable<Contract>> FindContractsByDesignerProfileIdAsync(int designerProfileId) =>
        await Context.Set<Contract>()
            .Include(c => c.ClientUser)
            .Include(c => c.DesignerProfile)
            .Where(c => c.DesignerProfileId == designerProfileId)
            .ToListAsync();

    public async Task<bool> ContractExistsForUserAndDesignerProfileAsync(int userId, int designerProfileId) =>
        await Context.Set<Contract>()
            .AnyAsync(c => c.ClientUserId == userId && c.DesignerProfileId == designerProfileId);

    public async Task<IEnumerable<Contract>> FindAllContractsAsync() =>
        await Context.Set<Contract>()
            .Include(c => c.ClientUser)
            .Include(c => c.DesignerProfile)
            .ToListAsync();
}