using CreatiLinkPlatform.API.Profile.Domain.Model.Aggregates;
using CreatiLinkPlatform.API.Shared.Domain.Repositories;
namespace CreatiLinkPlatform.API.Profile.Domain.Repositories;


public interface IProfileRepository : IBaseRepository<Model.Aggregates.Profile>
{
    Task<Model.Aggregates.Profile?> FindByIdAsync(int id);          
    Task<IEnumerable<Model.Aggregates.Profile>> FindByNameAsync(string name);  
    
    Task<Model.Aggregates.Profile?> FindByUserIdAsync(int userId);

}