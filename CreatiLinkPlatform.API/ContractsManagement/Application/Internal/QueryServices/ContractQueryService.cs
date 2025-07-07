using CreatiLinkPlatform.ContractsManagement.Domain.Model.Aggregates;
using CreatiLinkPlatform.ContractsManagement.Domain.Model.Queries;
using CreatiLinkPlatform.ContractsManagement.Domain.Repositories;
using CreatiLinkPlatform.ContractsManagement.Domain.Services;

namespace CreatiLinkPlatform.ContractsManagement.Application.Internal.QueryServices;

/// <summary>
/// Contract query service
/// </summary>
/// <param name="contractRepository">
/// Contract repository
/// </param>
public class ContractQueryService(IContractRepository contractRepository) : IContractQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<Contract>> Handle(GetAllContractsByUserIdQuery query)
    {
        return await contractRepository.FindContractsByUserIdAsync(query.UserId);
    }

    /// <inheritdoc />
    public async Task<Contract?> Handle(GetContractByIdQuery query)
    {
        return await contractRepository.FindContractByIdAsync(query.ContractId);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Contract>> Handle(GetAllContractsByProfileIdQuery query)
    {
        return await contractRepository.FindContractsByDesignerProfileIdAsync(query.ProfileId);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Contract>> Handle(GetAllContractsQuery query)
    {
        return await contractRepository.FindAllContractsAsync();
    }
}