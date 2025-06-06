using CreatiLinkPlatform.API.Shared.Domain.Repositories;
using CreatiLinkPlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace CreatiLinkPlatform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    
    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task CompleteAsync()
    {
        await _context.SaveChangesAsync();
    }
}