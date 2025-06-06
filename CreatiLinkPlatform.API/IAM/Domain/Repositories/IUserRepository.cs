using CreatiLinkPlatform.API.IAM.Domain.Model.Aggregates;
using CreatiLinkPlatform.API.Shared.Domain.Repositories;

namespace CreatiLinkPlatform.API.IAM.Domain.Repositories;

public interface IUserRepository : IBaseRepository<Users>
{
    Task<Users?> FindByEmailAsync(string email);
    bool ExistsByEmail(string email);

}
