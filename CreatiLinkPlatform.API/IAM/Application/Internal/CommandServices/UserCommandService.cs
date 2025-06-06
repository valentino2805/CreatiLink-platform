using CreatiLinkPlatform.API.IAM.Application.Internal.OutboundServices;
using CreatiLinkPlatform.API.IAM.Domain.Model.Aggregates;
using CreatiLinkPlatform.API.IAM.Domain.Model.Commands;
using CreatiLinkPlatform.API.IAM.Domain.Repositories;
using CreatiLinkPlatform.API.IAM.Domain.Services;
using CreatiLinkPlatform.API.Shared.Domain.Repositories;

namespace CreatiLinkPlatform.API.IAM.Application.Internal.CommandServices;


public class UserCommandService(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    ITokenService tokenService,
    IHashingService hashingService
    ) : IUserCommandService
{
    // inheritDoc
    public async Task Handle(SignUpCommand command)
    {
        // Validar que el rol sea uno de los permitidos
        var validRoles = new[] { "cliente", "profile" };
        if (!validRoles.Contains(command.Role.ToLower()))
            throw new Exception("Rol no válido. Debe ser 'cliente' o 'profile'.");

        if (userRepository.ExistsByEmail(command.Email))
            throw new Exception($"Email {command.Email} already exists");

        var hashedPassword = hashingService.HashPassword(command.Password);

        var user = new Users(command.Email, hashedPassword, command.Role);

        try
        {
            await userRepository.AddAsync(user);
            await unitOfWork.CompleteAsync();

            user.AssignProfileIdFromUserId();
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new Exception($"An error occurred while creating the user: {e.Message}");
        }
    }



    // inheritDoc
    public async Task<(Users user, string token)> Handle(SignInCommand command)
    {
        var users = await userRepository.FindByEmailAsync(command.Email);
        if (users is null) throw new Exception($"User {command.Email} not found");

        if (!hashingService.VerifyPassword(command.Password, users.Password))
            throw new Exception("Invalid password");

        var token = tokenService.GenerateToken(users);
        return (users, token);
    }

}