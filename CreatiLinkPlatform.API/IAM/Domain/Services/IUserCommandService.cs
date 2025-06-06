using CreatiLinkPlatform.API.IAM.Domain.Model.Aggregates;
using CreatiLinkPlatform.API.IAM.Domain.Model.Commands;

namespace CreatiLinkPlatform.API.IAM.Domain.Services;


public interface IUserCommandService
{

    Task Handle(SignUpCommand command);
    

    Task<(Users user, string token)> Handle(SignInCommand command);
}