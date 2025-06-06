using Microsoft.EntityFrameworkCore;
using CreatiLinkPlatform.API.IAM.Domain.Model.Aggregates;
using CreatiLinkPlatform.API.IAM.Domain.Repositories;
using CreatiLinkPlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using CreatiLinkPlatform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace CreatiLinkPlatform.API.IAM.Infrastructure.Persistence.EFC.Repositories;


public class UserRepository(AppDbContext context) : BaseRepository<Users>(context), IUserRepository
{
    // inheritedDoc
    public async Task<Users?> FindByEmailAsync(string email)
    {
        return await Context.Set<Users>().FirstOrDefaultAsync(users => users.Email.Equals(email));
    }

    // inheritedDoc
    public bool ExistsByEmail(string email)
    {
        return Context.Set<Users>().Any(users => users.Email.Equals(email));
    }
}