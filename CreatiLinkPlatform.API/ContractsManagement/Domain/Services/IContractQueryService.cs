using CreatiLinkPlatform.ContractsManagement.Domain.Model.Aggregates;
using CreatiLinkPlatform.ContractsManagement.Domain.Model.Queries;

namespace CreatiLinkPlatform.ContractsManagement.Domain.Services;

/// <summary>
/// Contract query service interface
/// </summary>
public interface IContractQueryService
{
    Task<IEnumerable<Contract>> Handle(GetAllContractsQuery query);
    /// <summary>
    /// Handle get all contracts by user id query
    /// </summary>
    /// <param name="query">
    /// The <see cref="GetAllContractsByUserIdQuery"/> query
    /// </param>
    /// <returns>
    /// The <see cref="IEnumerable{Contract}"/> object with the contracts
    /// </returns>
    Task<IEnumerable<Contract>> Handle(GetAllContractsByUserIdQuery query);

    /// <summary>
    /// Handle get contract by id query
    /// </summary>
    /// <param name="query">
    /// The <see cref="GetContractByIdQuery"/> query
    /// </param>
    /// <returns>
    /// The <see cref="Contract"/> object with the contract
    /// </returns>
    Task<Contract?> Handle(GetContractByIdQuery query);

    /// <summary>
    /// Handle get all contracts by designer profile id query
    /// </summary>
    /// <param name="query">
    /// The <see cref="GetAllContractsByDesignerProfileIdQuery"/> query
    /// </param>
    /// <returns>
    /// The <see cref="IEnumerable{Contract}"/> object with the contracts
    /// </returns>
    Task<IEnumerable<Contract>> Handle(GetAllContractsByProfileIdQuery query);
}