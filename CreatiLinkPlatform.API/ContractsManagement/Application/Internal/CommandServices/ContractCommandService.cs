using CreatiLinkPlatform.API.IAM.Domain.Repositories;
using CreatiLinkPlatform.API.Shared.Domain.Repositories;
using CreatiLinkPlatform.ContractsManagement.Domain.Model.Aggregates;
using CreatiLinkPlatform.ContractsManagement.Domain.Model.Commands;
using CreatiLinkPlatform.ContractsManagement.Domain.Repositories;
using CreatiLinkPlatform.ContractsManagement.Domain.Services;

namespace CreatiLinkPlatform.ContractsManagement.Application.Internal.CommandServices;

/// <summary>
/// Contract command service
/// </summary>
/// <param name="contractRepository">Contract repository</param>
/// <param name="userRepository">User repository</param>
/// <param name="unitOfWork">Unit of work</param>
public class ContractCommandService(
    IContractRepository contractRepository,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork)
    : IContractCommandService
{
    /// <inheritdoc />
    public async Task<Contract?> Handle(CreateContractCommand command)
    {
        // Validate ClientUserId and DesignerProfileId roles
        var clientUser = await userRepository.FindByIdAsync(command.ClientUserId);
        if (clientUser == null || clientUser.Role.ToLower() != "cliente")
            throw new InvalidOperationException("Invalid ClientUserId or user is not a 'cliente'.");

        var designerProfile = await userRepository.FindByIdAsync(command.DesignerProfileId);
        if (designerProfile == null || designerProfile.Role.ToLower() != "profile")
            throw new InvalidOperationException("Invalid DesignerProfileId or user is not a 'profile'.");

        // Check if a contract already exists
        var existingContract = await contractRepository.ContractExistsForUserAndDesignerProfileAsync(command.ClientUserId, command.DesignerProfileId);
        if (existingContract)
            throw new InvalidOperationException("A contract already exists between this user and designer profile.");

        // Create a new contract
        var contract = new Contract(command);

        // Add the contract to the repository
        await contractRepository.AddAsync(contract);
        await unitOfWork.CompleteAsync();

        return contract;
    }

    /// <inheritdoc />
    public async Task<Contract?> Handle(UpdateContractCommand command)
    {
        var contract = await contractRepository.FindContractByIdAsync(command.ContractId);
        if (contract == null)
            throw new InvalidOperationException("Contract not found.");
        contractRepository.Update(contract);
        await unitOfWork.CompleteAsync();

        return contract;
    }

    /// <inheritdoc />
    public async Task Handle(DeleteContractCommand command)
    {
        var contract = await contractRepository.FindContractByIdAsync(command.ContractId);
        if (contract != null)
        {
            contractRepository.Remove(contract);
            await unitOfWork.CompleteAsync();
        }
    }
}