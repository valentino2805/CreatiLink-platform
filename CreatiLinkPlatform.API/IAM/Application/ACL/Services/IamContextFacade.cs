using CreatiLinkPlatform.API.IAM.Domain.Model.Commands;
using CreatiLinkPlatform.API.IAM.Domain.Model.Queries;
using CreatiLinkPlatform.API.IAM.Domain.Services;
using CreatiLinkPlatform.API.IAM.Interfaces.ACL;

namespace CreatiLinkPlatform.API.IAM.Application.ACL.Services;


public class IamContextFacade(
    IUserCommandService userCommandService,
    IUserQueryService userQueryService) : IIamContextFacade
{
    // <inheritdoc />
    public async Task<int> CreateUser(string email, string password, string role)
    {
        var signUpCommand = new SignUpCommand(email, password, role); // Ahora se usa el role recibido
        await userCommandService.Handle(signUpCommand);

        var getUserByEmailQuery = new GetUserByEmailQuery(email);
        var result = await userQueryService.Handle(getUserByEmailQuery);
        return result?.Id ?? 0;
    }


    // <inheritdoc />
    public async Task<int> FetchUserIdByEmail(string email)
    {
        var getUserByEmailQuery = new GetUserByEmailQuery(email);
        var result = await userQueryService.Handle(getUserByEmailQuery);
        return result?.Id ?? 0;
    }

    // <inheritdoc />
    public async Task<string> FetchEmailByUserId(int userId)
    {
        var getUserByIdQuery = new GetUserByIdQuery(userId);
        var result = await userQueryService.Handle(getUserByIdQuery);
        return result?.Email ?? string.Empty;
    }
}
