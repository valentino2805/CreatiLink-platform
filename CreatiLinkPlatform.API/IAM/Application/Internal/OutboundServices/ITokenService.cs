using CreatiLinkPlatform.API.IAM.Domain.Model.Aggregates;

namespace CreatiLinkPlatform.API.IAM.Application.Internal.OutboundServices;


public interface ITokenService
{

    string GenerateToken(Users users);
    

    Task<int?> ValidateToken(string token);
}