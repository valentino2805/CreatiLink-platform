using CreatiLinkPlatform.API.Shared.Domain.Repositories;
using CreatiLinkPlatform.ContractsManagement.Domain.Model.Aggregates;

namespace CreatiLinkPlatform.ContractsManagement.Domain.Repositories;
//TODO: Fix aggregate call 

/// <summary>
/// ContractsManagement. repository interface
/// </summary>
public interface IContractRepository : IBaseRepository<Contract>
{
    /// <summary>
    /// Find contracts by user id
    /// </summary>
    /// <param name="userId">
    /// The user id to search for
    /// </param>
    /// <returns>
    /// A collection of <see cref="Contract"/> if found
    /// </returns>
    Task<IEnumerable<Contract>> FindContractsByUserIdAsync(int userId);

    /// <summary>
    /// Find contracts by designer profile id
    /// </summary>
    /// <param name="designerProfileId">
    /// The designer profile id to search for
    /// </param>
    /// <returns>
    /// A collection of <see cref="Contract"/> if found
    /// </returns>
    Task<IEnumerable<Contract>> FindContractsByDesignerProfileIdAsync(int designerProfileId);

    /// <summary>
    /// Find contract by id
    /// </summary>
    /// <param name="contractId">
    /// The contract id to search for
    /// </param>
    /// <returns>
    /// The <see cref="Contract"/> if found, otherwise null
    /// </returns>
    Task<Contract?> FindContractByIdAsync(int contractId);

    /// <summary>
    /// Check if a contract exists for a specific user and designer profile
    /// </summary>
    /// <param name="userId">
    /// The user id to search for
    /// </param>
    /// <param name="designerProfileId">
    /// The designer profile id to search for
    /// </param>
    /// <returns>
    /// True if the contract exists, otherwise false
    /// </returns>
    Task<bool> ContractExistsForUserAndDesignerProfileAsync(int userId, int designerProfileId);

    /// <summary>
    /// Find all contracts
    /// </summary>
    /// <returns>
    /// A collection of all <see cref="Contract"/>
    /// </returns>
    Task<IEnumerable<Contract>> FindAllContractsAsync();
}