using CreatiLinkPlatform.API.IAM.Domain.Model.Aggregates;
using CreatiLinkPlatform.API.IAM.Domain.Model.Queries;

namespace CreatiLinkPlatform.API.IAM.Domain.Services;


public interface IUserQueryService
{

    Task<Users?> Handle(GetUserByIdQuery query);
    

    Task<IEnumerable<Users>> Handle(GetAllUsersQuery query);
    

    Task<Users?> Handle(GetUserByEmailQuery query);
}