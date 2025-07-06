using CreatiLinkPlatform.ContractsManagement.Domain.Model.Aggregates;
using CreatiLinkPlatform.ContractsManagement.Domain.Model.Commands;

namespace CreatiLinkPlatform.ContractsManagement.Domain.Services;

/// <summary>
/// Contract command service interface
/// </summary>
public interface IContractCommandService
{
    /// <summary>
    /// Handle create contract command
    /// </summary>
    /// <param name="command">
    /// The <see cref="CreateContractCommand"/> command
    /// </param>
    /// <returns>
    /// The <see cref="Contract"/> object with the created contract
    /// </returns>
    Task<Contract?> Handle(CreateContractCommand command);

    /// <summary>
    /// Handle update contract command
    /// </summary>
    /// <param name="command">
    /// The <see cref="UpdateContractCommand"/> command
    /// </param>
    /// <returns>
    /// The <see cref="Contract"/> object with the updated contract
    /// </returns>
    Task<Contract?> Handle(UpdateContractCommand command);

    /// <summary>
    /// Handle delete contract command
    /// </summary>
    /// <param name="command">
    /// The <see cref="DeleteContractCommand"/> command
    /// </param>
    /// <returns>
    /// The <see cref="Task"/> object
    /// </returns>
    Task Handle(DeleteContractCommand command);
}